using UnityEngine;

public class BlockOrder : MonoBehaviour
{
    //SpriteRenderer sprite_rend;
    Transform _tran;
    public bool active;
    public Color color;

    void OnEnable()
    {
        active = true;
    }

    void Awake()
    {
       // sprite_rend = GetComponent<SpriteRenderer>();
        _tran = GetComponent<Transform>();
    }

    public void SetColor(Color newColor)
    {
        color = newColor;
    }

    public Vector3 GetPosition()
    {
        return _tran.position;
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
