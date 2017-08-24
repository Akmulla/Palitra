using UnityEngine;
using System.Collections;

public abstract class Line : MonoBehaviour
{
    protected float height;
    protected Transform tran;
    protected bool active=false;
    public GameObject left;
    public GameObject right;
    protected AnimationComponent anim;
    MeshRenderer[] mesh_rend;
    [HideInInspector]
    public TextureHandler texture_handler;
    MeshResize[] mesh_resize;

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
        mesh_rend = GetComponentsInChildren<MeshRenderer>();
        anim = GetComponent<AnimationComponent>();
        tran = GetComponent<Transform>();
        height = left.GetComponent<Renderer>().bounds.extents.y;
        texture_handler=GetComponent<TextureHandler>();
        mesh_resize = GetComponentsInChildren<MeshResize>();
    }

    public virtual void InitLine()
    {
        //active = true;
        for (int i=0;i<mesh_resize.Length;i++)
        {
            mesh_resize[i].scale();
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
        //left.GetComponent<MeshRenderer>().materials[1].mainTexture = texture;
        //right.GetComponent<MeshRenderer>().materials[1].mainTexture = texture;
        //left.GetComponent<MeshRenderer>().materials[0].mainTexture = texture;
        //right.GetComponent<MeshRenderer>().materials[0].mainTexture = texture;
        mesh_rend[0].materials[0].mainTexture = texture;
        mesh_rend[0].materials[1].mainTexture = texture;
        mesh_rend[1].materials[0].mainTexture = texture;
        mesh_rend[1].materials[1].mainTexture = texture;
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

    public virtual void Enable()
    {
        active = true;
    }


    public bool CheckIfActive()
    {
        return active;
    }
}
