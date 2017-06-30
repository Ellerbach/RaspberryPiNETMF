using System;
using System.Collections;
using System.Threading;
using System.IO;
using System.Runtime.CompilerServices;

namespace Microsoft.SPOT.Hardware
{
    public class HardwareProvider
    {
        private static HardwareProvider s_hwProvider = null;

        //--//

        public static void Register(HardwareProvider provider)
        {
            s_hwProvider = provider;

        }

        //--//

        public static HardwareProvider HwProvider
        {
            get
            {
                if (s_hwProvider == null)
                {
                    s_hwProvider = new HardwareProvider();
                }

                return s_hwProvider;
            }
        }

        //--//

        public virtual void GetSerialPins(string comPort, out Cpu.Pin rxPin, out Cpu.Pin txPin, out Cpu.Pin ctsPin, out Cpu.Pin rtsPin)
        {
            //no need only 1 com port
            //int comIdx = System.IO.Ports.SerialPortName.ConvertNameToIndex(comPort);

            rxPin = Cpu.Pin.Pin_P1_10;
            txPin = Cpu.Pin.Pin_P1_08;
            ctsPin = Cpu.Pin.GPIO_NONE;
            rtsPin = Cpu.Pin.GPIO_NONE;

            //NativeGetSerialPins(comIdx, ref rxPin, ref txPin, ref ctsPin, ref rtsPin);
        }

        public virtual int GetSerialPortsCount()
        {
            //only 1 com port
            return 1;
        }

        public virtual bool SupportsNonStandardBaudRate(int com)
        {
            //support only standard ones
            return false;
        }

        //public virtual void GetBaudRateBoundary(int com, out uint MaxBaudRate, out uint MinBaudRate)
        //{
        //    // only 1 Com and support only standards
        //    MaxBaudRate = (uint)System.IO.Ports.BaudRate.Baudrate230400;
        //    MinBaudRate = (uint)System.IO.Ports.BaudRate.Baudrate75;
        //}

        public virtual bool IsSupportedBaudRate(int com, ref uint baudrateHz)
        {
            uint[] baudrateSet = new uint[]  { 75,
                                               150,
                                               300,
                                               600,
                                               1200,
                                               2400,
                                               4800,
                                               9600,
                                               19200,
                                               38400,
                                               57600,
                                               115200,
                                               230400,
                                              };
            for (int i = 0; i < baudrateSet.Length; i++)
                if (baudrateSet[i] == baudrateHz)
                    return true;
            return false; //NativeIsSupportedBaudRate(com, ref baudrateHz);
        }

        //public virtual void GetSupportBaudRates(int com, out System.IO.Ports.BaudRate[] StdBaudRate, out int size)
        //{
        //    uint rBaudrate = 0;
        //    uint[] baudrateSet = new uint[]  { 75,
        //                                       150,
        //                                       300,
        //                                       600,
        //                                       1200,
        //                                       2400,
        //                                       4800,
        //                                       9600,
        //                                       19200,
        //                                       38400,
        //                                       57600,
        //                                       115200,
        //                                       230400,
        //                                      };

        //    StdBaudRate = new System.IO.Ports.BaudRate[13] {    System.IO.Ports.BaudRate.BaudrateNONE,
        //                                                        System.IO.Ports.BaudRate.BaudrateNONE,
        //                                                        System.IO.Ports.BaudRate.BaudrateNONE,
        //                                                        System.IO.Ports.BaudRate.BaudrateNONE,
        //                                                        System.IO.Ports.BaudRate.BaudrateNONE,
        //                                                        System.IO.Ports.BaudRate.BaudrateNONE,
        //                                                        System.IO.Ports.BaudRate.BaudrateNONE,
        //                                                        System.IO.Ports.BaudRate.BaudrateNONE,
        //                                                        System.IO.Ports.BaudRate.BaudrateNONE,
        //                                                        System.IO.Ports.BaudRate.BaudrateNONE,
        //                                                        System.IO.Ports.BaudRate.BaudrateNONE,
        //                                                        System.IO.Ports.BaudRate.BaudrateNONE,
        //                                                        System.IO.Ports.BaudRate.BaudrateNONE,
        //                                                    };

