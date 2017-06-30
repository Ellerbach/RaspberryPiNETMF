using System;
using System.Runtime.InteropServices;
using System.Text;

namespace RaspberryPiNETMF
{
    class ImportsWiringPi
    {
        #region importdllPWMandReadWrite
        //void pwmSetMode (int mode)
        [DllImport("libwiringPi.so", EntryPoint = "pwmSetMode")]
        static extern void pwmSetMode(int mode);      
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
        static extern void pinMode(int pin, int mode);
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
        [DllImport("libwiringPi.so", EntryPoint = "wiringPiISR")]
        static extern int wiringPiISR (int pin, int mode, [MarshalAs(UnmanagedType.FunctionPtr)]CallBackITR function);
        public delegate void CallBackITR();
        //static void initialiseEpoch (void)
        [DllImport("libwiringPi.so", EntryPoint = "initialiseEpoch")]
        static extern void initialiseEpoch();
        //void delay (unsigned int howLong)
        [DllImport("libwiringPi.so", EntryPoint = "delay")]
        static extern void delay (uint howLong);
        //void delayMicrosecondsHard (unsigned int howLong)
        [DllImport("libwiringPi.so", EntryPoint = "delayMicrosecondsHard")]
        static extern void delayMicrosecondsHard (uint howLong);
        //void delayMicroseconds (unsigned int howLong)
        [DllImport("libwiringPi.so", EntryPoint = "delayMicroseconds")]
        static extern void delayMicroseconds (uint howLong);
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

        enum WPI_Mode { Pins = 0, GPIO = 1, GPIO_SYS = 2, Phys = 3, PiFace = 4, Uninitialised = -1 };
        //        #define	WPI_MODE_PINS		 0
        //#define	WPI_MODE_GPIO		 1
        //#define	WPI_MODE_GPIO_SYS	 2
        //#define	WPI_MODE_PHYS		 3
        //#define	WPI_MODE_PIFACE		 4
        //#define	WPI_MODE_UNINITIALISED	-1

        //// Pin modes
        enum Pin_Mode { Input = 0, Output = 1, PWM = 2, GPIO_Clock = 3 };

        //#define	INPUT			 0
        //#define	OUTPUT			 1
        //#define	PWM_OUTPUT		 2
        //#define	GPIO_CLOCK		 3

        enum Digital { Low = 0, High = 1};
        //#define	LOW			 0
        //#define	HIGH			 1

        //// Pull up/down/none
         enum PUD { Off = 0, Down = 1, Up = 2};
        //#define	PUD_OFF			 0
        //#define	PUD_DOWN		 1
        //#define	PUD_UP			 2

        //// PWM
        enum PWM_Mode { MS =  0, Bal = 1 };
        //#define	PWM_MODE_MS		0
        //#define	PWM_MODE_BAL		1

        //// Interrupt levels
        enum Interrup { Edge = 0, Falling = 1, Rising = 2, Both = 3 };
        //#define	INT_EDGE_SETUP		0
        //#define	INT_EDGE_FALLING	1
        //#define	INT_EDGE_RISING		2
        //#define	INT_EDGE_BOTH		3
        #endregion

        #region ImportDllSPI
        //int wiringPiSPIGetFd (int channel)
        [DllImport("libwiringPiSPI.so", EntryPoint = "wiringPiSPIGetFd")]
        static extern int wiringPiSPIGetFd(int channel);      
        //int wiringPiSPIDataRW (int channel, unsigned char *data, int len)
        [DllImport("libwiringPiSPI.so", EntryPoint = "wiringPiSPIDataRW")]
        static extern int wiringPiSPIDataRW(int channel, [MarshalAs(UnmanagedType.LPArray)]byte[] data, int len);
        //int wiringPiSPISetup (int channel, int speed)
        [DllImport("libwiringPiSPI.so", EntryPoint = "wiringPiSPISetup")]
        static extern int wiringPiSPISetup(int channel, int speed);
        #endregion
    }
}
