using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace WindowsConsoleEngine
{
    public class Frame
    {
        private int _x;
        private int _y;
        private int _x2;
        private int _y2;
        private int _width;
        private int _height;
        private bool _hasBorder;
        private ConsoleColor _borderColor;

        public Frame(int x, int y, int width, int height, bool hasBorder = false, ConsoleColor borderColor = ConsoleColor.White)
        {
            if (x+width >= ScreenBuffer.ScreenBufferSource.GetLength(0) || y + height >= ScreenBuffer.ScreenBufferSource.GetLength(1))
                throw new Exception("Attempted to create a frame too large for the screen area. frame dimension maximum is screen dimension - 2");

            _x = x;
            _y = y;
            _width = width;
            _height = height;
            _hasBorder = hasBorder;
            _borderColor = borderColor;

            _x2 = _x + _width;
            _y2 = _y + _height;

            WipeContents();

            if (_hasBorder)
            {
                DrawBorder();
            }
            
            _x = x + 1;
            _y = y + 1;
            _x2 = _x2 - 1;
            _y2 = _y2 - 1;
            

        }

        public void Redraw()
        {
            _x--;
            _y--;
            _x2++;
            _y2++;
            WipeContents();
            if (_hasBorder)
            {
                DrawBorder();
            }
            _x++;
            _y++;
            _x2--;
            _y2--;
        }

        public bool WriteCharacter(int x, int y, DecoratedCharacter character)
        {
            if (x > _width || y > _height)
            {
                return false;
            }

            ScreenBuffer.UpdateCharacter(_x + x, _y + y, character.Character, character.ForegroundColor);
            
            return true;
        }

        public bool WriteCharacterArray(int x, int y, DecoratedCharacter[,] decoratedCharacters)
        {
           
            for (int yCount = 0; yCount < decoratedCharacters.GetLength(1); yCount++)
            {
                for (int xCount = 0; xCount < decoratedCharacters.GetLength(0); xCount++)
                {
                    if (xCount + _x < _x || xCount + _x > _x2 + 1 || yCount + _y < _y || yCount + _y > _y2 + 1 
                        || xCount + x + _x < _x || yCount + y + _y + 1 < _y)
                    {
                        continue;
                    }

                    try
                    {
                        ScreenBuffer.UpdateCharacter(xCount + _x, yCount + _y,
                            decoratedCharacters[xCount + x, yCount + y].Character,
                            decoratedCharacters[xCount + x, yCount + y].ForegroundColor);
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void DrawBorder()
        {
           //Draw Corners
            ScreenBuffer.UpdateCharacter(_x, _y, '\u2554', _borderColor);
            ScreenBuffer.UpdateCharacter(_x2 + 1, _y, '\u2557', _borderColor);
            ScreenBuffer.UpdateCharacter(_x2 + 1, _y2 + 1, '\u255D', _borderColor);
            ScreenBuffer.UpdateCharacter(_x, _y2 + 1, '\u255A', _borderColor);

            //Draw Lines
            for (int top = _x+1; top <= _x2; top++)
            {
                ScreenBuffer.UpdateCharacter(top, _y, '-', _borderColor);
            }
            for (int bottom = _x+1; bottom <= _x2; bottom++)
            {
                ScreenBuffer.UpdateCharacter(bottom, _y2+1, '-', _borderColor);
            }
            for (int left = _y+1; left <= _y2; left++)
            {
                ScreenBuffer.UpdateCharacter(_x, left, '|', _borderColor);
            }
            for (int right = _y+1; right <= _y2; right++)
            {
                ScreenBuffer.UpdateCharacter(_x2+1, right, '|', _borderColor);
            }

        }

        private void WipeContents()
        {
            for (int y = _y + 1; y <= _y2; y++)
            {
                for (int x = _x + 1; x <= _x2; x++)
                {
                    ScreenBuffer.UpdateCharacter(x, y, ' ', ConsoleColor.Black);
                }
            }
        }
    }
}
