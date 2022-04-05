using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This class is used for generating perlin noise
*/
public static class Noise
{   
    // The perlin noise is composed by a grid of values between 0 and 1
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, float scale)
    {   
        // Create 2D float array to store coordinates
        float[,] noiseMap = new float[mapWidth, mapHeight];

        if(scale <= 0){
            scale = 0.0001f;
        }

        for(int y = 0; y < mapHeight; y++){
            for(int x = 0; x < mapWidth; x++){
                // Avoid get same int value each time
                float sampleX = x / scale;
                float sampleY = y / scale;
                
                // Implement the perlin noise generation algorithem
                float perlinValue = Mathf.PerlinNoise(sampleX, sampleY);
                
                noiseMap[x,y] = perlinValue;
            }
        }

        return noiseMap;
    }
}
