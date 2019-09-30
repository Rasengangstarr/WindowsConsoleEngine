using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace WindowsConsoleEngine
{
    public static class ScreenWriter
    {
        private static short _width;
        private static short _height;
        public static void Refresh()
        {
            SafeFileHandle h = ScreenBuffer.CreateFile("CONOUT$", 0x40000000, 2, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);
            ScreenBuffer.SmallRect rect = new ScreenBuffer.SmallRect() { Left = 0, Top = 0, Right = _width, Bottom = _height };
            if (!h.IsInvalid)
            {
                ScreenBuffer.CharInfo[] buf = new ScreenBuffer.CharInfo[ScreenBuffer.ScreenBufferSource.GetLength(1) * ScreenBuffer.ScreenBufferSource.GetLength(0)];
                //Flatten Array
                var flattenCounter = 0;
                for (int y = 0; y < ScreenBuffer.ScreenBufferSource.GetLength(1); y++)
                {
                    for (int x = 0; x < ScreenBuffer.ScreenBufferSource.GetLength(0); x++)
                    {
                     
                        buf[flattenCounter] = ScreenBuffer.ScreenBufferSource[x,y];
                        flattenCounter++;
                    }
                }

                bool b = ScreenBuffer.WriteConsoleOutput(h, buf,
                    new ScreenBuffer.Coord() { X = _width, Y = _height },
                    new ScreenBuffer.Coord() { X = 0, Y = 0 },
                    ref rect);

            }
        }

        public static void Initialize(short width, short height)
        {
            Console.OutputEncoding = Encoding.UTF8;
            ScreenBuffer.ScreenBufferSource = new ScreenBuffer.CharInfo[width,height];
            Console.SetWindowSize(width, height);
            _width = width;
            _height = height;
        }

    }
}
