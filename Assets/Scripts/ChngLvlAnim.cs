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
        EventManager.StartListening("ChangeLvl",ChangeLvl);
    }

    void OnDisable()
    {
        EventManager.StopListening("ChangeLvl", ChangeLvl);
    }

    void ChangeLvl()
    {
        anim.SetTrigger("ChangeLvl");
    }
}
