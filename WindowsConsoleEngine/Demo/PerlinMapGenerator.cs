using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimplexNoise;

namespace Demo
{
    class PerlinMapGenerator
    {
        public float[,] Map;

        public PerlinMapGenerator(int width, int height, float scale)
        {
            var noise = Noise.Calc2D(width, height, scale);
            NormalizeNoiseMap(noise);
        }

        public void NormalizeNoiseMap(float[,] noiseMap)
        {
            Map = new float[noiseMap.GetLength(0), noiseMap.GetLength(1)];

            var largestVal = noiseMap.Cast<float>().Max(e => e);
            var smallestVal = noiseMap.Cast<float>().Min(e => e);

            for (int y = 0; y < noiseMap.GetLength(1); y++)
            {
                for (int x = 0; x < noiseMap.GetLength(0); x++)
                    
                {
                   Map[x, y] = (noiseMap[x, y] - smallestVal) / (largestVal - smallestVal);
                }
            }

        }
        

    }
}
