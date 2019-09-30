using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimplexNoise;

namespace Demo
{
    class PerlinMapGenerator
    {
        public int[,] Map;

        public PerlinMapGenerator(int width, int height, float scale)
        {
            var noise = Noise.Calc2D(width, height, scale);
            NormalizeNoiseMap(noise);
        }

        public void NormalizeNoiseMap(float[,] noiseMap)
        {
            Map = new int[noiseMap.GetLength(0), noiseMap.GetLength(1)];

            var largestVal = noiseMap.Cast<float>().Max(e => e);
            var smallestVal = noiseMap.Cast<float>().Min(e => e);

            for (int y = 0; y < noiseMap.GetLength(1); y++)
            {
                for (int x = 0; x < noiseMap.GetLength(0); x++)
                    
                {
                    var normalizedValue = (noiseMap[x, y] - smallestVal) / (largestVal - smallestVal);
                    if (normalizedValue <= 0.5)
                    {
                        //Water
                        Map[x, y] = 0;
                    } else
                    {
                        //Grass
                        Map[x, y] = 1;
                    } 
                }
            }

        }
        

    }
}
