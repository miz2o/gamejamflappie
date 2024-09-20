using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise 
{
    public static float[,] GenerateNoiseMap(int mapWith, int mapHeight, float scale)
    {
        float[,]noiseMap = new float[mapWith, mapHeight];
        if(scale <= 0)
        {
            scale = 0.0001f;

        }

        return noiseMap;

    }
}
