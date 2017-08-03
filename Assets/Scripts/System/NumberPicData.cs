using UnityEngine;
using System.Collections;


public class NumberPicData : MonoBehaviour
{
    public static NumberPicData Instance;
    public Sprite[] number_pic;
    [HideInInspector]
    public int min;
    [HideInInspector]
    public int max;

    void Awake()
    {
        Instance = this;
        min = 0;
        //max = number_pic.Length-1;
        max = 55;
    }
}
