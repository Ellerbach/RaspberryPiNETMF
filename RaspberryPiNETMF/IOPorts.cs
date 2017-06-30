using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Collections;
using System.Runtime.InteropServices;

namespace Microsoft.SPOT.Hardware
{

    public delegate void NativeEventHandler(uint data1, uint data2, DateTime time);

    //--//

    public class NativeEventDispatcher : IDisposable
    {
        protected NativeEventHandler m_threadSpawn = null;
        protected NativeEventHandler m_callbacks = null;
        protected bool m_disposed = false;
        private object m_NativeEventDispatcher;

        //--//

        //method called for an interruption. Not sure to implement
        public NativeEventDispatcher(string strDriverName, ulong drvData) 
		{ }

        //need to implement in the caller class
        public virtual void EnableInterrupt()
        { }

        //need to implement in the caler class
        public virtual void DisableInterrupt()
        { }

        //if anything need to be cleaned...
        protected virtual void Dispose(bool disposing)
        { }

        //--//

        ~NativeEventDispatcher()
        {
            Dispose(false);
        }

        [MethodImplAttribute(MethodImplOptions.Synchronized)]
        public virtual void Dispose()
        {
            if (!m_disposed)
            {
                Dispose(true);

                GC.SuppressFinalize(this);

                m_disposed = true;
            }
        }

        public event NativeEventHandler OnInterrupt
        {
            [MethodImplAttribute(MethodImplOptions.Synchronized)]
            add
            {
                if (m_disposed)
                {
                    throw new ObjectDisposedException("Exception");
                }

                NativeEventHandler callbacksOld = m_callbacks;
                NativeEventHandler callbacksNew = (NativeEventHandler)Delegate.Combine(callbacksOld, value);

                try
                {
                    m_callbacks = callbacksNew;

                    if (callbacksNew != null)
                    {
                        if (callbacksOld == null)
                        {
                            EnableInterrupt();
                        }

                        if (callbacksNew.Equals(value) == false)
                        {
                            callbacksNew = new NativeEventHandler(this.MultiCastCase);
                        }
                    }

                    m_threadSpawn = callbacksNew;
                }
                catch
                {
                    m_callbacks = callbacksOld;

                    if (callbacksOld == null)
                    {
                        DisableInterrupt();
                    }

                    throw;
                }
            }

            [MethodImplAttribute(MethodImplOptions.Synchronized)]
            remove
            {
                if (m_disposed)
                {
                    throw new ObjectDisposedException("Exception");
                }

                NativeEventHandler callbacksOld = m_callbacks;
                NativeEventHandler callbacksNew = (NativeEventHandler)Delegate.Remove(callbacksOld, value);

                try
                {
                    m_callbacks = (NativeEventHandler)callbacksNew;

                    if (callbacksNew == null && callbacksOld != null)
                    {
                        DisableInterrupt();
                    }
                }
                catch
                {
                    m_callbacks = callbacksOld;

                    throw;
                }
            }
        }

        private void MultiCastCase(uint port, uint state, DateTime time)
        {
            NativeEventHandler callbacks = m_callbacks;

            if (callbacks != null)
            {
                callbacks(port, state, time);
            }
        }
    }

    //--//

