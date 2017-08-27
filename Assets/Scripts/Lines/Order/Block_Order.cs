using UnityEngine;

public class Block_Order : MonoBehaviour
{
    SpriteRenderer sprite_rend;
    Transform tran;
    public bool active;
    public Color color;

    void OnEnable()
    {
        active = true;
    }

    void Awake()
    {
        sprite_rend = GetComponent<SpriteRenderer>();
        tran = GetComponent<Transform>();
    }

    public void SetColor(Color new_color)
    {
        color = new_color;
    }

    public Vector3 GetPosition()
    {
        return tran.position;
    }

    public void Disable()
    {
        active = false;
    }

    public void Enable()
    {
        gameObject.SetActive(true);
        active = true;
    }

    public Color GetColor()
    {
        return color;
    }
}
