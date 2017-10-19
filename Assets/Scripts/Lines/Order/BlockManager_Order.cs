using UnityEngine;

public class BlockManagerOrder : MonoBehaviour
{
    public int blockCount;
    public GameObject block;
    public Transform blockHolder;
    public Transform arrow;

    [SerializeField]
    float _offsetY;
    [SerializeField]
    BlockOrder[] _blockMas;
    LineOrder _line;
    float _blockSize;
    float _windowSize;
    //int active_block_count;
    int _currentBlock;
    SpriteRenderer _arrowRend;


    Color _savedColor = Color.clear;
    float _savedTime;

    void Awake()
    {
        _line = GetComponent<LineOrder>();
        _arrowRend = arrow.gameObject.GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        //active_block_count = block_count;
        EventManager.StartListening("BallColorChanged", ColorChanged);
    }

    void OnDisable()
    {
        EventManager.StopListening("BallColorChanged", ColorChanged);
    }

    void ColorChanged()
    {
        if ((!_line.finished) && 
            ((_savedColor != Ball.ball.GetColor())||(_savedTime+0.25f<Time.time)))
        {
            if ( (Ball.ball.GetPosition().y > _line.prevEdge) &&
                    (Ball.ball.GetColor() == _blockMas[_currentBlock].GetColor()) )
            {
                _blockMas[_currentBlock].Disable();
                _currentBlock++;

                if (_currentBlock >= blockCount)
                {
                    _line.finished = true;
                    BallMove.ballMove.ResumeSpeed();
                    arrow.gameObject.SetActive(false);
                    return;
                }

                arrow.position = _blockMas[_currentBlock].GetPosition() + 
                    new Vector3(0.0f, _line.GetHeight()+ _offsetY, 0.0f);

                _savedColor = Ball.ball.GetColor();
                _savedTime = Time.time;
            }
            else
            {
                foreach (BlockOrder item in _blockMas)
                {
                    item.Enable();
                }

                _currentBlock = 0;
                arrow.position = _blockMas[0].GetPosition() + 
                    new Vector3(0.0f, _line.GetHeight() + _offsetY, 0.0f);
            }

            _arrowRend.color = _blockMas[_currentBlock].GetColor();
            
        }
        
    }

    public void InitBlocks()
    {
        _currentBlock = 0;
        _windowSize = Edges.rightEdge - Edges.leftEdge;
        _blockSize = _windowSize / blockCount;
        Vector3 spawnPosition;
        GameObject obj;

        for (int i = 0; i < blockCount; i++)
        {
            spawnPosition = new Vector3
                (Edges.leftEdge + _blockSize / 2.0f + _blockSize * i, transform.position.y);
            obj = _blockMas[i].gameObject;
            obj.transform.position = spawnPosition;
            obj.transform.localScale = new Vector3(_blockSize, obj.transform.localScale.y, 1.0f);
            obj.transform.SetParent(blockHolder);
            obj.SetActive(true);
        }
        
        SetColors();
        arrow.gameObject.SetActive(true);
        if (_blockMas != null)
            arrow.position = _blockMas[0].GetPosition() + 
                new Vector3(0.0f, _line.GetHeight(), 0.0f);
        _arrowRend.color = _blockMas[0].GetColor();
    }

    public void SetDefault()
    {
        _currentBlock = 0;
        arrow.gameObject.SetActive(true);
        arrow.position = _blockMas[0].GetPosition() +
                new Vector3(0.0f, _line.GetHeight() + _offsetY, 0.0f);
        _arrowRend.color = _blockMas[0].GetColor();
    }

    void SetColors()
    {
        Color[] newColors;
        float fullLength =Mathf.Abs( Edges.centerX - _line.left.GetComponent<Renderer>().bounds.size.x);
        float visibleLenght= Mathf.Abs(Edges.centerX - Edges.leftEdge);
        
        float unusedPart = ((fullLength - visibleLenght) / fullLength)/2.0f;
        Texture2D texture = _line.textureHandler.CreateTexture(SkinManager.skinManager.GetCurrentSkin().colors
            , blockCount, unusedPart, out newColors);

        for (int i = 0; i < blockCount; i++)
        {
            _blockMas[i].SetColor(newColors[i]);
        }

        _line.SetTexture(texture);
    }


    void SetRandomColors()
    {
        Color[] colors = new Color[blockCount];
        colors[0] = SkinManager.skinManager.GetCurrentSkin().colors
       [Random.Range(0, SkinManager.skinManager.GetCurrentSkin().colors.Length)];
        for (int i = 1; i < blockCount; i++)
        {
            Color color = Color.black;
            for (int j = 0; j < SkinManager.skinManager.GetCurrentSkin().colors.Length; j++)
            {
                bool cond1 = (SkinManager.skinManager.GetCurrentSkin().colors[j] != colors[i - 1]);
                if (cond1)
                {
                    color = SkinManager.skinManager.GetCurrentSkin().colors[j];
                    break;
                }
            }
            colors[i] = color;
        }

        for (int i = 0; i < blockCount; i++)
        {
            _blockMas[i].SetColor(colors[i]);
        }
    }
}
