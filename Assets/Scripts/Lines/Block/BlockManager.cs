using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public int blockCount;
    public GameObject block;
    public Transform blockHolder;
    public Transform man;
    Block[] _blockMas;
    [HideInInspector]
    public float blockSize;
    float _windowSize;
    public static float spawnPoint;

    public void InitBlocks()
    {
        _windowSize = Edges.rightEdge - Edges.leftEdge;
        blockSize = _windowSize / blockCount;
        spawnPoint = Edges.leftEdge - _windowSize;
        //float offset = -5.0f;
        Vector3 spawnPosition;
        GameObject obj;
        Color prevColor=Color.black;
        for (int i=0;i<blockCount;i++)
        {
            //spawn_position = new Vector3(Edges.leftEdge + block_size/2.0f + block_size * i + offset, transform.position.y);
            spawnPosition = new Vector3(Edges.leftEdge + blockSize / 2.0f + blockSize * i, transform.position.y);
            obj = Instantiate(block, spawnPosition,Quaternion.identity);
            obj.transform.localScale = new Vector3(blockSize+0.1f, obj.transform.localScale.y, 1.0f);

            //obj.GetComponent<Block>().SetRandomColor();
            Color color= SkinManager.skinManager.GetCurrentSkin().colors
            [Random.Range(0, SkinManager.skinManager.GetCurrentSkin().colors.Length)];
            if (i!=0)
            {
                if (color==prevColor)
                {
                    if(color== SkinManager.skinManager.GetCurrentSkin().colors[0])
                    {
                        color = SkinManager.skinManager.GetCurrentSkin().colors[1];
                    }
                    else
                    {
                        color = SkinManager.skinManager.GetCurrentSkin().colors[0];
                    }
                }
            }
            obj.GetComponent<Block>().SetColor(color);
            prevColor = color;
            obj.transform.SetParent(blockHolder);
        }

        spawnPosition = new Vector3(Edges.leftEdge-_windowSize/ 2.0f, transform.position.y, 0.0f);
        obj = Instantiate(blockHolder.gameObject, spawnPosition, Quaternion.identity,transform);
        _blockMas = GetComponentsInChildren<Block>();

        blockHolder.GetComponent<BlockMove>().SetSpeed(GameController.gameController.GetLvlData().blockProp.speed);
        obj.GetComponent<BlockMove>().SetSpeed(GameController.gameController.GetLvlData().blockProp.speed);

        man.SetParent(blockHolder);
    }

    public List<Color> CheckCollisions()
    {
        List<Color> colors=new List<Color>();
        foreach (Block item in _blockMas)
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
        if (_blockMas != null)
            foreach (Block item in _blockMas)
        {
            
            item.SetRandomColor();
        }
    }
}