    public class Port : NativeEventDispatcher
    {
        #region Imported functions
         [DllImport("libwiringPi.so", EntryPoint = "pinMode")]
        static extern void pinMode(int pin, Pin_Mode mode);
        //void pullUpDnControl (int pin, int pud)
        [DllImport("libwiringPi.so", EntryPoint = "pullUpDnControl")]
        static extern void pullUpDnControl(int pin, int pud);
        //int digitalRead (int pin)
        [DllImport("libwiringPi.so", EntryPoint = "digitalRead")]
        static extern int digitalRead(int pin);
        //void digitalWrite (int pin, int value)
        [DllImport("libwiringPi.so", EntryPoint = "digitalWrite")]
        static extern void digitalWrite(int pin, int value);
        //void pwmWrite (int pin, int value)
        [DllImport("libwiringPi.so", EntryPoint = "pwmWrite")]
        static extern void pwmWrite(int pin, int value);
        //void digitalWriteByte (int value)
        [DllImport("libwiringPi.so", EntryPoint = "digitalWriteByte")]
        static extern void digitalWriteByte(int value);
        //int waitForInterrupt (int pin, int mS)
        [DllImport("libwiringPi.so", EntryPoint = "waitForInterrupt")]
        static extern int waitForInterrupt(int pin, int mS);
        //static void *interruptHandler (void *arg)
        //        [DllImport("libwiringPi.so", EntryPoint = "interruptHandler")]
        //        static extern void* interruptHandler(void* arg);
        //int wiringPiISR (int pin, int mode, void (*function)(void))
        //        [DllImport("libwiringPi.so", EntryPoint = "wiringPiISR")]
        //        static extern int wiringPiISR (int pin, int mode, void (*function)(void));
        //static void initialiseEpoch (void)
        [DllImport("libwiringPi.so", EntryPoint = "initialiseEpoch")]
        static extern void initialiseEpoch();
        //void delay (unsigned int howLong)
        [DllImport("libwiringPi.so", EntryPoint = "delay")]
        static extern void delay(uint howLong);
        //void delayMicrosecondsHard (unsigned int howLong)
        [DllImport("libwiringPi.so", EntryPoint = "delayMicrosecondsHard")]
        static extern void delayMicrosecondsHard(uint howLong);
        //void delayMicroseconds (unsigned int howLong)
        [DllImport("libwiringPi.so", EntryPoint = "delayMicroseconds")]
        static extern void delayMicroseconds(uint howLong);
        //unsigned int millis (void)
        [DllImport("libwiringPi.so", EntryPoint = "millis")]
        static extern uint millis();
        //unsigned int micros (void)
        [DllImport("libwiringPi.so", EntryPoint = "micros")]
        static extern uint micros();
        //int wiringPiSetup (void)
        [DllImport("libwiringPi.so", EntryPoint = "wiringPiSetup")]
        static extern int wiringPiSetup();
        //int wiringPiSetupGpio (void)
        [DllImport("libwiringPi.so", EntryPoint = "wiringPiSetupGpio")]
        static extern int wiringPiSetupGpio();
        //int wiringPiSetupPhys (void)
        [DllImport("libwiringPi.so", EntryPoint = "wiringPiSetupPhys")]
        static extern int wiringPiSetupPhys();
        //int wiringPiSetupSys (void)
        [DllImport("libwiringPi.so", EntryPoint = "wiringPiSetupSys")]
        static extern int wiringPiSetupSys();

		//determine the kind of Mode for pin initialization
        private enum WPI_Mode : int { Pins = 0, GPIO = 1, GPIO_SYS = 2, Phys = 3, PiFace = 4, Uninitialised = -1 };
        
        // Pin modes
        private enum Pin_Mode : int { Input = 0, Output = 1, PWM = 2, GPIO_Clock = 3 };
        #endregion

		// resistor mode. Not a real interest in Raspberry as
		// no Pull up or down is provded
        public enum ResistorMode
        {
            Disabled = 0,
            PullDown = 1,
            PullUp = 2,
        }
		
		// Interrupt mode
        public enum InterruptMode
        {
            InterruptNone = 0,
            InterruptEdgeLow = 1,
            InterruptEdgeHigh = 2,
            InterruptEdgeBoth = 3,
            InterruptEdgeLevelHigh = 4,
            InterruptEdgeLevelLow = 5,
        }

        //--//

        private InterruptMode m_interruptMode;
        private ResistorMode m_resistorMode;
        private uint m_portId;
        private uint m_flags;
        private bool m_glitchFilterEnable;
        private bool m_initialState;
		
		
        //--//

        // Port initialization
        protected Port(Cpu.Pin portId, bool glitchFilter, ResistorMode resistor, InterruptMode interruptMode)
        :base("Port", (ulong)portId) 
        { 
			initPort(portId, false, glitchFilter, resistor, interruptMode);
		}

        // Port initialization
        protected Port(Cpu.Pin portId, bool initialState)
            : base("Port", (ulong)portId)
        { 
			initPort(portId, initialState, false, ResistorMode.Disabled, InterruptMode.InterruptNone);
		}

