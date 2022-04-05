using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    Take the noise map and turn it into a texture
*/
public class MapDisplay : MonoBehaviour
{   
    public Renderer textureRender;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DrawNoiseMap(float[,] noiseMap)
    {   
        // Get the height and width of the noise map
        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);

        Texture2D texture = new Texture2D(width, height);

        // Set color to the texture 
        Color[] colourMap = new Color[width * height];
        for (int x = 0; x < width; x++){
            for (int y = 0; y < height; y++){
                
                colourMap[y * width + x] = Color.Lerp(Color.black, Color.white, noiseMap[x,y]);
            }
        }

        texture.SetPixels(colourMap);
        texture.Apply();

        // Show the texture without entering the Game mode
        textureRender.sharedMaterial.mainTexture = texture;

        // Set the size of the plane to the same size as the map
        textureRender.transform.localScale = new Vector3(width, 1, height);
    }

}
