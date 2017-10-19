using System.Collections;
using UnityEngine;

public class Start2Animation : MonoBehaviour
{
    Animator _anim;

    void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    IEnumerator Animate()
    {
        _anim.SetBool("animate", false);
        yield return new WaitForEndOfFrame();
        _anim.SetBool("animate", true);
    }

    void Reset()
    {
        _anim.SetBool("animate", false);
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
