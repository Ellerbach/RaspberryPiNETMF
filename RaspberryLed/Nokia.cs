using Microsoft.SPOT.Hardware;
using System;

namespace Nokia.LCDScreens
{
    public static class NokiaCharacters
    {
        public static readonly byte[][] ASCII = new byte[][]
                                           {
                                                new byte[]{0x00, 0x00, 0x00, 0x00, 0x00}    // 20  
                                               , new byte[]{0x00, 0x00, 0x5f, 0x00, 0x00} // 21 !
                                               , new byte[]{0x00, 0x07, 0x00, 0x07, 0x00} // 22 "
                                               , new byte[]{0x14, 0x7f, 0x14, 0x7f, 0x14} // 23 #
                                               , new byte[]{0x24, 0x2a, 0x7f, 0x2a, 0x12} // 24 $
                                               , new byte[]{0x23, 0x13, 0x08, 0x64, 0x62} // 25 %
                                               , new byte[]{0x36, 0x49, 0x55, 0x22, 0x50} // 26 &
                                               , new byte[]{0x00, 0x05, 0x03, 0x00, 0x00} // 27 '
                                               , new byte[]{0x00, 0x1c, 0x22, 0x41, 0x00} // 28 (
                                               , new byte[]{0x00, 0x41, 0x22, 0x1c, 0x00} // 29 )
                                               , new byte[]{0x14, 0x08, 0x3e, 0x08, 0x14} // 2a *
                                               , new byte[]{0x08, 0x08, 0x3e, 0x08, 0x08} // 2b +
                                               , new byte[]{0x00, 0x50, 0x30, 0x00, 0x00} // 2c ,
                                               , new byte[]{0x08, 0x08, 0x08, 0x08, 0x08} // 2d -
                                               , new byte[]{0x00, 0x60, 0x60, 0x00, 0x00} // 2e .
                                               , new byte[]{0x20, 0x10, 0x08, 0x04, 0x02} // 2f /
                                               , new byte[]{0x3e, 0x51, 0x49, 0x45, 0x3e} // 30 0
                                               , new byte[]{0x00, 0x42, 0x7f, 0x40, 0x00} // 31 1
                                               , new byte[]{0x42, 0x61, 0x51, 0x49, 0x46} // 32 2
                                               , new byte[]{0x21, 0x41, 0x45, 0x4b, 0x31} // 33 3
                                               , new byte[]{0x18, 0x14, 0x12, 0x7f, 0x10} // 34 4
                                               , new byte[]{0x27, 0x45, 0x45, 0x45, 0x39} // 35 5
                                               , new byte[]{0x3c, 0x4a, 0x49, 0x49, 0x30} // 36 6
                                               , new byte[]{0x01, 0x71, 0x09, 0x05, 0x03} // 37 7
                                               , new byte[]{0x36, 0x49, 0x49, 0x49, 0x36} // 38 8
                                               , new byte[]{0x06, 0x49, 0x49, 0x29, 0x1e} // 39 9
                                               , new byte[]{0x00, 0x36, 0x36, 0x00, 0x00} // 3a :
                                               , new byte[]{0x00, 0x56, 0x36, 0x00, 0x00} // 3b ;
                                               , new byte[]{0x08, 0x14, 0x22, 0x41, 0x00} // 3c <
                                               , new byte[]{0x14, 0x14, 0x14, 0x14, 0x14} // 3d =
                                               , new byte[]{0x00, 0x41, 0x22, 0x14, 0x08} // 3e >
                                               , new byte[]{0x02, 0x01, 0x51, 0x09, 0x06} // 3f ?
                                               , new byte[]{0x32, 0x49, 0x79, 0x41, 0x3e} // 40 @
                                               , new byte[]{0x7e, 0x11, 0x11, 0x11, 0x7e} // 41 A
                                               , new byte[]{0x7f, 0x49, 0x49, 0x49, 0x36} // 42 B
                                               , new byte[]{0x3e, 0x41, 0x41, 0x41, 0x22} // 43 C
                                               , new byte[]{0x7f, 0x41, 0x41, 0x22, 0x1c} // 44 D
                                               , new byte[]{0x7f, 0x49, 0x49, 0x49, 0x41} // 45 E
                                               , new byte[]{0x7f, 0x09, 0x09, 0x09, 0x01} // 46 F
                                               , new byte[]{0x3e, 0x41, 0x49, 0x49, 0x7a} // 47 G
                                               , new byte[]{0x7f, 0x08, 0x08, 0x08, 0x7f} // 48 H
                                               , new byte[]{0x00, 0x41, 0x7f, 0x41, 0x00} // 49 I
                                               , new byte[]{0x20, 0x40, 0x41, 0x3f, 0x01} // 4a J
                                               , new byte[]{0x7f, 0x08, 0x14, 0x22, 0x41} // 4b K
                                               , new byte[]{0x7f, 0x40, 0x40, 0x40, 0x40} // 4c L
                                               , new byte[]{0x7f, 0x02, 0x0c, 0x02, 0x7f} // 4d M
                                               , new byte[]{0x7f, 0x04, 0x08, 0x10, 0x7f} // 4e N
                                               , new byte[]{0x3e, 0x41, 0x41, 0x41, 0x3e} // 4f O
                                               , new byte[]{0x7f, 0x09, 0x09, 0x09, 0x06} // 50 P
                                               , new byte[]{0x3e, 0x41, 0x51, 0x21, 0x5e} // 51 Q
                                               , new byte[]{0x7f, 0x09, 0x19, 0x29, 0x46} // 52 R
                                               , new byte[]{0x46, 0x49, 0x49, 0x49, 0x31} // 53 S
                                               , new byte[]{0x01, 0x01, 0x7f, 0x01, 0x01} // 54 T
                                               , new byte[]{0x3f, 0x40, 0x40, 0x40, 0x3f} // 55 U
                                               , new byte[]{0x1f, 0x20, 0x40, 0x20, 0x1f} // 56 V
                                               , new byte[]{0x3f, 0x40, 0x38, 0x40, 0x3f} // 57 W
                                               , new byte[]{0x63, 0x14, 0x08, 0x14, 0x63} // 58 X
                                               , new byte[]{0x07, 0x08, 0x70, 0x08, 0x07} // 59 Y
                                               , new byte[]{0x61, 0x51, 0x49, 0x45, 0x43} // 5a Z
                                               , new byte[]{0x00, 0x7f, 0x41, 0x41, 0x00} // 5b [
                                               , new byte[]{0x02, 0x04, 0x08, 0x10, 0x20} // 5c ¥
                                               , new byte[]{0x00, 0x41, 0x41, 0x7f, 0x00} // 5d ]
                                               , new byte[]{0x04, 0x02, 0x01, 0x02, 0x04} // 5e ^
                                               , new byte[]{0x40, 0x40, 0x40, 0x40, 0x40} // 5f _
                                               , new byte[]{0x00, 0x01, 0x02, 0x04, 0x00} // 60 `
                                               , new byte[]{0x20, 0x54, 0x54, 0x54, 0x78} // 61 a
                                               , new byte[]{0x7f, 0x48, 0x44, 0x44, 0x38} // 62 b
                                               , new byte[]{0x38, 0x44, 0x44, 0x44, 0x20} // 63 c
                                               , new byte[]{0x38, 0x44, 0x44, 0x48, 0x7f} // 64 d
                                               , new byte[]{0x38, 0x54, 0x54, 0x54, 0x18} // 65 e
                                               , new byte[]{0x08, 0x7e, 0x09, 0x01, 0x02} // 66 f
                                               , new byte[]{0x0c, 0x52, 0x52, 0x52, 0x3e} // 67 g
                                               , new byte[]{0x7f, 0x08, 0x04, 0x04, 0x78} // 68 h
                                               , new byte[]{0x00, 0x44, 0x7d, 0x40, 0x00} // 69 i
                                               , new byte[]{0x20, 0x40, 0x44, 0x3d, 0x00} // 6a j 
                                               , new byte[]{0x7f, 0x10, 0x28, 0x44, 0x00} // 6b k
                                               , new byte[]{0x00, 0x41, 0x7f, 0x40, 0x00} // 6c l
                                               , new byte[]{0x7c, 0x04, 0x18, 0x04, 0x78} // 6d m
                                               , new byte[]{0x7c, 0x08, 0x04, 0x04, 0x78} // 6e n
                                               , new byte[]{0x38, 0x44, 0x44, 0x44, 0x38} // 6f o
                                               , new byte[]{0x7c, 0x14, 0x14, 0x14, 0x08} // 70 p
                                               , new byte[]{0x08, 0x14, 0x14, 0x18, 0x7c} // 71 q
                                               , new byte[]{0x7c, 0x08, 0x04, 0x04, 0x08} // 72 r
                                               , new byte[]{0x48, 0x54, 0x54, 0x54, 0x20} // 73 s
                                               , new byte[]{0x04, 0x3f, 0x44, 0x40, 0x20} // 74 t
                                               , new byte[]{0x3c, 0x40, 0x40, 0x20, 0x7c} // 75 u
                                               , new byte[]{0x1c, 0x20, 0x40, 0x20, 0x1c} // 76 v
                                               , new byte[]{0x3c, 0x40, 0x30, 0x40, 0x3c} // 77 w
                                               , new byte[]{0x44, 0x28, 0x10, 0x28, 0x44} // 78 x
                                               , new byte[]{0x0c, 0x50, 0x50, 0x50, 0x3c} // 79 y
                                               , new byte[]{0x44, 0x64, 0x54, 0x4c, 0x44} // 7a z
                                               , new byte[]{0x00, 0x08, 0x36, 0x41, 0x00} // 7b 
                                               , new byte[]{0x00, 0x00, 0x7f, 0x00, 0x00} // 7c |
                                               , new byte[]{0x00, 0x41, 0x36, 0x08, 0x00} // 7d 
                                               , new byte[]{0x10, 0x08, 0x08, 0x10, 0x08} // 7e ←
                                               , new byte[]{0x78, 0x46, 0x41, 0x46, 0x78} // 7f →
                                           };
    }

