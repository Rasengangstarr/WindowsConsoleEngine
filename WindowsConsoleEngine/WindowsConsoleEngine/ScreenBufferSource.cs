using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace WindowsConsoleEngine
{
    internal static class ScreenBuffer
    {
        internal static CharInfo[,] ScreenBufferSource { get; set; }

        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern SafeFileHandle CreateFile(
            string fileName,
            [MarshalAs(UnmanagedType.U4)] uint fileAccess,
            [MarshalAs(UnmanagedType.U4)] uint fileShare,
            IntPtr securityAttributes,
            [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
            [MarshalAs(UnmanagedType.U4)] int flags,
            IntPtr template);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern bool WriteConsoleOutput(
            SafeFileHandle hConsoleOutput,
            CharInfo[] lpBuffer,
            Coord dwBufferSize,
            Coord dwBufferCoord,
            ref SmallRect lpWriteRegion);

        [StructLayout(LayoutKind.Sequential)]
        public struct Coord
        {
            public short X;
            public short Y;

            public Coord(short X, short Y)
            {
                this.X = X;
                this.Y = Y;
            }
        };

        [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
        public struct CharUnion
        {
            [FieldOffset(0)] public char UnicodeChar;
            [FieldOffset(0)] public byte AsciiChar;
        }

        [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
        public struct CharInfo
        {
            [FieldOffset(0)] public CharUnion Char;
            [FieldOffset(2)] public short Attributes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SmallRect
        {
            public short Left;
            public short Top;
            public short Right;
            public short Bottom;
        }

        public static void UpdateCharacter(int x, int y, char character, ConsoleColor foregroundColor = ConsoleColor.Empty,
            ConsoleColor backgroundColor = ConsoleColor.Empty)
        {
            switch (foregroundColor)
            {
                case ConsoleColor.Black:
                    ScreenBufferSource[x, y].Attributes = 0;
                    break;
                case ConsoleColor.White:
                    ScreenBufferSource[x, y].Attributes = 7;
                    break;
                case ConsoleColor.Red:
                    ScreenBufferSource[x, y].Attributes = 4;
                    break;
                case ConsoleColor.Green:
                    ScreenBufferSource[x, y].Attributes = 2;
                    break;
                case ConsoleColor.Blue:
                    ScreenBufferSource[x, y].Attributes = 1;
                    break;
            }


            //TODO: implement background color bitshifting

            ScreenBufferSource[x, y].Char.UnicodeChar = character;

        }
    }
}
