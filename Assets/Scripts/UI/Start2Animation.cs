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

    IEnumerator Animate()
    {
        anim.SetBool("animate", false);
        yield return new WaitForEndOfFrame();
        anim.SetBool("animate", true);
    }

    void Reset()
    {
        anim.SetBool("animate", false);
    }

    void OnEnable()
    {
        StartCoroutine(Animate());
    }

    void OnDisable()
    {
        Reset();
    }
}
