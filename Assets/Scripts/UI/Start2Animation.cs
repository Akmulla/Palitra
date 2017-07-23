using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start2Animation : MonoBehaviour
{
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Animate()
    {
        anim.SetBool("animate", true);
    }

    void Reset()
    {
        anim.SetBool("animate", false);
    }

    void OnEnable()
    {
        Animate();
    }
}
