using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChngLvlAnim : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnEnable()
    {
        EventManager.StartListening("LvlFinished", LvlFinished);
    }

    void OnDisable()
    {
        EventManager.StopListening("LvlFinished", LvlFinished);
    }

    void LvlFinished()
    {
        anim.SetTrigger("ChangeLvl");
    }
}
