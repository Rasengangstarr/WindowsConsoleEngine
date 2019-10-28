using System;
using System.Collections.Generic;
using System.Text;

namespace FPS
{
    public static class MapLoader
    {
        public static char[,] LoadMap(string path)
        {
            string[] lines = System.IO.File.ReadAllLines(path);

            char[,] chars = new char[lines[0].Length,lines.Length];

            var lineNum = 0;
            var charNum = 0;

            foreach (var line in lines)
            {
                foreach (char character in line)
                {
                    chars[charNum, lineNum] = character;
                    charNum++;
                }
                lineNum += 1;
                charNum = 0;
            }

            return chars;
        }
    }
}
