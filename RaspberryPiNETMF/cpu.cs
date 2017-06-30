using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.SPOT.Hardware
{
    public static class Cpu
    {

        [Flags]
        public enum PinUsage : byte
        {
            NONE = 0,
            INPUT = 1,
            OUTPUT = 2,
            ALTERNATE_A = 4,
            ALTERNATE_B = 8,
        };

        [Flags]
        public enum PinValidResistorMode : byte
        {
            NONE = 0,
            Disabled = 1 << Microsoft.SPOT.Hardware.Port.ResistorMode.Disabled,
            PullUp = 1 << Microsoft.SPOT.Hardware.Port.ResistorMode.PullDown,
            PullDown = 1 << Microsoft.SPOT.Hardware.Port.ResistorMode.PullUp,
        };

        [Flags]
        public enum PinValidInterruptMode : byte
        {
            NONE = 0,
            InterruptEdgeLow = 1 << Microsoft.SPOT.Hardware.Port.InterruptMode.InterruptEdgeLow,
            InterruptEdgeHigh = 1 << Microsoft.SPOT.Hardware.Port.InterruptMode.InterruptEdgeHigh,
            InterruptEdgeBoth = 1 << Microsoft.SPOT.Hardware.Port.InterruptMode.InterruptEdgeBoth,
            InterruptEdgeLevelHigh = 1 << Microsoft.SPOT.Hardware.Port.InterruptMode.InterruptEdgeLevelHigh,
            InterruptEdgeLevelLow = 1 << Microsoft.SPOT.Hardware.Port.InterruptMode.InterruptEdgeLevelLow,
        };
        /// <remarks>
        /// Refer to http://elinux.org/Rpi_Low-level_peripherals for diagram.
        /// P1-01 = bottom left, P1-02 = top left
        /// pi connector P1 pin    = GPIOnum
        ///                  P1-03 = GPIO0
        ///                  P1-05 = GPIO1
        ///                  P1-07 = GPIO4
        ///                  P1-08 = GPIO14 - alt function (UART0_TXD) on boot-up
        ///                  P1-10 = GPIO15 - alt function (UART0_TXD) on boot-up
        ///                  P1-11 = GPIO17
        ///                  P1-12 = GPIO18
        ///                  P1-13 = GPIO21
        ///                  P1-15 = GPIO22
        ///                  P1-16 = GPIO23
        ///                  P1-18 = GPIO24
        ///                  P1-19 = GPIO10
        ///                  P1-21 = GPIO9
        ///                  P1-22 = GPIO25
        ///                  P1-23 = GPIO11
        ///                  P1-24 = GPIO8
        ///                  P1-26 = GPIO7
        ///                  
        ///                  P5-03 = GPI28
        ///                  P5-04 = GPI29
        ///                  P5-05 = GPI30
        ///                  P5-06 = GPI31
        /// 
        /// So to turn on Pin7 on the GPIO connector, pass in enumGPIOPIN.gpio4 as the pin parameter
        /// </remarks>
        public enum Pin : uint
        {
            GPIO_NONE = uint.MaxValue,

            //Revision 1

            GPIO_00 = 8,
            GPIO_01 = 9,
            GPIO_04 = 7,
            GPIO_07 = 11,
            GPIO_08 = 10,
            GPIO_09 = 13,
            GPIO_10 = 12,
            GPIO_11 = 14,
            GPIO_14 = 15,
            GPIO_15 = 16,
            GPIO_17 = 0,
            GPIO_18 = 1,
            GPIO_21 = 2,
            GPIO_22 = 3,
            GPIO_23 = 4,
            GPIO_24 = 5,
            GPIO_25 = 6,

            Pin_P1_03 = 8,
            Pin_P1_05 = 9,
            Pin_P1_07 = 7,
            Pin_P1_08 = 15,
            Pin_P1_10 = 16,
            Pin_P1_11 = 0,
            Pin_P1_12 = 1,
            Pin_P1_13 = 2,
            Pin_P1_15 = 3,
            Pin_P1_16 = 4,
            Pin_P1_18 = 5,
            Pin_P1_19 = 12,
            Pin_P1_21 = 13,
            Pin_P1_22 = 6,
            Pin_P1_23 = 14,
            Pin_P1_24 = 10,
            Pin_P1_26 = 11,
            //LED = 16,

            //Revision 2

            
            V2_GPIO_02 = 8,
            V2_GPIO_03 = 9,
            
            V2_GPIO_04 = 7,
            V2_GPIO_07 = 11,
            V2_GPIO_08 = 10,
            V2_GPIO_09 = 13,
            V2_GPIO_10 = 12,
            V2_GPIO_11 = 14,
            V2_GPIO_14 = 15,
            V2_GPIO_15 = 16,
            V2_GPIO_17 = 0,
            V2_GPIO_18 = 1,
            
            V2_GPIO_22 = 3,
            V2_GPIO_23 = 4,
            V2_GPIO_24 = 5,
            V2_GPIO_25 = 6,
            V2_GPIO_27 = 2,

            //Revision 2, new plug P5
            V2_GPIO_28 = 17,
            V2_GPIO_29 = 18,
            V2_GPIO_30 = 19,
            V2_GPIO_31 = 20,

            V2_Pin_P1_03 = 8,
            V2_Pin_P1_05 = 9,
            V2_Pin_P1_07 = 7,
            V2_Pin_P1_08 = 15,
            V2_Pin_P1_10 = 16,
            V2_Pin_P1_11 = 0,
            V2_Pin_P1_12 = 1,
            V2_Pin_P1_13 = 2,
            V2_Pin_P1_15 = 3,
            V2_Pin_P1_16 = 4,
            V2_Pin_P1_18 = 5,
            V2_Pin_P1_19 = 12,
            V2_Pin_P1_21 = 13,
            V2_Pin_P1_22 = 6,
            V2_Pin_P1_23 = 14,
            V2_Pin_P1_24 = 10,
            V2_Pin_P1_26 = 11,
            //V2_LED = 16,

            //Revision 2, new plug P5
            V2_Pin_P5_03 = 17,
            V2_Pin_P5_04 = 18,
            V2_Pin_P5_05 = 19,
            V2_Pin_P5_06 = 20,
        }

        public enum PWMChannel : int
        {
            PWM_NONE = -1,
            PWM_0 = 0,
            PWM_1 = 1,
            PWM_2 = 2,
            PWM_3 = 3,
            PWM_4 = 4,
            PWM_5 = 5,
            PWM_6 = 6,
            PWM_7 = 7,
        }

        public enum AnalogChannel : int
        {
            ANALOG_NONE = -1,
            ANALOG_0 = 0,
            ANALOG_1 = 1,
            ANALOG_2 = 2,
            ANALOG_3 = 3,
            ANALOG_4 = 4,
            ANALOG_5 = 5,
            ANALOG_6 = 6,
            ANALOG_7 = 7,
        }

        //--//

        extern public static uint SystemClock
        {
            
            get;
        }

        extern public static uint SlowClock
        {
            
            get;
        }

        extern public static TimeSpan GlitchFilterTime
        {
            
            get;

            
            set;
        }
    }
}
