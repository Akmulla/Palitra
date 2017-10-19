using UnityEngine;

public class TextureHandler : MonoBehaviour
{
    int _textSize = 2048;
    int _halfSize;
    int _height = 1;
    Texture2D _texture;
    Color[] _col;

    public void Awake()
    {
        //text_size = Screen.width;
        //half_size = text_size / 2;
        _halfSize = _textSize / 2;
        _texture = new Texture2D(_textSize, _height,TextureFormat.RGB24,false);
        _texture.filterMode = FilterMode.Point;
        _col = new Color[_textSize * _height];
    }


    public Texture2D CreateTexture(Color color)
    {
        
        for (int i=0;i<_col.Length;i++)
        {
            _col[i] = color;
        }
        _texture.SetPixels(_col);
        _texture.Apply();
                 
        return _texture;
    }
    ///
    public Texture2D CreateTexture(Color[] colors, int blockCount, float unusedPart,
        out Color[] newColors)
    {
        if (blockCount == 0)
            blockCount = 5;
        //Color[] col = new Color[text_size * height];
        newColors = CreateBlockColors(colors, blockCount);
        
        //Texture2D text = new Texture2D(text_size, height);
        int greyLeft = (int)(_texture.width* unusedPart);
        int greyRight = (int)(_texture.width * (1.0f-unusedPart));
        int blockSize = (greyRight - greyLeft) / blockCount;

        int k = 0;
        for (int y = 0; y < _texture.height; y++)
        {
            int h = 0;
            int xx = 0;
            for (int x = 0; x < _texture.width; x++)
            {
                if ((x < greyLeft) || (x > greyRight))
                {
                    if (x < greyLeft)
                    {
                        _col[k] = _col[k] = newColors[0];
                    }

                    if (x > greyRight)
                    {
                        _col[k] = _col[k] = newColors[newColors.Length-1];
                    }
                }
                else
                {
                    
                    if (xx >= blockSize)
                    {
                        xx = 0;
                        h++;
                    }

                    if (h < newColors.Length)
                        _col[k] = newColors[h];
                    else
                    {
                        _col[k] = newColors[newColors.Length - 1];
                    }
                    xx++;
                }


                
                k++;
                //text.SetPixel(x, y, new_colors[h]);
            }
            if (newColors.Length % 2 == 0)
            {
                _col[_halfSize - 1] = newColors[newColors.Length / 2 - 1];
                _col[_halfSize - 2] = newColors[newColors.Length / 2 - 1];
                _col[_halfSize] = newColors[newColors.Length / 2];
                _col[_halfSize + 1] = newColors[newColors.Length / 2];
            }
        }
        _texture.SetPixels(_col);
        _texture.Apply();

        return _texture;
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

    Color[] CreateBlockColors(Color[] colors, int blockCount)
    {
        if (blockCount == 0)
            blockCount = 5;

        Color[] newColors = new Color[blockCount];
        newColors[0] = SkinManager.skinManager.GetCurrentSkin().colors
       [Random.Range(0, SkinManager.skinManager.GetCurrentSkin().colors.Length)];
        if (blockCount == 1)
            return newColors;
        for (int i = 1; i < blockCount; i++)
        {
            //Color color = Color.black;
            Color[] availCol = new Color[colors.Length - 1];
            int n = 0;
            for (int j = 0; j < colors.Length; j++)
            {
                if (newColors[i - 1] != colors[j])
                {
                    availCol[n] = colors[j];
                    n++;
                }
            }
            newColors[i] = availCol[Random.Range(0, availCol.Length)];
        }
        if (blockCount == 2)
            return newColors;
        for (int i = 0; i < colors.Length; i++)
        {
            if ((colors[i] != newColors[0]) && (colors[i] != newColors[newColors.Length - 2]))
            {
                newColors[newColors.Length - 1] = colors[i];
                break;
            }
        }
        return newColors;
    }
}