        // Port initialization
        protected Port(Cpu.Pin portId, bool initialState, bool glitchFilter, ResistorMode resistor)
            : base("Port", (ulong)portId)
        { 
			initPort(portId, initialState, glitchFilter, resistor, InterruptMode.InterruptNone);
		}

		// Called for all Port initialization
		// verify the library is inialized
		// store internal variables
		private void initPort(Cpu.Pin portId, bool initialState, bool glitchFilter, ResistorMode resistor, InterruptMode interruptMode)
		{
			if (wiringPiSetup () == -1)
                throw new Exception("Unable to initialize libwiringPi.so library");
            m_portId = (uint)portId;
			m_initialState = initialState;
			m_glitchFilterEnable = glitchFilter;
			m_resistorMode = resistor;
			m_interruptMode = interruptMode;
		}

        // If anything need to be cleaned
        protected override void Dispose(bool disposing)
        { 
			// maybe close the port at some point?
		}

        // Read the state of the port
        public bool Read()
        { 
			return (digitalRead((int)m_portId)!=0?true:false);  
		}

		// return the port id
        public Cpu.Pin Id
        {
            get { return (Cpu.Pin)m_portId; }
        }

        // Is it a special port?
		// by default, only accessible pins are listed. So none is reserved
        static public bool ReservePin(Cpu.Pin pin, bool fReserve)
        { return false;  }
    }

    //--//

	// Input port to read values
	// TODO: implement interrupt mode!
    public class InputPort : Port
    {
	
        [DllImport("libwiringPi.so", EntryPoint = "pinMode")]
        static extern void pinMode(int pin, Pin_Mode mode);
        //int digitalRead (int pin)
        [DllImport("libwiringPi.so", EntryPoint = "digitalRead")]
        static extern int digitalRead(int pin);
  
		// Pin modes
        private enum Pin_Mode : int { Input = 0, Output = 1, PWM = 2, GPIO_Clock = 3 };
		
        public InputPort(Cpu.Pin portId, bool glitchFilter, ResistorMode resistor)
            : base(portId, glitchFilter, resistor, InterruptMode.InterruptNone)
        {
			initPort(portId, glitchFilter, resistor, InterruptMode.InterruptNone);
        }

        protected InputPort(Cpu.Pin portId, bool glitchFilter, ResistorMode resistor, InterruptMode interruptMode)
            : base(portId, glitchFilter, resistor, interruptMode)
        {
			initPort(portId, glitchFilter, resistor, interruptMode);
        }

        protected InputPort(Cpu.Pin portId, bool initialState, bool glitchFilter, ResistorMode resistor)
            : base(portId, initialState, glitchFilter, resistor)
        {
			initPort(portId, glitchFilter, resistor, InterruptMode.InterruptNone);
			// need to understand what the initialState mean for an input port...
        }
		
		private void initPort(Cpu.Pin portId, bool glitchFilter, ResistorMode resistor, InterruptMode interruptMode)
		{
			this.Resistor = resistor;
			this.GlitchFilter = glitchFilter;
			pinMode((int)Id, Pin_Mode.Input);
		}

        public ResistorMode Resistor
        {
            get;
            set;
        }

         public bool GlitchFilter
        {
            get;
            set;
        }
    }

    //--//

	// OutoutPort to write on an output port
	// 
    public class OutputPort : Port
    {
        #region Imported functions
 //       [DllImport("libbcm2835.so", EntryPoint = "bcm2835_init")]
 //       static extern bool bcm2835_init();

 //       [DllImport("libbcm2835.so", EntryPoint = "bcm2835_gpio_fsel")]
 //       static extern void bcm2835_gpio_fsel(Cpu.Pin pin, byte mode_out);

//        [DllImport("libbcm2835.so", EntryPoint = "bcm2835_gpio_write")]
//        static extern void bcm2835_gpio_write(Cpu.Pin pin, byte value);
        [DllImport("libwiringPi.so", EntryPoint = "pinMode")]
        static extern void pinMode(int pin, Pin_Mode mode);
 		//void digitalWrite (int pin, int value)
        [DllImport("libwiringPi.so", EntryPoint = "digitalWrite")]
        static extern void digitalWrite(int pin, int value);
		 // Pin modes
        private enum Pin_Mode : int { Input = 0, Output = 1, PWM = 2, GPIO_Clock = 3 };

