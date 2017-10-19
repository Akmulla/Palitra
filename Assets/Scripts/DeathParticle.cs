using UnityEngine;

public class DeathParticle : MonoBehaviour
{
    void OnEnable()
    {
        Color col = Ball.ball.GetColor();
        Change(col);
    }

    public void Change(Color applyColor)
    {
        ParticleSystem.ColorOverLifetimeModule part = GetComponent<ParticleSystem>().colorOverLifetime;
        part.color = applyColor;
        ParticleSystem.MainModule part2 = GetComponent<ParticleSystem>().main;
        part2.startColor = applyColor;
    }
}
