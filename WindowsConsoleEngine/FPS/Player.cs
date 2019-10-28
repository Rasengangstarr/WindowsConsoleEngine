using System;
using System.Collections.Generic;
using System.Text;
using WindowsConsoleEngine;
using ConsoleColor = WindowsConsoleEngine.ConsoleColor;

namespace FPS
{
    public class Player
    {
        public float posX { get; set; }
        public float posY { get; set; }

        public float viewingAngle { get; set; }

        public void Initialize(char[,] map)
        {
            for (var y = 0; y < map.GetLength(1); y++)
            {
                for (var x = 0; x < map.GetLength(0); x++)
                {
                    if (map[x, y] == 'P')
                    {
                        posX = x;
                        posY = y;
                        return;
                    }
                }
            }
        }

        public void MoveForward(float speed)
        {
            posX += speed * (float)Math.Cos(DegreeToRadian(viewingAngle));
            posY += speed * (float)Math.Sin(DegreeToRadian(viewingAngle));
        }
        public void Turn(float degrees)
        {
            viewingAngle += degrees;
        }

        private double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        public DecoratedCharacter[,] GeneratePlayerView(char[,] map, int viewWidth, int viewHeight)
        {
            DecoratedCharacter[,] projection = new DecoratedCharacter[viewWidth,viewHeight];
            string shadingCharacters = ".:-=+*#%@";

            //CastRays
            for (double rayAngle = viewingAngle-50; rayAngle < viewingAngle+50; rayAngle++)
            {
                double rayX = posX;
                double rayY = posY;
                for (int rayStep = 1; rayStep < 500; rayStep++)
                {
                    //Move forward in steps of 0.01
                    rayX += 0.1f * (double)Math.Cos(DegreeToRadian(rayAngle));
                    rayY += 0.1f * (double)Math.Sin(DegreeToRadian(rayAngle));

                    if (Math.Floor(rayX) >= map.GetLength(0) || Math.Floor(rayX) <= 0 || Math.Floor(rayY) >= map.GetLength(1) || Math.Floor(rayY) <= 0)
                    {
                        break;
                    }
                    if (map[(int)Math.Floor(rayX), (int)Math.Floor(rayY)] != ' ')
                    {

                        double wallHeight = 50;
                        double dist = Math.Sqrt(Math.Pow(rayX-posX, 2) + Math.Pow(rayY - posY, 2)) * Math.Cos(DegreeToRadian(viewingAngle - rayAngle));
                        //Calculate height of line to draw on screen
                        double lineHeight = (wallHeight / dist);

                        //calculate lowest and highest pixel to fill in current stripe
                        double drawStart = -lineHeight / 2 + wallHeight / 2;
                        if (drawStart < 0) drawStart = 0;
                        double drawEnd = lineHeight / 2 + wallHeight / 2;
                        if (drawEnd >= wallHeight) drawEnd = wallHeight - 1;

                        var charToDraw = Math.Abs(drawEnd-drawStart) < shadingCharacters.Length ? shadingCharacters[(int) Math.Floor(Math.Abs(drawEnd - drawStart))] : '$';

                        WindowsConsoleEngine.ConsoleColor colToDraw;

                        colToDraw = ConsoleColor.Black;

                        switch (map[(int) Math.Floor(rayX), (int) Math.Floor(rayY)])
                        {
                            case 'x':
                                colToDraw = ConsoleColor.Red;
                                break;
                            case 'y':
                                colToDraw = ConsoleColor.Blue;
                                break;
                            case 'z':
                                colToDraw = ConsoleColor.Yellow;
                                break;
                        }

                        for (int i = (int)Math.Floor(drawStart); i <= Math.Floor(drawEnd); i++)
                        {
                            projection[(int)Math.Floor(rayAngle+50-viewingAngle), i] = new DecoratedCharacter(charToDraw, colToDraw, WindowsConsoleEngine.ConsoleColor.Black);
                        }

                        break;
                    }
                }
            }
            return projection;
        }
    }
}
