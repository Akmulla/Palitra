using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleReload : MonoBehaviour
{

    public GameObject[] particles;

    void Reload()
    {
        for (int i=0;i<particles.Length;i++)
        {
            particles[i].SetActive(false);
            particles[i].SetActive(true);
        }
    }

    void OnEnable()
    {
        EventManager.StartListening("SkinChanged", Reload);
        EventManager.StartListening("BeginGame", Reload);
    }

    void OnDisable()
    {
        EventManager.StopListening("SkinChanged", Reload);
        EventManager.StopListening("BeginGame", Reload);
    }

}
