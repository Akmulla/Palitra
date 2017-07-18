using UnityEngine;
using System.Collections;

public class Calc_Numbers : MonoBehaviour
{
    public SpriteRenderer[] sprite_rend;
    int number=0;
    //Sprite[] number_pic;

	void Start ()
    {
        //sprite_rend = GetComponent<SpriteRenderer>();
        //number = 0;
        
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
        if ((number<0)||(number>100))
        {
            print("нахуй иди блять с такими значениями");
            return;
        }   

        if (number<10)
        {
            sprite_rend[1].gameObject.SetActive(false);
            sprite_rend[0].sprite = NumberPicData.Instance.number_pic[number];
        }
        else
        {
            sprite_rend[1].gameObject.SetActive(true);
            sprite_rend[0].sprite = NumberPicData.Instance.number_pic[number % 10];
            sprite_rend[1].sprite = NumberPicData.Instance.number_pic[number / 10];

        }
        
    }
}
