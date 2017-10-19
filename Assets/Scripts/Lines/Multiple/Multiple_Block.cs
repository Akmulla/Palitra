using UnityEngine;

public class MultipleBlock : MonoBehaviour
{
    //SpriteRenderer sprite_rend;
    Color _color;
    CalcNumbers _calcNumb;
    public bool active;
    int _hp = 3;

    void OnEnable()
    {
        //gameObject.SetActive(true);
       
    }
	
	void Awake ()
    {
        //sprite_rend = GetComponent<SpriteRenderer>();
        _calcNumb = GetComponentInChildren<CalcNumbers>();
        //block_manager = transform.parent.GetComponentInParent<Multiple_BlockManager>();
    }

	public void InitBlock(int blockCount, Color color)
    {
        active = true;
        gameObject.SetActive(true);
        switch (blockCount)
        {
            case 1:
                //hp = Random.Range(GameController.game_controller.GetLvlData().multiple_prop_1_part.min_taps,
                //GameController.game_controller.GetLvlData().multiple_prop_1_part.max_taps+1);
                _hp=GameController.gameController.GetLvlData().multipleProp1Part.minTaps;
                break;
            case 2:
                //hp = Random.Range(GameController.game_controller.GetLvlData().multiple_prop_2_parts.min_taps,
                //GameController.game_controller.GetLvlData().multiple_prop_2_parts.max_taps+1);
                _hp = GameController.gameController.GetLvlData().multipleProp2Parts.minTaps;
                break;
            case 3:
                //hp = Random.Range(GameController.game_controller.GetLvlData().multiple_prop_3_parts.min_taps,
                //GameController.game_controller.GetLvlData().multiple_prop_3_parts.max_taps+1);
                _hp = GameController.gameController.GetLvlData().multipleProp3Parts.minTaps;
                break;
        }
        _calcNumb.SetNumber(_hp);
        SetColor(color);
    }
	public void SetColor(Color newColor)
    {
        //sprite_rend.color = color;
        _color = newColor;
    }

    public bool Hit()
    {
        _hp--;
        if ((_hp >= 1) && (Ball.ball.trianType == TrianType.DoubleTap))
            _hp--;

        _calcNumb.SetNumber(_hp);
        if (_hp<=0)
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
        return _color;
    }
}
