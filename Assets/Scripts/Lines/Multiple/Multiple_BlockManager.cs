using UnityEngine;
//using System;
using System.Collections;
using System.Collections.Generic;

public class Multiple_BlockManager : MonoBehaviour
{
    public int block_count;
    public GameObject block;
    public Transform block_holder;
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
        //if (current_block<block_mas.Length)
        //{
        //    if (Ball.ball.GetColor() == block_mas[current_block].GetColor())
        //    {
        //        block_mas[current_block].Hit();
        //        current_block++;
        //        if (current_block >= block_mas.Length)
        //        {
        //            line.Disable();
        //        }
        //    }
        //}

        foreach (Multiple_Block item in block_mas)
        {
            //if ((line.GetTransform().position.y - line.GetHeight() - Ball.ball.GetPosition().y < SpawnWaves.spawn.dist)&&
            //    (Ball.ball.GetColor() == item.GetColor()) && (item.active))
            //if ((GameController.game_controller.GetLinesPassedNumber() == line.line_spawn_number - 1) &&
            //    (Ball.ball.GetColor() == item.GetColor()) && (item.active))
            if ((Ball.ball.GetPosition().y> line.prev_edge) &&
                (Ball.ball.GetColor() == item.GetColor()) && (item.active))
            {
                if (item.Hit())
                {
                    active_block_count--;
                }
                if (active_block_count<=0)
                {
                    //EventManager.TriggerEvent("LinePassed");
                    //line.Disable();
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
            obj = (GameObject)Instantiate(block, spawn_position, Quaternion.identity);
            obj.transform.localScale = new Vector3(block_size + 0.1f, obj.transform.localScale.y, 1.0f);


            //Color color = SkinManager.skin_manager.GetCurrentSkin().colors
            //[Random.Range(0, SkinManager.skin_manager.GetCurrentSkin().colors.Length)];

            //obj.GetComponent<Multiple_Block>().SetColor(color);
            //obj.GetComponent<Multiple_Block>().block_count=block_count;
            //obj.GetComponent<Multiple_Block>().InitBlock(block_count,color);
            obj.transform.SetParent(block_holder);
        }
        block_mas = GetComponentsInChildren<Multiple_Block>();
        SetColors();
        //for (int i = 0; i < block_count; i++)
        //{
        //    block_mas[i].InitBlock(block_count,new_colors[i]);
        //}
    }

    void SetColors()
    {
        Color[] colors = SkinManager.skin_manager.GetCurrentSkin().colors;
        Color[] new_colors=new Color[block_count];
        //Texture2D[] texture = TextureHandler.CreateTexture(colors, block_count, out new_colors);
        float full_length = Mathf.Abs(Edges.center_x - 
            line.left.GetComponent<Renderer>().bounds.size.x);
        float visible_lenght = Mathf.Abs(Edges.center_x - Edges.leftEdge);

        float unused_part = 1.0f - (full_length - visible_lenght) / full_length;
        //float unused_part = 1.0f - (visible_lenght) / full_length;
        //print(unused_part);
        Texture2D[] texture = TextureHandler.CreateTexture(colors, block_count, unused_part, out new_colors);
        //print(new_colors.Length);
        for (int i = 0; i < block_count; i++)
        {
            //block_mas[i].SetColor(new_colors[i]);
            block_mas[i].InitBlock(block_count,new_colors[i]);
        }
        line.SetTexture(texture);
    }
    //void SetColors()
    //{
    //    Color[] colors = SkinManager.skin_manager.GetCurrentSkin().colors;
        
    //    Texture2D[] texture = TextureHandler.CreateTexture(colors, block_count, out new_colors);
    //    for (int i = 0; i < block_count; i++)
    //    {
    //        block_mas[i].SetColor(new_colors[i]);
    //    }
    //    line.SetTexture(texture);
    //}

    void SetHp()
    {
        
    }
    //public IEnumerator SetRandomColors()
    //{
    //    //print("sfg");
    //    while (block_mas == null)
    //    yield return new WaitForEndOfFrame();


    //    //создаем и заполняем массив цветов
    //    int color_count = SkinManager.skin_manager.GetCurrentSkin().colors.Length > block_count ?
    //        SkinManager.skin_manager.GetCurrentSkin().colors.Length : block_count;

    //        Color[] colors = new Color[color_count];
    //        for (int i = 0; i < color_count; i++)
    //        {
    //            if (i < 3)
    //            {
    //                colors[i] = SkinManager.skin_manager.GetCurrentSkin().colors[i];
    //                //Debug.Log(colors[i]);
    //            }
    //            else
    //            {
    //                colors[i] = SkinManager.skin_manager.GetCurrentSkin().colors
    //                    [Random.Range(0, SkinManager.skin_manager.GetCurrentSkin().colors.Length)];
    //            }
    //        }
    //        //перемешиваем массив цветов
    //        new System.Random().Shuffle(colors);
    //        //присваеваем цвета блокам
    //        for (int i = 0; i < block_count; i++)
    //        {
    //            block_mas[i].SetColor(colors[i]);
    //        }
        
      
    //}

    //public void SetRandomColors()
    //{
    //    //print("sfg");
    //    if (block_mas != null)
    //    {
    //        //создаем и заполняем массив цветов
    //        Color[] colors = new Color[block_count];
    //        for (int i = 0; i < block_count; i++)
    //        {

    //            if (i < 3)
    //            {
    //                colors[i] = SkinManager.skin_manager.GetCurrentSkin().colors[i];
    //                Debug.Log(colors[i]);
    //            }
    //            else
    //            {
    //                colors[i] = SkinManager.skin_manager.GetCurrentSkin().colors
    //                    [Random.Range(0, SkinManager.skin_manager.GetCurrentSkin().colors.Length)];
    //            }
    //        }
    //        //перемешиваем массив цветов
    //        new System.Random().Shuffle(colors);
    //        //присваеваем цвета блокам
    //        for (int i = 0; i < block_count; i++)
    //        {
    //            block_mas[i].SetColor(colors[i]);
    //        }
    //    }
    //}
}
