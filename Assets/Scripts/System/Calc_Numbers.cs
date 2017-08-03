using UnityEngine;
using System.Collections;

public class Calc_Numbers : MonoBehaviour
{
    public SpriteRenderer[] sprite_rend;
    int number=0;
    Line line;
    public Transform tran_0;
    public Transform tran_1;
    //Sprite[] number_pic;

	void Awake ()
    {
        //sprite_rend = GetComponent<SpriteRenderer>();
        //number = 0;
        line = transform.parent.GetComponentInParent<Line>();
        tran_0 = sprite_rend[0].transform;
        tran_1 = sprite_rend[1].transform;
        Resize(tran_0);
        Resize(tran_1);
    }

    void Resize(Transform pic_tran)
    {
        float pic_height = sprite_rend[0].sprite.bounds.extents.y * pic_tran.lossyScale.y;
        float k = line.GetHeight() / pic_height;
        float new_scale=pic_tran.localScale.y*k;

        pic_tran.localScale = new Vector3(new_scale, new_scale, 1.0f);
    }

    void SetPositionForOne()
    {
        tran_0.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
    }

    void SetPositionForTwo()
    {
        float pic_width = sprite_rend[0].sprite.bounds.extents.x * tran_0.lossyScale.x;

        tran_0.localPosition = new Vector3(pic_width, 0.0f, 0.0f);
        tran_1.localPosition = new Vector3(-pic_width, 0.0f, 0.0f);
    }

    public void Increase()
    {
        number++;
        Calculate(number);
    }
	
	public void Decrease()
    {
        number--;
        Calculate(number);
    }

    public void SetNumber(int value)
    {
        number = value;
        Calculate(number);
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
            sprite_rend[1].gameObject.SetActive(false);
            sprite_rend[0].sprite = NumberPicData.Instance.number_pic[number];

            SetPositionForOne();
        }
        else
        {
            sprite_rend[1].gameObject.SetActive(true);
            sprite_rend[0].sprite = NumberPicData.Instance.number_pic[number % 10];
            sprite_rend[1].sprite = NumberPicData.Instance.number_pic[number / 10];

            SetPositionForTwo();
        }
        
    }
}
