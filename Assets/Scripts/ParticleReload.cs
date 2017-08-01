using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleReload : MonoBehaviour
{
    bool on = true;
    public GameObject[] particles;

    public void Reload()
    {
        TurnOff();
        TurnOn();
    }

    public void TurnOn()
    {
        for (int i = 0; i < particles.Length; i++)
        {
            particles[i].SetActive(true);
        }
        on = true;
    }

    public void TurnOff()
    {
        for (int i = 0; i < particles.Length; i++)
        {
            particles[i].SetActive(false);
        }
        on = false;
    }

    public void SkinChanged()
    {
        TurnOff();
        TurnOn();
    }


    //void EndGame()
    //{
    //    TurnOff();
    //}

    //void BeginGame()
    //{
    //    if (!on)
    //        TurnOn();
    //}

    void OnEnable()
    {
        EventManager.StartListening("SkinChanged", SkinChanged);
        //EventManager.StartListening("EndGame", EndGame);
        //EventManager.StartListening("BeginGame", BeginGame);
    }

    void OnDisable()
    {
        EventManager.StopListening("SkinChanged", SkinChanged);
        //EventManager.StopListening("BeginGame", BeginGame);
        //EventManager.StopListening("EndGame", EndGame);
    }

}
