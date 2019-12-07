using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class IcePainter : MonoBehaviour
{
    private const int sizex = 640;
    private const int sizey = 400;
    
    
    public MovementScript player;

    private Material material;
    private Texture2D texture;
    private (int, int) oldpos;
    private byte[] pixels;
    private bool lastcomplete = true;

    void Start()
    {
        texture = new Texture2D(sizex, sizey, TextureFormat.Alpha8, false);
        texture.filterMode = FilterMode.Bilinear;
        pixels = new byte[sizex * sizey];
        for (int i = 0; i < sizex; i++)
        {
            for (int j = 0; j < sizey; j++)
            {
                pixels[i + j * sizex] = 0;
            }
        }
        texture.SetPixelData(pixels, 0);
        texture.Apply();

        MaterialPropertyBlock b = new MaterialPropertyBlock();
        b.SetTexture(CrackTex, texture);
        GetComponent<MeshRenderer>().SetPropertyBlock(b);
        
        oldpos = GetPixelCoord();
    }

    private (int, int) GetPixelCoord() => ((player.GetPosition() - transform.position).Div(transform.localScale * 5)).Xy().ToPixel(sizex / 2, sizey / 2);
    
    // Update is called once per frame
    void Update()
    {
        (int, int) pc = GetPixelCoord();


        if (!player.IsAir() &&
            pixels[(pc.Item1 + 1) + (pc.Item2 + 1) * sizex] > 10 &&
            pixels[(pc.Item1 - 1) + (pc.Item2 + 1) * sizex] > 10 &&
            pixels[(pc.Item1 + 1) + (pc.Item2 - 1) * sizex] > 10 &&
            pixels[(pc.Item1 - 1) + (pc.Item2 - 1) * sizex] > 10)
        {
            player.Death();
        }
        
        int dx = Math.Sign(pc.Item1 - oldpos.Item1);
        int dy = Math.Sign(pc.Item2 - oldpos.Item2);

        if ((dx != 0 || dy != 0) && !player.IsAir())
        {
            bool flood = false;

            int nx = oldpos.Item1 + dx;
            int ny = oldpos.Item2 + dy;

            while (dx != 0 || dy != 0)
            {
                int i = Mathf.Clamp(nx, 0, sizex - 1) + 
                        Mathf.Clamp(ny, 0, sizey - 1) * sizex;
                     
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
                for (int x = 0; x < sizex; x++)
                {
                    for (int y = 0; y < sizey; y++)
                    {
                        if (x == 0 || y == 0 || x == sizex - 1 || y == sizey - 1)
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
                    if (x < 0 || y < 0 || x >= sizex || y >= sizey)
                        continue;
                    if (temp[x + y * sizex] || pixels[x + y * sizex] > 20) 
                        continue;
        
                    //Debug.Log($"aiin {x} | {y}");
                    temp[x + y * sizex] = true;

                    fillqueue.Enqueue((x, y + 1));
                    fillqueue.Enqueue((x + 1, y)); 
                    fillqueue.Enqueue((x, y - 1)); 
                    fillqueue.Enqueue((x - 1, y));                   
                }
                
                for (int j = 0; j < sizex * sizey; j++)
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
        for (int x = 0; x < sizex; x++)
        {
            for (int y = 0; y < sizey; y++)
            {
                if (pixels[x + y * sizex] > 0 &&
                    (getPixel(x + 1, y) < 220 || getPixel(x, y - 1) < 220 ||
                     getPixel(x - 1, y) < 220 || getPixel(x, y + 1) < 220))
                    pixels[x + y * sizex] = (byte)Math.Max(0, pixels[x + y * sizex] - 2);
            }
        }
    }
    
    private byte getPixel(int x, int y)
    {
        if (x < 0 || y < 0 || x >= sizex || y >= sizey)
            return 0;
        return pixels[x + y * sizex];
    }
    
    private bool[] temp = new bool[sizex * sizey];
    private Queue<(int, int)> fillqueue = new Queue<(int, int)>((sizex + sizey) * 2 * 5);
    private static readonly int CrackTex = Shader.PropertyToID("_CrackTex");
    
    
}
