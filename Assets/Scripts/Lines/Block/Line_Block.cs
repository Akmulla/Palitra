using UnityEngine;
using System.Collections.Generic;
using System;

public class Line_Block : Line
{
    //public Color col;
    int block_count = 5;
    float scrollSpeed = 0.5f;
    private Vector2 savedOffset;
    Renderer left_rend;
    Renderer right_rend;
    bool left_dir = true;
    float x;
    Texture2D main_texture;

    public override void InitLine()
    {
        left_dir = UnityEngine.Random.value > 0.5f ? true : false;
        scrollSpeed = GameController.game_controller.GetLvlData().block_prop.speed;
        block_count = GameController.game_controller.GetLvlData().block_prop.block_count;
        //anim.ResetAnimation();
        base.InitLine();
        height = left.GetComponent<Renderer>().bounds.extents.y;
    }

    protected override void CheckIfPassed()
    {
        int coord_x = (int)(main_texture.width / 2.0f + main_texture.width * x);
        List <Color> colors = new List<Color>();
        for (int i=-1;i<=1;i++)
        {
            colors.Add(main_texture.GetPixel(coord_x+i, (int)(main_texture.height / 2.0f)));
        }
        
        Ball.ball.LinePassed(colors,false);
        anim.BeginAnimation();
    }

    protected override void Awake()
    {
        base.Awake();
        tran = GetComponent<Transform>();
        left_rend=left.GetComponent<Renderer>();
        right_rend=right.GetComponent<Renderer>();
        savedOffset = left_rend.sharedMaterial.GetTextureOffset("_MainTex");
    }

    public override void ChangeColor()
    {
        Color[] colors = SkinManager.skin_manager.GetCurrentSkin().colors;
        //Texture2D[] texture = TextureHandler.CreateTexture(colors,block_count,out main_texture);
        Color[] col;
        main_texture= texture_handler.CreateTexture(colors, block_count,0.0f,out col);
        SetTexture(main_texture);
    }

    public override void Enable()
    {
        base.Enable();
        //x = UnityEngine.Random.Range(0.0f, 0.9f);
    }

    protected override void Update()
    {
        base.Update();
       // if (active)
        {
            x = left_dir ? Mathf.Repeat(Time.time * scrollSpeed, 1) : Mathf.Repeat(-Time.time * scrollSpeed, 1);
            Vector2 offset = new Vector2(x, savedOffset.y);
            Vector2 offset_right = new Vector2(x + 0.5f, savedOffset.y);

            left_rend.materials[0].SetTextureOffset("_MainTex", offset);
            left_rend.materials[1].SetTextureOffset("_MainTex", offset);

            right_rend.materials[0].SetTextureOffset("_MainTex", offset);
            right_rend.materials[1].SetTextureOffset("_MainTex", offset);
            //rend.material.mainTextureOffset=offset;
        }
        ////////
        int coord_x = (int)(main_texture.width / 2.0f + main_texture.width * x);
        List<Color> colors = new List<Color>();
        for (int i = -1; i <= 1; i++)
        {
            colors.Add(main_texture.GetPixel(coord_x + i, (int)(main_texture.height / 2.0f)));
        }
        //col=(main_texture.GetPixel(coord_x , (int)(main_texture.height / 2.0f)));
        ///////////
    }
}


  



