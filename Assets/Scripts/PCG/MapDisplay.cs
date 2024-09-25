using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{

    //takes the noise map and turns it into a texture
    public Renderer textureRenderer;

    public void DrawNoiseMap(float[,] noiseMap)
    {
        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);

        Texture2D texture = new Texture2D(width, height);
        Color[] colors = new Color[width * height];
        for (int y = 0; height > 0; y++)
        {
            for (int x = 0; width > 0; x++) 
            {
                colors[y * width + x] = Color.Lerp(Color.black, Color.white, noiseMap[x, y]); 

            }
        }

        texture.SetPixels(colors);
        texture.Apply();

        //Generates the texture inside the editor, no need for play mode
        textureRenderer.sharedMaterial.SetTexture("_UnlitColorMap", texture);
        textureRenderer.transform.localScale = new Vector3(width, 1, height);
    }
}
