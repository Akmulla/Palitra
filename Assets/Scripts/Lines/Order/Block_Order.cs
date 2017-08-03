using UnityEngine;
using System.Collections;

public class Block_Order : MonoBehaviour
{
    SpriteRenderer sprite_rend;
    Transform tran;
    public bool active;
    public Color color;

    void OnEnable()
    {
        //gameObject.SetActive(true);
        active = true;
    }

    void Awake()
    {
        sprite_rend = GetComponent<SpriteRenderer>();
        tran = GetComponent<Transform>();
    }

    public void SetColor(Color new_color)
    {
        //sprite_rend.color = color;
        color = new_color;
    }

    public Vector3 GetPosition()
    {
        return tran.position;
    }

    //public bool Hit()
    //{
    //    hp--;
    //    if (hp <= 0)
    //    {
    //        Disable();
    //        return true;
    //    }

    //    calc_numb.SetNumber(hp);
    //    return false;


    //}

    public void Disable()
    {
        //gameObject.SetActive(false);
        active = false;
    }

    public void Enable()
    {
        gameObject.SetActive(true);
        active = true;
    }

    public Color GetColor()
    {
        //return sprite_rend.color;
        return color;
    }
}
