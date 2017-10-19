using System.Collections.Generic;
using UnityEngine;

public class LineBlock : Line
{
    int _mistake = 2;
    int _blockCount = 5;
    float _scrollSpeed = 0.5f;
    private Vector2 _savedOffset;
    Renderer _leftRend;
    Renderer _rightRend;
    bool _leftDir = true;
    float _x;
    Texture2D _mainTexture;

    float _randOffset;

    public override void InitLine()
    {
        _leftDir = Random.value > 0.5f ? true : false;
        _scrollSpeed = GameController.gameController.GetLvlData().blockProp.speed;
        _blockCount = GameController.gameController.GetLvlData().blockProp.blockCount;
        base.InitLine();
    }

    protected override void CheckIfPassed()
    {
        int coordX = (int)(_mainTexture.width / 2.0f + _mainTexture.width * _x);
        //float coord_x = x - Mathf.Floor(x);
        //float center = main_texture.width / 2.0f + coord_x;
        List <Color> colors = new List<Color>();
        for (int i=-_mistake; i<= _mistake; i++)
        {
            //colors.Add(main_texture.GetPixel((int)coord_x+i, (int)(main_texture.height / 2.0f)));
            colors.Add(_mainTexture.GetPixel(coordX + i, (int)(_mainTexture.height / 2.0f)));
        }
        
        if (Ball.ball.LinePassed(colors,false))
            anim.BeginAnimation();
    }

    protected override void Awake()
    {
        base.Awake();
        tran = GetComponent<Transform>();
        _leftRend=left.GetComponent<Renderer>();
        _rightRend=right.GetComponent<Renderer>();
        _savedOffset = _leftRend.sharedMaterial.GetTextureOffset("_MainTex");
    }

    public override void ChangeColor()
    {
        Color[] colors = SkinManager.skinManager.GetCurrentSkin().colors;
        Color[] col;
        _mainTexture = textureHandler.CreateTexture(colors, _blockCount,0.0f,out col);
        SetTexture(_mainTexture);
    }

    public override void Enable()
    {
        base.Enable();
        _randOffset = Random.Range(0.0f, 0.9f);
    }

    protected override void Update()
    {
        base.Update();
        if (active)
        {
            _x = _leftDir ? Mathf.Repeat(Time.time * _scrollSpeed, 1) : Mathf.Repeat(-Time.time * _scrollSpeed, 1);
            _x += _randOffset;
            Vector2 offset = new Vector2(_x, _savedOffset.y);
            //Vector2 offset_right = new Vector2(x + 0.5f, savedOffset.y);

            _leftRend.materials[0].SetTextureOffset("_MainTex", offset);
            _leftRend.materials[1].SetTextureOffset("_MainTex", offset);

            _rightRend.materials[0].SetTextureOffset("_MainTex", offset);
            _rightRend.materials[1].SetTextureOffset("_MainTex", offset);
        }
    }
}


  



