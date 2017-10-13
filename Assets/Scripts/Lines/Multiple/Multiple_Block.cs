using UnityEngine;
using System.Collections;

public class Multiple_Block : MonoBehaviour
{
    //SpriteRenderer sprite_rend;
    Color color;
    Calc_Numbers calc_numb;
    public bool active;
    int hp = 3;

    void OnEnable()
    {
        //gameObject.SetActive(true);
       
    }
	
	void Awake ()
    {
        //sprite_rend = GetComponent<SpriteRenderer>();
        calc_numb = GetComponentInChildren<Calc_Numbers>();
        //block_manager = transform.parent.GetComponentInParent<Multiple_BlockManager>();
    }

	public void InitBlock(int block_count, Color color)
    {
        active = true;
        gameObject.SetActive(true);
        switch (block_count)
        {
            case 1:
                //hp = Random.Range(GameController.game_controller.GetLvlData().multiple_prop_1_part.min_taps,
                //GameController.game_controller.GetLvlData().multiple_prop_1_part.max_taps+1);
                hp=GameController.game_controller.GetLvlData().multiple_prop_1_part.min_taps;
                break;
            case 2:
                //hp = Random.Range(GameController.game_controller.GetLvlData().multiple_prop_2_parts.min_taps,
                //GameController.game_controller.GetLvlData().multiple_prop_2_parts.max_taps+1);
                hp = GameController.game_controller.GetLvlData().multiple_prop_2_parts.min_taps;
                break;
            case 3:
                //hp = Random.Range(GameController.game_controller.GetLvlData().multiple_prop_3_parts.min_taps,
                //GameController.game_controller.GetLvlData().multiple_prop_3_parts.max_taps+1);
                hp = GameController.game_controller.GetLvlData().multiple_prop_3_parts.min_taps;
                break;
        }
        calc_numb.SetNumber(hp);
        SetColor(color);
    }
	public void SetColor(Color new_color)
    {
        //sprite_rend.color = color;
        color = new_color;
    }

    public bool Hit()
    {
        hp--;
        if ((hp >= 1) && (Ball.ball.trian_type == TrianType.DoubleTap))
            hp--;

        calc_numb.SetNumber(hp);
        if (hp<=0)
        {
            Disable();
            return true;
        }

        
        return false;
        
        
    }

    void Disable()
    {
        gameObject.SetActive(false);
        active = false;
    }

    public Color GetColor()
    {
        //return sprite_rend.color;
        return color;
    }
}