    public class Nokia_5110
    {
        //private OutputPort reset, dataMode;
        private OutputPort reset, dataMode;
        private PWM backlight = null;
        private SPI spi;

        public byte[] ByteMap = new byte[504];

        private uint _backlightVal = 0;
        private bool _invd = false;

        public Nokia_5110(bool useBacklight, Cpu.Pin latch , Cpu.Pin backlight ,
            Cpu.Pin reset , Cpu.Pin dataCommand )
        {
            if (useBacklight)
                this.backlight = new PWM(backlight);

            SPI.Configuration spiConfiguration = new SPI.Configuration(
                latch,            // chip select port
                false,                 // IC is accessed when chip select is low
                0,                     // setup time 1 ms
                0,                     // hold chip select 1 ms after transfer
                false,                 // clock line is low if device is not selected
                true,                  // data is sampled at leading edge of clock
                4000,                 // clockrate is 15 MHz
                SPI.SPI_module.SPI1   // use first SPI bus
                );

            spi = new SPI(spiConfiguration);

            this.reset = new OutputPort(reset, true);
            this.dataMode = new OutputPort(dataCommand, true);
            Initialize();
        }
        
        private void Initialize()
        {
            reset.Write(false);
            reset.Write(true);

            dataMode.Write(false);
            spi.Write(new byte[] { 0x21, 0xBF, 0x04, 0x14, 0x0C, 0x20, 0x0C });
            dataMode.Write(true);

            Clear();
            Refresh();
        }

