using UnityEngine;

public class Block : MonoBehaviour
{
    public bool collides;
    BlockManager _blockManager;
    static float _collisionDist = 0.01f;

    void Start()
    {
        _blockManager = GetComponentInParent<BlockManager>();
    }

    public void SetRandomColor()
    {
        GetComponent<SpriteRenderer>().color = SkinManager.skinManager.GetCurrentSkin().colors
            [Random.Range(0, SkinManager.skinManager.GetCurrentSkin().colors.Length)];
    }

    public void SetColor(Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
    }

    void Update()
    {
        collides = CheckIfCollides();
    }

    public Color GetColor()
    {
        return GetComponent<SpriteRenderer>().color;
    }

    public bool CheckIfCollides()
    {
        if ((transform.position.x + _blockManager.blockSize / 2.0f+_collisionDist>=0.0f) &&
                (transform.position.x - _blockManager.blockSize / 2.0f - _collisionDist <= 0.0f))
            return true;
        return false;
    }
}
