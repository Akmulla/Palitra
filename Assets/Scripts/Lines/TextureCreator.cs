using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureCreator : MonoBehaviour
{
    public static TextureCreator creator;
    int text_size = 2048;
    int half_size;
    int height = 128;
    int grey_left;
    int grey_right;
    public Line line;

    void Awake ()
    {
        creator = this;
        half_size = text_size / 2;
        
        float full_length = Mathf.Abs(Edges.center_x - line.left.GetComponent<Renderer>().bounds.size.x);
        float visible_lenght = Mathf.Abs(Edges.center_x - Edges.leftEdge);
        float unused_part = ((full_length - visible_lenght) / full_length)/2.0f;
        grey_left = (int)((float)text_size * unused_part);
        grey_right = (int)((float)text_size * (1.0f - unused_part));
    }

    public Texture2D CreateTexture(Color color)
    {
        Texture2D text = new Texture2D(text_size, height);
        Color[] col = new Color[text_size * height];

        for (int i = 0; i < col.Length; i++)
        {
            col[i] = color;
        }
        text.SetPixels(col);
        text.Apply();
        //Texture2D new_tex = text.MemberwiseClone();
        return text;
    }
}
