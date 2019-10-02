using System;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using WindowsConsoleEngine;
using Demo;

namespace WindowsConsoleEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            ScreenWriter.Initialize(100, 50);

            var mapWidth = 30;
            var mapHeight = 30;

            Frame mapFrame = new Frame(10, 10, mapWidth, mapHeight, true, ConsoleColor.Red);
            
            Frame statsFrame = new Frame(20,20,30,5,true, ConsoleColor.Red);

            var decoratedMap = GenerateMap(mapWidth*5, mapHeight*5, 0.1f);
           
            ScreenWriter.Refresh();

            var x = 0;

            while (true)
            {
                x -= 1;
                mapFrame.Redraw();
                mapFrame.WriteCharacterArray(x, x, decoratedMap);

                statsFrame.Redraw();
                statsFrame.WriteString(0, 0, "mapWidth = " + mapWidth * 5, ConsoleColor.Green);
                statsFrame.WriteString(0, 1, "mapHeight = " + mapHeight * 5, ConsoleColor.Green);
                statsFrame.WriteString(0, 2, "frameWidth = " + mapWidth , ConsoleColor.Green);
                statsFrame.WriteString(0, 3, "frameHeight = " + mapHeight, ConsoleColor.Green);
                statsFrame.WriteString(0, 4, "currentX = " + x, ConsoleColor.Blue);

                ScreenWriter.Refresh();
            }

        }

        private static DecoratedCharacter[,] GenerateMap(int width, int height, float scale)
        {
            PerlinMapGenerator mapGenerator = new PerlinMapGenerator(width, height, scale);

            var map = mapGenerator.Map;

            var mapDecorated = new DecoratedCharacter[width, height];

            for (int mapY = 0; mapY < height; mapY++)
            {
                for (int mapX = 0; mapX < width; mapX++)
                {
                    if (map[mapX, mapY] == 0)
                        mapDecorated[mapX, mapY] = new DecoratedCharacter('X', ConsoleColor.Blue);
                    else
                        mapDecorated[mapX, mapY] = new DecoratedCharacter('O', ConsoleColor.Red);
                }
            }

            return mapDecorated;
        }
    }
}