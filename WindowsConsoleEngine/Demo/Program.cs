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

            var mapWidth = 50;
            var mapHeight = 30;

            Frame mapFrame = new Frame(0, 0, mapWidth, mapHeight, true, ConsoleColor.Blue);
            

            var decoratedMap = GenerateMap(mapWidth, mapHeight, 0.1f);
            mapFrame.WriteCharacterArray(0,0, decoratedMap);

            var rand = new Random();
            Frame randomFrame = new Frame(52, 0, 10, 30, true, ConsoleColor.Red);
            ScreenWriter.Refresh();

            while (true)
            {
                var randomCharArray = new DecoratedCharacter[10, 10];
                for (int y = 0; y < 10; y++)
                {
                    for (int x = 0; x < 10; x++)
                    {
                        int num = rand.Next(0, 26); // Zero to 25
                        char let = (char)('a' + num);
                        randomCharArray[x, y] = new DecoratedCharacter(let, ConsoleColor.Green);
                    }
                }

                decoratedMap = GenerateMap(mapWidth, mapHeight, 0.1f);
                mapFrame.WriteCharacterArray(0, 0, decoratedMap);

                randomFrame.WriteCharacterArray(0, 0, randomCharArray);

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