        public void WriteText(string text, bool noSpace = true)
        {
            if (noSpace)
                foreach (char c in text.ToCharArray())
                    spi.Write(NokiaCharacters.ASCII[c - 0x20]);
            else
            {
                byte[] letter = new byte[6];
                foreach (char c in text.ToCharArray())
                {
                    for (int i = 0; i < 5; i++)
                        letter[i] = NokiaCharacters.ASCII[c - 0x20][i];

                    letter[5] = 0x00;
                    spi.Write(letter);
                }

            }
        }

        public bool DrawPoint(short x, short y, bool on)
        {
            if (x < 0 || x >= 84 || y < 0 || y >= 48)
                return true; // out of the range! return true to indicate failure.

            ushort index = (ushort)((x % 84) + (int)(y * 0.125) * 84);
            
            byte bitMask = (byte)(1 << (y % 8));
            
            if (on)
                ByteMap[index] |= bitMask;
            else
                ByteMap[index] &= (byte)~bitMask;

            return false; // all is good (false = no error), return true to continue
        }

        public void DrawLine(short x1, short y1, short x2, short y2, bool on)
        {
            // This is a common line drawing algorithm. Read about it here:
            // http://en.wikipedia.org/wiki/Bresenham's_line_algorithm

            short sx = (x1 < x2) ? sx = 1 : sx = -1;
            short sy = (y1 < y2) ? sy = 1 : sy = -1;

            short dx = (short)((x2 > x1) ? x2 - x1 : x1 - x2);
            short dy = (short)((y2 > x1) ? y2 - y1 : y1 - y2);

            float err = dx - dy, e2;

            // if there is an error with drawing a point or the line is finished get out of the loop!
            while (!((x1 == x2 && y1 == y2) || DrawPoint(x1, y1, on)))
            {
                e2 = 2 * err;
                
                if (e2 > -dy)
                {
                    err -= dy;
                    x1 += sx;
                }

                if (e2 < dx)
                {
                    err += dx;
                    y1 += sy;
                }
            }
        }

