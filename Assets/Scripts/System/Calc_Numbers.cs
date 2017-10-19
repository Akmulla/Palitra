using UnityEngine;

public class CalcNumbers : MonoBehaviour
{
    public SpriteRenderer[] spriteRend;
    int _number;
    Line _line;
    public Transform tran0;
    public Transform tran1;

	void Awake ()
    {
        _line = transform.parent.GetComponentInParent<Line>();
        tran0 = spriteRend[0].transform;
        tran1 = spriteRend[1].transform;
        Resize(tran0);
        Resize(tran1);
    }

    void Resize(Transform picTran)
    {
        float coeff = 0.9f;

        float picHeight = spriteRend[0].sprite.bounds.extents.y * picTran.lossyScale.y;
        float k = _line.GetHeight() / picHeight;
        float newScale=picTran.localScale.y*k;

        picTran.localScale = new Vector3(newScale*coeff, newScale*coeff, 1.0f);
    }

    void SetPositionForOne()
    {
        tran0.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
    }

    void SetPositionForTwo()
    {
        float picWidth = spriteRend[0].sprite.bounds.extents.x * tran0.lossyScale.x;

        tran0.localPosition = new Vector3(picWidth, 0.0f, 0.0f);
        tran1.localPosition = new Vector3(-picWidth, 0.0f, 0.0f);
    }

    public void Increase()
    {
        _number++;
        Calculate(_number);
    }
	
	public void Decrease()
    {
        _number--;
        Calculate(_number);
    }

    public void SetNumber(int value)
    {
        _number = value;
        Calculate(_number);
    }

    void Calculate(int number)
    {
        if ((number<0)||(number>99))
        {
            print("нахуй иди блять с такими значениями");
            return;
        }   

        if (number<10)
        {
            spriteRend[1].gameObject.SetActive(false);
            spriteRend[0].sprite = NumberPicData.instance.numberPic[number];

            SetPositionForOne();
        }
        else
        {
            spriteRend[1].gameObject.SetActive(true);
            spriteRend[0].sprite = NumberPicData.instance.numberPic[number % 10];
            spriteRend[1].sprite = NumberPicData.instance.numberPic[number / 10];

            SetPositionForTwo();
        }
        
    }
}
