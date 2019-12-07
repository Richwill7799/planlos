using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class IcePainter : MonoBehaviour
{
    private const int size = 250;
    
    
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
                int i = Mathf.Clamp(nx, 0, size - 1) + 
                        Mathf.Clamp(ny, 0, size - 1) * size;
                     
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
                    if (temp[x + y * size] || pixels[x + y * size] > 20) 
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
                    if (!temp[j] && pixels[j] == 0)
                        pixels[j] = 255;
                }
            }
            
        }
        
        //for (int j = 0; j < size * size; j++)
        //{
        //   if (pixels[j] > 0)
        //        pixels[j]--;
        //}
        
        
        
        texture.SetPixelData(pixels, 0);
        texture.Apply();
        
        oldpos = pc;
    }

    public void FixedUpdate()
    {
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                if (pixels[x + y * size] > 0 &&
                    (getPixel(x + 1, y) < 220 || getPixel(x, y - 1) < 220 ||
                     getPixel(x - 1, y) < 220 || getPixel(x, y + 1) < 220))
                    pixels[x + y * size] = (byte)Math.Max(0, pixels[x + y * size] - 2);
            }
        }
    }
    
    private byte getPixel(int x, int y)
    {
        if (x < 0 || y < 0 || x >= size || y >= size)
            return 0;
        return pixels[x + y * size];
    }
    
    private bool[] temp = new bool[size * size];
    private Queue<(int, int)> fillqueue = new Queue<(int, int)>(size * 4 * 5);
    private static readonly int CrackTex = Shader.PropertyToID("_CrackTex");
    
    
}
