using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.SPOT.Hardware
{
       public class PWM : IDisposable
    {

                  #region importdll
        //void pwmSetMode (int mode)
        [DllImport("libwiringPi.so", EntryPoint = "pwmSetMode")]
        static extern void pwmSetMode(PWM_Mode mode);
        //void pwmSetRange (unsigned int range)
        [DllImport("libwiringPi.so", EntryPoint = "pwmSetRange")]
        static extern void pwmSetRange(uint range);
        //void pwmSetClock (int divisor)
        [DllImport("libwiringPi.so", EntryPoint = "pwmSetClock")]
        static extern void pwmSetClock(int divisor);
        //void gpioClockSet (int pin, int freq)
        [DllImport("libwiringPi.so", EntryPoint = "gpioClockSet")]
        static extern void gpioClockSet(int pin, int freq);
        //void pinMode (int pin, int mode)
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

        enum WPI_Mode : int { Pins = 0, GPIO = 1, GPIO_SYS = 2, Phys = 3, PiFace = 4, Uninitialised = -1 };
        //        #define	WPI_MODE_PINS		 0
        //#define	WPI_MODE_GPIO		 1
        //#define	WPI_MODE_GPIO_SYS	 2
        //#define	WPI_MODE_PHYS		 3
        //#define	WPI_MODE_PIFACE		 4
        //#define	WPI_MODE_UNINITIALISED	-1

        //// Pin modes
        enum Pin_Mode : int { Input = 0, Output = 1, PWM = 2, GPIO_Clock = 3 };

        //#define	INPUT			 0
        //#define	OUTPUT			 1
        //#define	PWM_OUTPUT		 2
        //#define	GPIO_CLOCK		 3

        enum Digital { Low = 0, High = 1 };
        //#define	LOW			 0
        //#define	HIGH			 1

        //// Pull up/down/none
        enum PUD { Off = 0, Down = 1, Up = 2 };
        //#define	PUD_OFF			 0
        //#define	PUD_DOWN		 1
        //#define	PUD_UP			 2

        //// PWM
        enum PWM_Mode { MS = 0, Bal = 1 };
        //#define	PWM_MODE_MS		0
        //#define	PWM_MODE_BAL		1

        //// Interrupt levels
        enum Interrup { Edge = 0, Falling = 1, Rising = 2, Both = 3 };
        //#define	INT_EDGE_SETUP		0
        //#define	INT_EDGE_FALLING	1
        //#define	INT_EDGE_RISING		2
        //#define	INT_EDGE_BOTH		3

        // Pin mapping is different!
        // https://projects.drogon.net/raspberry-pi/wiringpi/pins/
        #endregion
        public enum ScaleFactor : uint
        {
            Milliseconds = 1000,
            Microseconds = 1000000,
            Nanoseconds  = 1000000000,
        }

        //--//
        
        /// <summary>
        /// The pin used for this PWM port, can be set only when the port is constructed
        /// </summary>
        private readonly Cpu.Pin m_pin;
        /// <summary>
        /// The channel used for this PWM port, can be set only when the port is constructed
        /// </summary>
        private readonly Cpu.PWMChannel m_channel;
        /// <summary>
        /// The period of the PWM in microseconds
        /// </summary>
        private uint m_period;
        /// <summary>
        /// The Duty Cycle of the PWM wave in microseconds
        /// </summary>
        private uint m_duration;
        /// <summary>
        /// Polarity of the wave, it determines the idle state of the port
        /// </summary>
        private bool m_invert;
        /// <summary>
        /// Scale of the period/duration (mS, uS, nS)
        /// </summary>
        private ScaleFactor m_scale;

        //--//

        /// <summary>
        /// Build an instance of the PWM type
        /// </summary>
        /// <param name="channel">The channel to use</param>
        /// <param name="frequency_Hz">The frequency of the pulse in Hz</param>
        /// <param name="dutyCycle">The duty cycle of the pulse as a fraction of unity.  Value should be between 0.0 and 1.0</param>
        /// <param name="invert">Whether the output should be inverted or not</param>
        public PWM(Cpu.PWMChannel channel, double frequency_Hz, double dutyCycle, bool invert)
        {
            HardwareProvider hwProvider = HardwareProvider.HwProvider;

            if(hwProvider == null) throw new InvalidOperationException();

            m_pin = hwProvider.GetPwmPinForChannel(channel);
            m_channel = channel;
            //--//
            m_period = PeriodFromFrequency(frequency_Hz, out m_scale);
            m_duration = DurationFromDutyCycleAndPeriod(dutyCycle, m_period);
            m_invert = invert;
            //--//
            try
            {
                Init();
                
                Commit();
                
                Port.ReservePin(m_pin, true);
            }
            catch
            {
                Dispose(false);
            }
        }

        /// <summary>
        /// Build an instance of the PWM type
        /// </summary>
        /// <param name="channel">The channel</param>
        /// <param name="period">The period of the pulse</param>
        /// <param name="duration">The duration of the pulse.  The value should be a fraction of the period</param>
        /// <param name="scale">The scale factor for the period/duration (nS, uS, mS)</param>
        /// <param name="invert">Whether the output should be inverted or not</param>
        public PWM(Cpu.PWMChannel channel, uint period, uint duration, ScaleFactor scale, bool invert)
        {
            HardwareProvider hwProvider = HardwareProvider.HwProvider;

            if (hwProvider == null) throw new InvalidOperationException();

            m_pin      = hwProvider.GetPwmPinForChannel(channel);
            m_channel  = channel;
            //--//
            m_period   = period;
            m_duration = duration;
            m_scale    = scale;
            m_invert   = invert;
            //--//
            try
            {
                Init();
                
                Commit();
                
                Port.ReservePin(m_pin, true);
            }
            catch
            {
                Dispose(false);
            }
        }

        /// <summary>
        /// Create a PWM, warning: only 1 PWM available on Raspberry
        /// Implement only hard PWM
        /// </summary>
        /// <param name="pin">must be GPIO_P1-12</param>
        public PWM(Cpu.Pin pin)
        {
            m_pin = pin;
            Init();     
        }

        /// <summary>
        /// The first parameter of SetPulse is the period, the second is the duration. 
        /// Some definitions:
        /// Duty Cycle 
        ///     Proportion of "on" time vs. the period. 
        ///     Expressed as a percent with 100% being fully on. 
        ///     This is used only with SetDutyCycle and the default clock rate. 
        /// Period 
        ///     "Peak to Peak" time. This is in microseconds (1/1,000,000 second). 
        ///     Used in SetPulse. 
        /// Duration 
        ///     Duration of the "on" time for a cycle. This is also in microseconds.
        ///     This needs to be less than the Period. Used in SetPulse. 
        /// </summary>
        /// <param name="dutyCycle">Proportion of "on" time vs. the period</param>
        public void SetDutyCycle(uint dutyCycle)
        {
            if (dutyCycle > 100)
                dutyCycle = 100;
            pwmWrite((int)Cpu.Pin.Pin_P1_12, (int)(dutyCycle * 1024 / 100));
        }

        /// <summary>
        /// The first parameter of SetPulse is the period, the second is the duration. 
        /// Some definitions:
        /// Duty Cycle 
        ///     Proportion of "on" time vs. the period. 
        ///     Expressed as a percent with 100% being fully on. 
        ///     This is used only with SetDutyCycle and the default clock rate. 
        /// Period 
        ///     "Peak to Peak" time. This is in microseconds (1/1,000,000 second). 
        ///     Used in SetPulse. 
        /// Duration 
        ///     Duration of the "on" time for a cycle. This is also in microseconds.
        ///     This needs to be less than the Period. Used in SetPulse. 
        /// </summary>
        /// <param name="period">"Peak to Peak" time. This is in microseconds (1/1,000,000 second)</param>
        /// <param name="duration">Duration of the "on" time for a cycle. This is also in microseconds.</param>
        public void SetPulse(uint period, uint duration)
        {
            if (duration > period)
                duration = period;
            if (period == 0)
                return;
            pwmSetMode(PWM_Mode.Bal);	// Pi default mode
            pwmSetRange(1024);		// Default range of 
            pwmSetClock((int)(19.2 * period));			// 19.2 / 32 = 600KHz - Also starts the PWM
            SetDutyCycle(duration / period);

        }
        /// <summary>
        /// Finalizer for the PWM type, will stop the port if still running and un-reserve the underlying pin
        /// </summary>
        ~PWM()
        {
            Dispose(false);
        }

        /// <summary>
        /// Diposes the PWM type, will stop the port if still running and un-reserve the PIN
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Starts the PWM port for an indefinite amount of time
        /// </summary>
        public void Start()
        {
            SetDutyCycle((uint)DutyCycle);
        
        }

        /// <summary>
        /// Stop the PWM port
        /// </summary>
        public void Stop()
        {
            SetDutyCycle(0);
        }

        //--//

        /// <summary>
        /// The GPIO pin chosen for the selected channel
        /// </summary>
        public Cpu.Pin Pin
        {
            get
            {
                return m_pin;
            }
        }

        /// <summary>
        /// Gets and sets the frequency (in Hz) of the pulse
        /// </summary>
        public double Frequency
        {
            get
            {
                return FrequencyFromPeriod(m_period, m_scale);
            }
            set
            {
                m_period = PeriodFromFrequency(value, out m_scale);
                Commit();
                //--//
            }
        }


        /// <summary>
        /// Gets and sets the duty cycle of the pulse as a fraction of unity. Value should be included between 0.0 and 1.0
        /// </summary>
        public double DutyCycle
        {
            get
            {
                return DutyCycleFromDurationAndPeriod(m_period, m_duration);
            }
            set
            {
                m_duration = DurationFromDutyCycleAndPeriod(value, m_period);
                Commit();
                //--//
            }
        }


        /// <summary>
        /// Gets and sets the Frequency of the pulse
        /// </summary>
        public uint Period
        {
            get
            {
                return m_period;
            }
            set
            {
                m_period = value;
                Commit();
                //--//
            }
        }


        /// <summary>
        /// Gets and sets the duration of the pulse.  Value should be a frction of the period
        /// </summary>
        public uint Duration
        {
            get
            {
                return m_duration;
            }
            set
            {
                m_duration = value;
                Commit();
                //--//
            }
        }

        /// <summary>
        /// Gets or sets the scale factor for the Duration and Period.  Setting the Scale does not cause 
        /// an immediate update to the PWM.   The update occurs when Duration or Period are set.
        /// </summary>
        public ScaleFactor Scale
        {
            get
            {
                return m_scale;
            }
            set
            {
                m_scale = value;
                Commit();
                //--//
            }
        }
        

        //--//

        /// <summary>
        /// Starts a number of PWM ports at the same time
        /// </summary>
        /// <param name="ports"></param>
        public static void Start(PWM[] ports)
        { 
        for (int i = 0; i< ports.Length; i++)
            {
                ports[i].Start();
            }
        }

        /// <summary>
        /// Stops a number of PWM ports at the same time
        /// </summary>
        /// <param name="ports"></param>
        public static void Stop(PWM[] ports)
        {
            for (int i = 0; i < ports.Length; i++)
            {
                ports[i].Stop();
            }
        }
        //--//

        protected void Dispose(bool disposing)
        {
            try
            {
                Stop();
            }
            catch
            {
                // hide all exceptions...
            }
            finally
            {
                Uninit();

                Port.ReservePin(m_pin, false);
            }
        }

        /// <summary>
        /// Moves values to the HAL
        /// </summary>
        protected void Commit()
        {
            SetPulse(m_period, m_duration);
        }

        /// <summary>
        /// Initializes the controller
        /// </summary>
        protected void Init()
        {
            if (m_pin != Cpu.Pin.Pin_P1_12)
                throw new Exception("Wrong Pin, only GPIO-P1-12 available");
            if (wiringPiSetup() == -1)
                throw new Exception("Unable to initialize bcm2835.so library");
            // only pin 1 is hard PWM
            pinMode((int)Cpu.Pin.Pin_P1_12, Pin_Mode.PWM);     
        }

        /// <summary>
        /// Uninitializes the controller
        /// </summary>   
        protected void Uninit()
        { 
            //just do nothing..
        }

        //--//

        private static uint PeriodFromFrequency(double f, out ScaleFactor scale)
        {
            if(f >= 1000.0)
            {
                scale = ScaleFactor.Nanoseconds;
                return (uint)(((uint)ScaleFactor.Nanoseconds / f) + 0.5);
            }
            else if(f >= 1.0)
            {
                scale = ScaleFactor.Microseconds;
                return (uint)(((uint)ScaleFactor.Microseconds / f) + 0.5);
            }
            else
            {
                scale = ScaleFactor.Milliseconds;
                return (uint)(((uint)ScaleFactor.Milliseconds / f) + 0.5);
            }
        }

        private static uint DurationFromDutyCycleAndPeriod(double dutyCycle, double period)
        {
            if (period <= 0)
                throw new ArgumentException();

            if (dutyCycle < 0)
                return 0;

            if (dutyCycle > 1)
                return 1;

            return (uint)(dutyCycle * period);            
        }

        private static double FrequencyFromPeriod(double period, ScaleFactor scale)
        {
            return ((uint)scale / period);
        }

        private static double DutyCycleFromDurationAndPeriod(double period, double duration)
        {
            if (period <= 0)
                throw new ArgumentException();

            if (duration < 0)
                return 0;

            if (duration > period)
                return 1;

            return (duration / period);
        }
    }
}

