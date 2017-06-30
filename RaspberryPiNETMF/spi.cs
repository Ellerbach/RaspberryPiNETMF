using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.SPOT.Hardware
{

    public sealed class SPI : IDisposable
    {
        // see http://www.airspayce.com/mikem/bcm2835/group__spi.html#ga600dc972f1064908b41b349c92d7647d
        // P1-19 (MOSI)
        // P1-21 (MISO)
        // P1-23 (CLK)
        // P1-24 (CE0)
        // P1-26 (CE1)
        
        public enum SPI_module : int
        {
            SPI1 = 0,
            SPI2 = 1,
            SPI3 = 2,
            None = 3,
        }

        //#endregion

        #region ImportDllSPI
        //int wiringPiSPIGetFd (int channel)
        [DllImport("libwiringPiSPI.so", EntryPoint = "wiringPiSPIGetFd")]
        static extern int wiringPiSPIGetFd(SPI_module channel);
        //int wiringPiSPIDataRW (int channel, unsigned char *data, int len)
        [DllImport("libwiringPiSPI.so", EntryPoint = "wiringPiSPIDataRW")]
        static extern int wiringPiSPIDataRW(SPI_module channel, [MarshalAs(UnmanagedType.LPArray)]byte[] data, int len);
        //int wiringPiSPISetup (int channel, int speed)
        [DllImport("libwiringPiSPI.so", EntryPoint = "wiringPiSPISetup")]
        static extern int wiringPiSPISetup(SPI_module channel, int speed);
        //int main (int argc, char *argv [])
        [DllImport("libgpio.so", EntryPoint = "main")]
        static extern int main(int argc, [MarshalAs(UnmanagedType.LPArray)]string[] argv);

        #endregion

        #region internal
        SPI.Configuration config;
        
        #endregion

        /// <summary>
        /// Start SPI operations. Forces RPi SPI0 pins P1-19 (MOSI), P1-21 (MISO), 
        /// P1-23 (CLK), P1-24 (CE0) and P1-26 (CE1) to alternate function ALT0, 
        /// which enables those pins for SPI interface. You should call bcm2835_spi_end() 
        /// when all SPI funcitons are complete to return the pins to their default functions 
        /// </summary>
        /// <param name="config"></param>

        public SPI(SPI.Configuration config)
        {
            this.config = config;
			// initialize the io
			string[] arg = { "gpio", "load", "spi" };
			main(3, arg);
            //configure the right speed
            if (wiringPiSPISetup(config.SPI_mod, (int)(config.Clock_RateKHz * 1000)) <0)
                throw new Exception("Unable to initialize bcm2835.so library");           
        }

        public SPI.Configuration Config { get; set; }

        /// <summary>
        /// Supposed to clean something. 
        /// TODO: call the cleaning function to release pins
        /// </summary>
        public void Dispose()
        {

        }
        public void Write(byte[] writeBuffer)
        {
            byte[] bwriteBuffer = new byte[writeBuffer.Length];
            Array.Copy(writeBuffer, bwriteBuffer, writeBuffer.Length);
            int startReadOffset = 0;
            WriteRead(writeBuffer, 0, writeBuffer.Length, bwriteBuffer, 0, writeBuffer.Length, startReadOffset);

        }
        public void Write(ushort[] writeBuffer)
        {
            ushort[] bwriteBuffer = new ushort[writeBuffer.Length];
            Array.Copy(writeBuffer, bwriteBuffer, writeBuffer.Length);
            int startReadOffset = 0;
            WriteRead(writeBuffer, 0, writeBuffer.Length, bwriteBuffer, 0, writeBuffer.Length, startReadOffset);
        }
        public void WriteRead(byte[] writeBuffer, byte[] readBuffer)
        {
            int startReadOffset = 0;
            WriteRead(writeBuffer, 0, writeBuffer.Length, readBuffer, 0, writeBuffer.Length, startReadOffset);
        }
        public void WriteRead(ushort[] writeBuffer, ushort[] readBuffer)
        {
            int startReadOffset=0;
            WriteRead(writeBuffer, 0, writeBuffer.Length, readBuffer, 0, writeBuffer.Length, startReadOffset);
        }
        public void WriteRead(byte[] writeBuffer, byte[] readBuffer, int startReadOffset)
        {
            WriteRead(writeBuffer, 0, writeBuffer.Length, readBuffer, 0, writeBuffer.Length, startReadOffset);
        }
        public void WriteRead(ushort[] writeBuffer, ushort[] readBuffer, int startReadOffset)
        {
            WriteRead(writeBuffer, 0, writeBuffer.Length, readBuffer, 0, writeBuffer.Length, startReadOffset);
        }
        public void WriteRead(byte[] writeBuffer, int writeOffset, int writeCount, byte[] readBuffer, int readOffset, int readCount, int startReadOffset)
        { 
            byte[] bwrite = new byte[writeCount];
            Array.Copy(writeBuffer, writeOffset, bwrite, 0, writeCount);
            wiringPiSPIDataRW(config.SPI_mod,bwrite, writeCount);
            Array.Copy(bwrite, 0, readBuffer, readOffset, readCount);
            startReadOffset = readOffset;
        }
        public void WriteRead(ushort[] writeBuffer, int writeOffset, int writeCount, ushort[] readBuffer, int readOffset, int readCount, int startReadOffset)
        {
            byte[] bwrite = new byte[writeCount * 2];
            Array.Copy(writeBuffer, writeOffset, bwrite, 0, writeBuffer.Length);
            byte[] bread = new byte[readCount * 2];
            wiringPiSPIDataRW(config.SPI_mod, bwrite, writeCount);
            Array.Copy(bwrite, 0, readBuffer, readOffset, readCount * 2);
            startReadOffset = readOffset;
        }
         
        public class Configuration
        {
            public readonly Cpu.Pin BusyPin;
            public readonly bool BusyPin_ActiveState;
            public readonly bool ChipSelect_ActiveState;
            public readonly uint ChipSelect_HoldTime;
            public readonly Cpu.Pin ChipSelect_Port;
            public readonly uint ChipSelect_SetupTime;
            public readonly bool Clock_Edge;
            public readonly bool Clock_IdleState;
            public readonly uint Clock_RateKHz;
            public readonly SPI.SPI_module SPI_mod;

            public Configuration(Cpu.Pin ChipSelect_Port, bool ChipSelect_ActiveState, uint ChipSelect_SetupTime, uint ChipSelect_HoldTime, bool Clock_IdleState, bool Clock_Edge, uint Clock_RateKHz, SPI.SPI_module SPI_mod)
            { 
                this.ChipSelect_Port = ChipSelect_Port;
                this.ChipSelect_ActiveState = ChipSelect_ActiveState;
                this.ChipSelect_SetupTime = ChipSelect_SetupTime;
                this.ChipSelect_HoldTime = ChipSelect_HoldTime;
                this.Clock_IdleState = Clock_IdleState;
                this.Clock_Edge = Clock_Edge;
                this.Clock_RateKHz = Clock_RateKHz;
                this.SPI_mod = SPI_mod;            
            }
            public Configuration(Cpu.Pin ChipSelect_Port, bool ChipSelect_ActiveState, uint ChipSelect_SetupTime, uint ChipSelect_HoldTime, bool Clock_IdleState, bool Clock_Edge, uint Clock_RateKHz, SPI.SPI_module SPI_mod, Cpu.Pin BusyPin, bool BusyPin_ActiveState)
            {
                this.ChipSelect_Port = ChipSelect_Port;
                this.ChipSelect_ActiveState = ChipSelect_ActiveState;
                this.ChipSelect_SetupTime = ChipSelect_SetupTime;
                this.ChipSelect_HoldTime = ChipSelect_HoldTime;
                this.Clock_IdleState = Clock_IdleState;
                this.Clock_Edge = Clock_Edge;
                this.Clock_RateKHz = Clock_RateKHz;
                this.SPI_mod = SPI_mod;
                this.BusyPin = BusyPin;
                this.BusyPin_ActiveState = BusyPin_ActiveState;
            }
        }
    }
}
    
