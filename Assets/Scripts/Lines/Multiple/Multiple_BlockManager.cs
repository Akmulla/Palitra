using UnityEngine;
//using System;
using System.Collections;
using System.Collections.Generic;

public class Multiple_BlockManager : MonoBehaviour
{
    public int block_count;
    public GameObject block;
    public Transform block_holder;
    [SerializeField]
    Multiple_Block[] block_mas;
    Line_Multiple line;
    float block_size;
    float window_size;
    int active_block_count;
    Color[] new_colors;
    //int current_block;

    void Start()
    {
        //window_size = Edges.rightEdge - Edges.leftEdge;
        //block_size = window_size / (float)block_count;  
    }

    public void SetDefault()
    {
        for (int i = 0; i < block_count; i++)
        {
            //block_mas[i].SetColor(new_colors[i]);
            block_mas[i].InitBlock(block_count, new_colors[i]);
        }
    }
    void OnEnable()
    {
        active_block_count = block_count;
        EventManager.StartListening("BallColorChanged", ColorChanged);
        //current_block = 0;
    }
    void OnDisable()
    {
        EventManager.StopListening("BallColorChanged", ColorChanged);
    }
    void ColorChanged()
    {
        foreach (Multiple_Block item in block_mas)
        {
            if ((Ball.ball.GetPosition().y> line.prev_edge) &&
                (Ball.ball.GetColor() == item.GetColor()) && (item.active))
            {
                if (item.Hit())
                {
                    active_block_count--;
                }
                if (active_block_count<=0)
                {
                    line.finished = true;
                    Ball.ball.LinePassed(Ball.ball.GetColor());
                    BallMove.ball_move.ResumeSpeed();
                }
            }
        }
    }
    public void InitBlocks()
    {
        line = GetComponent<Line_Multiple>();
        Vector3 spawn_position;
        GameObject obj;
        window_size = Edges.rightEdge - Edges.leftEdge;
        block_size = window_size / (float)block_count;
        for (int i = 0; i < block_count; i++)
        {
            spawn_position = new Vector3(Edges.leftEdge + block_size / 2.0f + block_size * i, transform.position.y);
            //obj = (GameObject)Instantiate(block, spawn_position, Quaternion.identity);
            obj = block_mas[i].gameObject;
            obj.transform.position = spawn_position;
            //obj.transform.localScale = new Vector3(block_size, obj.transform.localScale.y, 1.0f);
            //obj.transform.SetParent(block_holder);
            obj.SetActive(true);
        }
        //block_mas = GetComponentsInChildren<Multiple_Block>();
        SetColors();
    }

    void SetColors()
    {
        Color[] colors = SkinManager.skin_manager.GetCurrentSkin().colors;
        //Color[] new_colors=new Color[block_count];
        //Texture2D[] texture = TextureHandler.CreateTexture(colors, block_count, out new_colors);
        float full_length = Mathf.Abs(Edges.center_x - 
            line.left.GetComponent<Renderer>().bounds.size.x);
        float visible_lenght = Mathf.Abs(Edges.center_x - Edges.leftEdge);

        //float unused_part = 1.0f - (full_length - visible_lenght) / full_length;
        float unused_part = (full_length - visible_lenght) / full_length;
        unused_part /= 2.0f;
        Texture2D texture = TextureHandler.CreateTexture(colors, block_count, 
            unused_part, out new_colors);
        //print(new_colors.Length);
        for (int i = 0; i < block_count; i++)
        {
            //block_mas[i].SetColor(new_colors[i]);
            block_mas[i].InitBlock(block_count,new_colors[i]);
        }
        line.SetTexture(texture);
    }

    void SetHp()
    {
        
    }
}
