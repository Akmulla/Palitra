using UnityEngine;
using System.Collections;

public abstract class Line : MonoBehaviour
{
    protected float height;
    protected Transform tran;
    protected bool active;
    public GameObject left;
    public GameObject right;
    protected AnimationComponent anim;

    protected virtual void Update()
    {
        CheckIfCrossed();
    }
    public Transform GetTransform()
    {
        return tran;
    }
    public float GetHeight()
    {
        return height;
    }
    protected virtual void CheckIfCrossed()
    {
        if ((active) && (tran.position.y - height <= Ball.ball.GetCollisionPosition().y))
        {
            active = false;
            CheckIfPassed();
           
        }
    }

    protected virtual void Awake()
    {
        anim = GetComponent<AnimationComponent>();
        tran = GetComponent<Transform>();
        height = left.GetComponent<Renderer>().bounds.extents.y;
    }

    public virtual void InitLine()
    {
        active = true;
        ChangeColor();
    }

    public void SetTexture(Texture2D[] texture)
    {
        left.GetComponent<MeshRenderer>().materials[1].mainTexture = texture[0];
        right.GetComponent<MeshRenderer>().materials[1].mainTexture = texture[1];
        left.GetComponent<MeshRenderer>().materials[0].mainTexture = texture[0];
        right.GetComponent<MeshRenderer>().materials[0].mainTexture = texture[1];
    }

    public void SetTexture(Texture2D texture)
    {
        left.GetComponent<MeshRenderer>().materials[1].mainTexture = texture;
        right.GetComponent<MeshRenderer>().materials[1].mainTexture = texture;
        left.GetComponent<MeshRenderer>().materials[0].mainTexture = texture;
        right.GetComponent<MeshRenderer>().materials[0].mainTexture = texture;
    }

    protected virtual void OnEnable()
    {
        
    }

    public abstract void ChangeColor();
    protected abstract void CheckIfPassed();

    public void Disable()
    {
        active = false;
    }

    public bool CheckIfActive()
    {
        return active;
    }
}
