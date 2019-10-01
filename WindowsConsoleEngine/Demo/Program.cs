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
            
            Frame robinFrame = new Frame(20,20,10,10,true, ConsoleColor.Red);

            var decoratedMap = GenerateMap(mapWidth*5, mapHeight*5, 0.1f);
           
            ScreenWriter.Refresh();

            var x = 100;

            while (true)
            {
                x -= 1;
                mapFrame.Redraw();
                mapFrame.WriteCharacterArray(x, x, decoratedMap);

                robinFrame.Redraw();
                robinFrame.WriteCharacter(5, 5, new DecoratedCharacter('R', ConsoleColor.Red));

                ScreenWriter.Refresh();
                Thread.Sleep(100);
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