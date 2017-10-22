using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseStartAnim : MonoBehaviour
{
    public Animator anim;

    public void Animate()
    {
         anim.SetBool("animate",true);
    }

    public void ResetAnim()
    {
        anim.SetBool("animate", false);
    }

    void OnEnable()
    {
        ResetAnim();
    }
}
