using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsConsoleEngine
{
    public class DecoratedCharacter
    {
        public DecoratedCharacter(char character, ConsoleColor foregroundColor, ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            Character = character;
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
        }

        public char Character { get; set; }
        public ConsoleColor ForegroundColor { get; set; }
        public ConsoleColor BackgroundColor { get; set; }
    }
}