        #endregion

  //      private Cpu.Pin mPort;

        public OutputPort(Cpu.Pin portId, bool initialState)
            : base(portId, initialState)
        {
            initOutputPort(portId, initialState, false, ResistorMode.Disabled);
        }

        protected OutputPort(Cpu.Pin portId, bool initialState, bool glitchFilter, ResistorMode resistor)
            : base(portId, initialState, glitchFilter, resistor)
        {
            initOutputPort(portId, initialState, glitchFilter, resistor);
        }

        private void initOutputPort(Cpu.Pin portId, bool initialState, bool glitchFilter, ResistorMode resistor)
        {
 //           if (!bcm2835_init())
//                throw new Exception("Unable to initialize bcm2835.so library");
 //           mPort = portId;
 //           bcm2835_gpio_fsel(mPort, 1);
   			pinMode((int)Id, Pin_Mode.Output);
			this.InitialState = initialState;
            Write(InitialState); // bcm2835_gpio_write(mPort, 1);
        }

        //extern public void Write(bool state)
        public void Write(bool state)
        {
 //           bcm2835_gpio_write(mPort, (state == true ? (byte)1 : (byte)0) );
 			digitalWrite((int)Id, (state == true ? (byte)1 : (byte)0));
        }

         public bool InitialState
        {
            get;
            internal set;
        }
    }

    //--//

	// This is a tristate port which can be used to read and write
	// TODO: implement the tristate port :)
    public sealed class TristatePort : OutputPort
    {
        public TristatePort(Cpu.Pin portId, bool initialState, bool glitchFilter, ResistorMode resistor)
            : base(portId, initialState, glitchFilter, resistor)
        {
			Resistor = resistor;
			GlitchFilter = glitchFilter;
			Active = initialState;
        }

        public bool Active
        {
            get;
            set;
        }

        public ResistorMode Resistor
        {
            get;
            set;
        }

        public bool GlitchFilter
        {
            get;
            internal set;
        }
    }

    //--//

	// InterruptPort is derived from InputPort 
	// is used to get intettupted when something happen
	// TODO: impement the interruption modes
    public sealed class InterruptPort : InputPort
    {
        [DllImport("libwiringPi.so", EntryPoint = "wiringPiISR")]
        static extern int wiringPiISR(int pin, int mode, [MarshalAs(UnmanagedType.FunctionPtr)]CallBackITR function);
        public delegate void CallBackITR();

        bool bInterrup = false;
        
        public InterruptPort(Cpu.Pin portId, bool glitchFilter, ResistorMode resistor, InterruptMode interrupt)
            : base(portId, glitchFilter, resistor, interrupt)
        {
            m_threadSpawn = null;
            m_callbacks = null;
			Interrupt = interrupt;
        }

        public void ClearInterrupt()
		{
            // what to do here? does it stop?
            bInterrup = false;
		}

        public InterruptMode Interrupt
        {
            get;
            set;
        }

        // TODO: implement
        public override void EnableInterrupt()
        {
            bInterrup = true;
            wiringPiISR((int)Id, (int)Interrupt, WaitInterrupt);
        }

        // TODO : implement
        public override void DisableInterrupt()
		{
            bInterrup = false;
            wiringPiISR((int)Id, (int)InterruptMode.InterruptNone, WaitInterrupt);
		}

        private void WaitInterrupt()
        {
            if (bInterrup)
            {
                NativeEventHandler callbacks = m_callbacks;

                if (callbacks != null)
                {
                    //We don't know which stage it is in case of EdgeBoth, socheck the value of the pin..
                    if (Interrupt == InterruptMode.InterruptEdgeBoth)
                    {
                        if (Read())
                            callbacks((uint)Id, (uint)InterruptMode.InterruptEdgeHigh, DateTime.Now);
                        else
                            callbacks((uint)Id, (uint)InterruptMode.InterruptEdgeLow, DateTime.Now);
                    }
                    else
                        callbacks((uint)Id, (uint)Interrupt, DateTime.Now);
                }

            }
        }

    }
}


