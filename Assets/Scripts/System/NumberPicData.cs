using UnityEngine;

public class NumberPicData : MonoBehaviour
{
    public static NumberPicData instance;
    public Sprite[] numberPic;
    [HideInInspector]
    public int min;
    [HideInInspector]
    public int max;

    void Awake()
    {
        instance = this;
        min = 0;
        //max = number_pic.Length-1;
        max = 55;
    }
}
