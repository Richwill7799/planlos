using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IcePainter : MonoBehaviour
{
    private const int size = 500;
    
    
    public GameObject player;

    private Material material;
    private Texture2D texture;
    private (int, int) oldpos;
    private byte[] pixels;
    private bool lastcomplete = true;

    void Start()
    {
        texture = new Texture2D(size, size, TextureFormat.Alpha8, false);
        texture.filterMode = FilterMode.Bilinear;
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

        MaterialPropertyBlock b = new MaterialPropertyBlock();
        b.SetTexture(CrackTex, texture);
        GetComponent<MeshRenderer>().SetPropertyBlock(b);
        
        oldpos = GetPixelCoord();
    }

    private (int, int) GetPixelCoord() => ((player.transform.position - transform.position).Div(transform.localScale * 5)).Xy().ToPixel(size / 2, size / 2);
    
    // Update is called once per frame
    void Update()
    {
        (int, int) pc = GetPixelCoord();

        int dx = Math.Sign(pc.Item1 - oldpos.Item1);
        int dy = Math.Sign(pc.Item2 - oldpos.Item2);

        if (dx != 0 || dy != 0)
        {
            bool flood = false;

            int nx = oldpos.Item1 + dx;
            int ny = oldpos.Item2 + dy;

            while (dx != 0 || dy != 0)
            {
                int i = Mathf.Clamp(nx, 0, size) + 
                        Mathf.Clamp(ny, 0, size) * size;
                     
                flood |= pixels[i] > 0 && lastcomplete; 
                lastcomplete = pixels[i] == 0;
                pixels[i] = 255;
                
                dx = Math.Sign(pc.Item1 - nx);
                dy = Math.Sign(pc.Item2 - ny);

                nx += dx;
                ny += dy;
            }

           
            
            if (flood)
            {
                Debug.Log("Start flood");
                Array.Clear(temp, 0, temp.Length);
                fillqueue.Clear();
                for (int x = 0; x < size; x++)
                {
                    for (int y = 0; y < size; y++)
                    {
                        if (x == 0 || y == 0 || x == size - 1 || y == size - 1)
                        {
                            fillqueue.Enqueue((x, y));
                        }
                    }
                }

                while (fillqueue.Count > 0)
                {
                    (int, int) c = fillqueue.Dequeue();
                    int x = c.Item1;
                    int y = c.Item2;
                    if (x < 0 || y < 0 || x >= size || y >= size)
                        continue;
                    if (temp[x + y * size] || pixels[x + y * size] == 255) 
                        continue;
        
                    //Debug.Log($"aiin {x} | {y}");
                    temp[x + y * size] = true;

                    fillqueue.Enqueue((x, y + 1));
                    fillqueue.Enqueue((x + 1, y)); 
                    fillqueue.Enqueue((x, y - 1)); 
                    fillqueue.Enqueue((x - 1, y));                   
                }
                
                for (int j = 0; j < size * size; j++)
                {
                    if (!temp[j])
                        pixels[j] = Math.Max(pixels[j], (byte)50);
                }
            }
            
        }
        
        for (int j = 0; j < size * size; j++)
        {
            if (pixels[j] > 10 && pixels[j] < 250)
                pixels[j] = (byte)Math.Min(pixels[j] + 1, 250);
        }
        
        texture.SetPixelData(pixels, 0);
        texture.Apply();
        
        oldpos = pc;
    }
    
    private bool[] temp = new bool[size * size];
    private Queue<(int, int)> fillqueue = new Queue<(int, int)>(size * 4 * 5);
    private static readonly int CrackTex = Shader.PropertyToID("_CrackTex");
}
