using System;
using WindowsConsoleEngine;

namespace FPS
{
    class Program
    {
        static void Main(string[] args)
        {
            ScreenWriter.Initialize(110, 52);

            var map = MapLoader.LoadMap("Map01.txt");
            var player = new Player();
            player.Initialize(map);

            var projectionFrame = new Frame(0, 0, 100, 50, true, WindowsConsoleEngine.ConsoleColor.Cyan);

            //Controls
            while (true)
            {
                var input = Console.KeyAvailable ? Console.ReadKey() : new ConsoleKeyInfo();

                if (input.Key == ConsoleKey.DownArrow)
                    player.MoveForward(-0.1f);
                else if (input.Key == ConsoleKey.UpArrow)
                    player.MoveForward(0.1f);
                else if (input.Key == ConsoleKey.LeftArrow)
                    player.Turn(-3f);
                else if (input.Key == ConsoleKey.RightArrow)
                    player.Turn(3f);

                var projection = player.GeneratePlayerView(map, 100, 50);
                projectionFrame.WriteCharacterArray(0, 0, projection);
                projectionFrame.WriteString(1,1,"PlayerX = " + player.posX, WindowsConsoleEngine.ConsoleColor.Cyan);
                projectionFrame.WriteString(1, 2, "PlayerY = " + player.posY, WindowsConsoleEngine.ConsoleColor.Cyan);
                projectionFrame.WriteString(1, 3, "PlayerA = " + player.viewingAngle, WindowsConsoleEngine.ConsoleColor.Cyan);
                ScreenWriter.Refresh();
            }

        }


      
    }

    
}
