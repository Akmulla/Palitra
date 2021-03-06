﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureHandler : MonoBehaviour
{
    int text_size = 2048;
    int half_size;
    int height = 1;
    Texture2D texture;
    Color[] col;

    public void Awake()
    {
        //text_size = Screen.width;
        //half_size = text_size / 2;
        half_size = text_size / 2;
        texture = new Texture2D(text_size, height,TextureFormat.RGB24,false);
        texture.filterMode = FilterMode.Point;
        col = new Color[text_size * height];
    }


    public Texture2D CreateTexture(Color color)
    {
        
        for (int i=0;i<col.Length;i++)
        {
            col[i] = color;
        }
        texture.SetPixels(col);
        texture.Apply();
                 
        return texture;
    }
    ///
    public Texture2D CreateTexture(Color[] colors, int block_count, float unused_part,
        out Color[] new_colors)
    {
        if (block_count == 0)
            block_count = 5;
        //Color[] col = new Color[text_size * height];
        new_colors = CreateBlockColors(colors, block_count);
        
        //Texture2D text = new Texture2D(text_size, height);
        int grey_left = (int)((float)texture.width* unused_part);
        int grey_right = (int)((float)texture.width * (1.0f-unused_part));
        int block_size = (grey_right - grey_left) / block_count;

        int k = 0;
        for (int y = 0; y < texture.height; y++)
        {
            int h = 0;
            int xx = 0;
            for (int x = 0; x < texture.width; x++)
            {
                if ((x < grey_left) || (x > grey_right))
                {
                    if (x < grey_left)
                    {
                        col[k] = col[k] = new_colors[0];
                    }

                    if (x > grey_right)
                    {
                        col[k] = col[k] = new_colors[new_colors.Length-1];
                    }
                }
                else
                {
                    
                    if (xx >= block_size)
                    {
                        xx = 0;
                        h++;
                    }

                    if (h < new_colors.Length)
                        col[k] = new_colors[h];
                    else
                    {
                        col[k] = new_colors[new_colors.Length - 1];
                    }
                    xx++;
                }


                
                k++;
                //text.SetPixel(x, y, new_colors[h]);
            }
            if (new_colors.Length % 2 == 0)
            {
                col[half_size - 1] = new_colors[new_colors.Length / 2 - 1];
                col[half_size - 2] = new_colors[new_colors.Length / 2 - 1];
                col[half_size] = new_colors[new_colors.Length / 2];
                col[half_size + 1] = new_colors[new_colors.Length / 2];
            }
        }
        texture.SetPixels(col);
        texture.Apply();

        return texture;
    }

    //public Texture2D[] CreateTexture(Color[] colors, int block_count, out Texture2D main_text)
    //{
    //    Texture2D[] texture = new Texture2D[2];
    //    texture[0] = new Texture2D(half_size, height);
    //    texture[1] = new Texture2D(half_size, height);
    //    Color[] new_colors = CreateBlockColors(colors, block_count);

    //    Texture2D text = new Texture2D(text_size, height);
    //    float block_size = text.width / (float)block_count;
    //    for (int i = 0; i < block_count; i++)
    //    {
    //        for (int y = 0; y < text.height; y++)
    //        {
    //            for (int x = (int)(i * block_size); x < (int)((i + 1) * block_size); x++)
    //            {
    //                text.SetPixel(x, y, new_colors[i]);
    //            }
    //        }
    //    }

    //    for (int x = 0; x < half_size; x++)
    //    {
    //        for (int y = 0; y < text.height; y++)
    //        {
    //            texture[0].SetPixel(x, y, text.GetPixel(x, y));
    //        }
    //    }

    //    for (int x = 0; x < half_size; x++)
    //    {
    //        for (int y = 0; y < text.height; y++)
    //        {
    //            texture[1].SetPixel(x, y, text.GetPixel(x + half_size, y));
    //        }
    //    }

    //    text.Apply();
    //    texture[0].Apply();
    //    texture[1].Apply();

    //    ////
    //    texture[0] = text;
    //    texture[1] = text;
    //    ////
    //    main_text = text;
    //    return texture;
    //}

    Color[] CreateBlockColors(Color[] colors, int block_count)
    {
        if (block_count == 0)
            block_count = 5;

        Color[] new_colors = new Color[block_count];
        new_colors[0] = SkinManager.skin_manager.GetCurrentSkin().colors
       [UnityEngine.Random.Range(0, SkinManager.skin_manager.GetCurrentSkin().colors.Length)];
        if (block_count == 1)
            return new_colors;
        for (int i = 1; i < block_count; i++)
        {
            //Color color = Color.black;
            Color[] avail_col = new Color[colors.Length - 1];
            int n = 0;
            for (int j = 0; j < colors.Length; j++)
            {
                if (new_colors[i - 1] != colors[j])
                {
                    avail_col[n] = colors[j];
                    n++;
                }
            }
            new_colors[i] = avail_col[UnityEngine.Random.Range(0, avail_col.Length)];
        }
        if (block_count == 2)
            return new_colors;
        for (int i = 0; i < colors.Length; i++)
        {
            if ((colors[i] != new_colors[0]) && (colors[i] != new_colors[new_colors.Length - 2]))
            {
                new_colors[new_colors.Length - 1] = colors[i];
                break;
            }
        }
        return new_colors;
    }
}