        public void DrawRectangle(short X, short Y, short width, short height, bool on, bool filled)
        {
            // this is easier to do with points instead of lines since the line algorithm isn't that great.
            // this is only best to do with points because its straight lines. 

            short xe = (short)(X + width);
            short ye = (short)(Y + height);

            if (filled)
                for (short y = Y; y != ye; y++)
                    for (short x = X; x != xe; x++)
                        DrawPoint(x, y, on);
            else
            {
                xe -= 1;
                ye -= 1;

                for (short x = X; x != xe; x++)
                    DrawPoint(x, Y, on);

                for (short x = X; x <= xe; x++)
                    DrawPoint(x, ye, on);

                for (short y = Y; y != ye; y++)
                    DrawPoint(X, y, on);

                for (short y = Y; y <= ye; y++)
                    DrawPoint(xe, y, on);
            }
        }

        public void Refresh()
        {
             spi.Write(ByteMap);   
        }

        public void Clear()
        {
            ByteMap = new byte[504];
            dataMode.Write(false);
            spi.Write(new byte[]{ 0x80, 0x40});
            dataMode.Write(true);
        }

        private void Inverse(bool inverse)
        {
            _invd = inverse;
            dataMode.Write(false);
            spi.Write(inverse ? new byte[] { 0x0D } : new byte[] { 0x0C });
            dataMode.Write(true);
        }

        public bool InverseColors
        {
            get { return _invd; }
            set { Inverse(value); }
        }

        public uint BacklightBrightness
        {
            get
            { return _backlightVal; }
            set
            {
                if (backlight != null)
                {
                    if (value > 100)
                        value = 100;

                    backlight.SetDutyCycle(value);
                    _backlightVal = value;
                }
            }
        }
    }
}
