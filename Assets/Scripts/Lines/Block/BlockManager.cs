using UnityEngine;
using System.Collections.Generic;

public class BlockManager : MonoBehaviour
{
    public int block_count;
    public GameObject block;
    public Transform block_holder;
    public Transform man;
    Block[] block_mas;
    [HideInInspector]
    public float block_size;
    float window_size;
    public static float spawn_point;

    public void InitBlocks()
    {
        window_size = Edges.rightEdge - Edges.leftEdge;
        block_size = window_size / (float)block_count;
        spawn_point = Edges.leftEdge - window_size;
        //float offset = -5.0f;
        Vector3 spawn_position;
        GameObject obj;
        Color prev_color=Color.black;
        for (int i=0;i<block_count;i++)
        {
            //spawn_position = new Vector3(Edges.leftEdge + block_size/2.0f + block_size * i + offset, transform.position.y);
            spawn_position = new Vector3(Edges.leftEdge + block_size / 2.0f + block_size * i, transform.position.y);
            obj = (GameObject)Instantiate(block, spawn_position,Quaternion.identity);
            obj.transform.localScale = new Vector3(block_size+0.1f, obj.transform.localScale.y, 1.0f);

            //obj.GetComponent<Block>().SetRandomColor();
            Color color= SkinManager.skin_manager.GetCurrentSkin().colors
            [Random.Range(0, SkinManager.skin_manager.GetCurrentSkin().colors.Length)];
            if (i!=0)
            {
                if (color==prev_color)
                {
                    if(color== SkinManager.skin_manager.GetCurrentSkin().colors[0])
                    {
                        color = SkinManager.skin_manager.GetCurrentSkin().colors[1];
                    }
                    else
                    {
                        color = SkinManager.skin_manager.GetCurrentSkin().colors[0];
                    }
                }
            }
            obj.GetComponent<Block>().SetColor(color);
            prev_color = color;
            obj.transform.SetParent(block_holder);
        }

        spawn_position = new Vector3(Edges.leftEdge-window_size/ 2.0f, transform.position.y, 0.0f);
        obj = (GameObject)Instantiate(block_holder.gameObject, spawn_position, Quaternion.identity,transform);
        block_mas = GetComponentsInChildren<Block>();

        block_holder.GetComponent<BlockMove>().SetSpeed(GameController.game_controller.GetLvlData().block_prop.speed);
        obj.GetComponent<BlockMove>().SetSpeed(GameController.game_controller.GetLvlData().block_prop.speed);

        man.SetParent(block_holder);
    }

    public List<Color> CheckCollisions()
    {
        List<Color> colors=new List<Color>();
        foreach (Block item in block_mas)
        {
            if (item.CheckIfCollides())
            {
                colors.Add(item.GetColor());
            }
        }

        return colors;
    }

    public void SetRandomColors()
    {
        if (block_mas != null)
            foreach (Block item in block_mas)
        {
            
            item.SetRandomColor();
        }
    }
}


