using UnityEngine;
//using System;

public class MultipleBlockManager : MonoBehaviour
{
    public int blockCount;
    public GameObject block;
    public Transform blockHolder;
    [SerializeField]
    MultipleBlock[] _blockMas;
    LineMultiple _line;
    float _blockSize;
    float _windowSize;
    int _activeBlockCount;
    Color[] _newColors;
    //int current_block;

    void Start()
    {
        //window_size = Edges.rightEdge - Edges.leftEdge;
        //block_size = window_size / (float)block_count;  
    }

    public void SetDefault()
    {
        for (int i = 0; i < blockCount; i++)
        {
            //block_mas[i].SetColor(new_colors[i]);
            _blockMas[i].InitBlock(blockCount, _newColors[i]);
        }
    }
    void OnEnable()
    {
        _activeBlockCount = blockCount;
        EventManager.StartListening("BallColorChanged", ColorChanged);
        //current_block = 0;
    }
    void OnDisable()
    {
        EventManager.StopListening("BallColorChanged", ColorChanged);
    }
    void ColorChanged()
    {
        foreach (MultipleBlock item in _blockMas)
        {
            if ((Ball.ball.GetPosition().y> _line.prevEdge) &&
                (Ball.ball.GetColor() == item.GetColor()) && (item.active))
            {
                if (item.Hit())
                {
                    _activeBlockCount--;

                }
                if (_activeBlockCount<=0)
                {
                    _line.finished = true;
                    Ball.ball.LinePassed(Ball.ball.GetColor());
                    BallMove.ballMove.ResumeSpeed();
                }
            }
        }
    }
    public void InitBlocks()
    {
        _line = GetComponent<LineMultiple>();
        Vector3 spawnPosition;
        GameObject obj;
        _windowSize = Edges.rightEdge - Edges.leftEdge;
        _blockSize = _windowSize / blockCount;
        for (int i = 0; i < blockCount; i++)
        {
            spawnPosition = new Vector3(Edges.leftEdge + _blockSize / 2.0f + _blockSize * i, transform.position.y);
            //obj = (GameObject)Instantiate(block, spawn_position, Quaternion.identity);
            obj = _blockMas[i].gameObject;
            obj.transform.position = spawnPosition;
            //obj.transform.localScale = new Vector3(block_size, obj.transform.localScale.y, 1.0f);
            //obj.transform.SetParent(block_holder);
            obj.SetActive(true);
        }
        //block_mas = GetComponentsInChildren<Multiple_Block>();
        SetColors();
    }

    void SetColors()
    {
        Color[] colors = SkinManager.skinManager.GetCurrentSkin().colors;
        //Color[] new_colors=new Color[block_count];
        //Texture2D[] texture = TextureHandler.CreateTexture(colors, block_count, out new_colors);
        float fullLength = Mathf.Abs(Edges.centerX - 
            _line.left.GetComponent<Renderer>().bounds.size.x);
        float visibleLenght = Mathf.Abs(Edges.centerX - Edges.leftEdge);

        //float unused_part = 1.0f - (full_length - visible_lenght) / full_length;
        float unusedPart = (fullLength - visibleLenght) / fullLength;
        unusedPart /= 2.0f;
        Texture2D texture = _line.textureHandler.CreateTexture(colors, blockCount, 
            unusedPart, out _newColors);
        //print(new_colors.Length);
        for (int i = 0; i < blockCount; i++)
        {
            //block_mas[i].SetColor(new_colors[i]);
            _blockMas[i].InitBlock(blockCount,_newColors[i]);
        }
        _line.SetTexture(texture);
    }

    void SetHp()
    {
        
    }
}
