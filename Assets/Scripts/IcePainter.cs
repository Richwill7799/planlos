using System;
using UnityEngine;

public class IcePainter : MonoBehaviour
{
    private const int size = 100;
    
    
    public GameObject player;

    private Material material;
    private Texture2D texture;
    private (int, int) oldpos;
    private byte[] pixels;

    void Start()
    {
        texture = new Texture2D(size, size, TextureFormat.R8, false);
        texture.filterMode = FilterMode.Point;
        pixels = new byte[size * size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                pixels[i + j * size] = 0;
            }
        }
        texture.SetPixelData(pixels, 0);
        texture.Apply();
        
        material = gameObject.GetComponent<MeshRenderer>().material;
        material.mainTexture = texture;

        oldpos = GetPixelCoord();
    }

    private (int, int) GetPixelCoord() => ((player.transform.position - transform.position).Div(transform.localScale * 5)).Xy().ToPixel(size / 2, size / 2);
    
    // Update is called once per frame
    void Update()
    {
        (int, int) pc = GetPixelCoord();

        int dx = pc.Item1 - oldpos.Item1;
        int dy = pc.Item2 - oldpos.Item2;

        if (dx != 0 || dy != 0)
        {
            if(dx * dx > 1 || dy * dy > 1)
                Debug.Log("Fucking jumps");
            int i = Mathf.Clamp(pc.Item1, 0, size) + Mathf.Clamp(pc.Item2, 0, size) * size;
            if (pixels[i] != 0)
            {
                
                Debug.Log("Start flood");
                Array.Clear(temp, 0, temp.Length);
                if (fill(size / 2, size / 2))
                {
                    for (int j = 0; j < size * size; j++)
                    {
                        if (temp[j])
                            pixels[j] = 255;
                    }
                }
            }
            pixels[i] = 255;
            texture.SetPixelData(pixels, 0);
            texture.Apply();
        }
        oldpos = pc;
    }
    
    private bool[] temp = new bool[size * size];

    private bool fill(int x, int y)
    {
        
        //Debug.Log($"Testin {x} | {y}");
        if (x < 0 || y < 0 || x >= size || y >= size)
            return false;
        if (temp[x + y * size] || pixels[x + y * size] != 0) 
            return true;
        
        //Debug.Log($"aiin {x} | {y}");
        temp[x + y * size] = true;

        return  fill(x, y + 1) &&
                fill(x, y - 1) && 
                fill(x - 1, y) && 
                fill(x + 1, y);
    }
    
    
}
