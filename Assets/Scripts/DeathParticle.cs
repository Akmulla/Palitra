using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathParticle : MonoBehaviour
{
    void OnEnable()
    {
        Color col = Ball.ball.GetColor();
        Change(col);
    }

    public void Change(Color apply_color)
    {
        ParticleSystem.ColorOverLifetimeModule part = GetComponent<ParticleSystem>().colorOverLifetime;
        part.color = apply_color;
        ParticleSystem.MainModule part2 = GetComponent<ParticleSystem>().main;
        part2.startColor = apply_color;
    }
}
