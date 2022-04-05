using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseMapGenerator : MonoBehaviour
{   
    public int mapWidth;
    public int mapHeight;
    public float noiseScale;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            GenerateMap();
        }
    }

    // Fetching the 2D noise map from the noise class
    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, noiseScale);

        MapDisplay display = FindObjectOfType<MapDisplay>();
        display.DrawNoiseMap (noiseMap);
    }
}
