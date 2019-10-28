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
            ScreenWriter.Initialize(110, 52);

            var mapWidth = 50;
            var mapHeight = 50;

            Frame mapFrame = new Frame(0, 0, mapWidth, mapHeight, true, ConsoleColor.Red);
            
            Frame statsFrame = new Frame(52,12,30,5,true, ConsoleColor.Red);

            Frame miniMapFrame = new Frame(52, 0, 10, 10, true, ConsoleColor.Red);

            var decoratedMap = GenerateMap(mapWidth * 5, mapHeight * 5, 0.01f );
            var decoratedMiniMap = GenerateMap(10, 10, 0.01f * (mapWidth * 5f) / 10);

            ScreenWriter.Refresh();

            var x = 0;
            var y = 0;

            while (true)
            {
                var input = Console.KeyAvailable ? Console.ReadKey() : new ConsoleKeyInfo();

                if (input.Key == ConsoleKey.DownArrow && y > -mapHeight * 4)
                {
                    y -= 1;
                }
                else if (input.Key == ConsoleKey.UpArrow && y <= 0)
                {
                    y += 1;
                }
                else if (input.Key == ConsoleKey.LeftArrow && x < mapWidth * 5)
                {
                    x += 1;
                }
                else if (input.Key == ConsoleKey.RightArrow && x > 0)
                {
                    x -= 1;
                }

                mapFrame.Redraw();
                mapFrame.WriteCharacterArray(x, y, decoratedMap);

                statsFrame.Redraw();
                statsFrame.WriteString(0, 0, "mapWidth = " + mapWidth * 5, ConsoleColor.White);
                statsFrame.WriteString(0, 1, "mapHeight = " + mapHeight * 5, ConsoleColor.Grey);
                statsFrame.WriteString(0, 2, "frameWidth = " + mapWidth, ConsoleColor.Cyan);
                statsFrame.WriteString(0, 3, "frameHeight = " + mapHeight, ConsoleColor.Blue);
                statsFrame.WriteString(0, 4, "currentX = " + x, ConsoleColor.Red);

                miniMapFrame.WriteCharacterArray(0, 0, decoratedMiniMap);
                miniMapFrame.WriteCharacter((int)Math.Floor(((float)(-x+(mapWidth/2f)) / (float)mapWidth) * 2) - 1,
                    (int)Math.Floor(((float)(-y + (mapHeight / 2f)) / (float)mapHeight) * 2) -1,
                    new DecoratedCharacter(' ', ConsoleColor.Black, ConsoleColor.White));

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
                    if (map[mapX, mapY] < 0.2)
                        mapDecorated[mapX, mapY] = new DecoratedCharacter(' ', ConsoleColor.Black, ConsoleColor.DarkCyan);
                    else if (map[mapX, mapY] < 0.3)
                        mapDecorated[mapX, mapY] = new DecoratedCharacter(' ', ConsoleColor.Black, ConsoleColor.Cyan);
                    else if (map[mapX, mapY] < 0.35)
                        mapDecorated[mapX, mapY] = new DecoratedCharacter(' ', ConsoleColor.Black, ConsoleColor.DarkYellow);
                    else if (map[mapX, mapY] < 0.8)
                        mapDecorated[mapX, mapY] = new DecoratedCharacter(' ', ConsoleColor.Black, ConsoleColor.Green);
                    else if (map[mapX, mapY] < 0.8)
                        mapDecorated[mapX, mapY] = new DecoratedCharacter(' ', ConsoleColor.Black, ConsoleColor.DarkGreen);
                    else 
                        mapDecorated[mapX, mapY] = new DecoratedCharacter(' ', ConsoleColor.Black, ConsoleColor.Grey);

                }
            }

            return mapDecorated;
        }
    }
}