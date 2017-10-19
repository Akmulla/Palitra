using UnityEngine;

public abstract class Line : MonoBehaviour
{
    protected float height;
    protected Transform tran;
    protected bool active;
    public GameObject left;
    public GameObject right;
    protected AnimationComponent anim;
    MeshRenderer[] _meshRend;
    [HideInInspector]
    public TextureHandler textureHandler;
    protected MeshResize[] meshResize;

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
            CheckIfPassed();
            active = false;
        }
    }

    protected virtual void Awake()
    {
        _meshRend = GetComponentsInChildren<MeshRenderer>();
        anim = GetComponent<AnimationComponent>();
        tran = GetComponent<Transform>();
        height = left.GetComponent<Renderer>().bounds.extents.y;
        textureHandler=GetComponent<TextureHandler>();
        meshResize = GetComponentsInChildren<MeshResize>();
    }

    public virtual void InitLine()
    {
        active = false;
        for (int i=0;i<meshResize.Length;i++)
        {
            meshResize[i].Scale();
        }
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
        _meshRend[0].materials[0].mainTexture = texture;
        _meshRend[0].materials[1].mainTexture = texture;
        _meshRend[1].materials[0].mainTexture = texture;
        _meshRend[1].materials[1].mainTexture = texture;
    }

    protected virtual void OnEnable()
    {
        //нужно для переопределения
    }

    public abstract void ChangeColor();
    protected abstract void CheckIfPassed();

    public void Disable()
    {
        active = false;
    }

    public virtual void Enable()
    {
        active = true;
    }
    
    public bool CheckIfActive()
    {
        return active;
    }
}