        //    size = 0;
        //    for (int i = 0; i < baudrateSet.Length; i++)
        //    {
        //        rBaudrate = baudrateSet[i];
        //        if (IsSupportedBaudRate(com, ref rBaudrate))
        //        {
        //            StdBaudRate[size] = (System.IO.Ports.BaudRate)rBaudrate;
        //            size++;
        //        }
        //    }

        //}

        //--//
        public virtual void GetSpiPins(SPI.SPI_module spi_mod, out Cpu.Pin msk, out Cpu.Pin miso, out Cpu.Pin mosi)
        {
            msk = Cpu.Pin.Pin_P1_23;
            miso = Cpu.Pin.Pin_P1_21;
            mosi = Cpu.Pin.Pin_P1_19;
        }

        public virtual int GetSpiPortsCount()
        {
            // only 1 SPI on Raspberry
            return 1;
        }

        //--//
        public virtual void GetI2CPins(out Cpu.Pin scl, out Cpu.Pin sda)
        {
            scl = Cpu.Pin.Pin_P1_05;
            sda = Cpu.Pin.Pin_P1_03;
        }


        public virtual int GetPWMChannelsCount()
        {
            // Only 1 native but lots of "soft ones"
            return 1;
        }

        public virtual Cpu.Pin GetPwmPinForChannel(Cpu.PWMChannel channel)
        {
            //only 1 PWM so return all the time the same
            return Cpu.Pin.Pin_P1_12;
        }

        //--//

        public virtual int GetAnalogChannelsCount()
        {
            //no analog on Raspberry
            return 0;
        }

        public virtual Cpu.Pin GetAnalogPinForChannel(Cpu.AnalogChannel channel)
        {
            return Cpu.Pin.GPIO_NONE;
        }

        public virtual int[] GetAvailablePrecisionInBitsForChannel(Cpu.AnalogChannel channel)
        {
            //Return nothing as no analog
            return null; // new int[0]; //NativeGetAvailablePrecisionInBitsForChannel(channel);
        }

        //--//
        
        public virtual int GetPinsCount()
        {
            // assuming we are running on V2, on V1, it's different, 4 less
            // TODO: fix it to check the version
            return 21;//NativeGetPinsCount();
        }

        public virtual void GetPinsMap(out Cpu.PinUsage[] pins, out int PinCount)
        {

            PinCount = GetPinsCount();

            pins = new Cpu.PinUsage[PinCount];

            //return something that is filled with specific usage
            for(int i = 0; i<PinCount; i++)
                pins[i] = Cpu.PinUsage.ALTERNATE_A;

            //NativeGetPinsMap(pins);
        }

        public virtual Cpu.PinUsage GetPinsUsage(Cpu.Pin pin)
        {
            return Cpu.PinUsage.ALTERNATE_A;
        }

        public virtual Cpu.PinValidResistorMode GetSupportedResistorModes(Cpu.Pin pin)
        {
            return Cpu.PinValidResistorMode.NONE;
        }

        public virtual Cpu.PinValidInterruptMode GetSupportedInterruptModes(Cpu.Pin pin)
        {
            //all pins are both Edge
            return Cpu.PinValidInterruptMode.InterruptEdgeBoth;
        }

        //--//

        public virtual Cpu.Pin GetButtonPins(Button iButton)
        {
            // no button on the Raspberry
            return Cpu.Pin.GPIO_NONE;
        }

        //--//
        public virtual void GetLCDMetrics(out int width, out int height, out int bitsPerPixel, out int orientationDeg)
        {
            // no LCD so return 0
            // TODO: check resolution, implement this
            height = 0;
            width = 0;
            bitsPerPixel = 0;
            orientationDeg = 0;
            //NativeGetLCDMetrics(out height, out width, out bitsPerPixel, out orientationDeg);
        }

 
    }
}


