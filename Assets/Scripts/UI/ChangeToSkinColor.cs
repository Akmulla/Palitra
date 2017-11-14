using UnityEngine;
using UnityEngine.UI;

public class ChangeToSkinColor : MonoBehaviour
{
    public enum SkinColors { Color_1,Color_2,Color_3,Background,Particle};
    public SkinColors color;
	
    void OnEnable()
    {
        EventManager.StartListening("SkinChanged", ChangeColor);
        ChangeColor();
    }

    void OnDisable()
    {
        EventManager.StopListening("SkinChanged", ChangeColor);
    }

    void ChangeColor()
    {
        Color apply_color=Color.black;

        if (color==SkinColors.Particle)
        {
            apply_color = SkinManager.skin_manager.GetCurrentSkin().particle_color;
        }
        else
        {
            if (color == SkinColors.Background)
            {
                apply_color = SkinManager.skin_manager.GetCurrentSkin().bg_color;
            }
            else
            {
                apply_color = SkinManager.skin_manager.GetCurrentSkin().colors[(int)color];
            }
        }

        Image image = GetComponent<Image>();
        if (image!=null)
        {
            image.color = apply_color;
        }
        
        SpriteRenderer sprite_rend = GetComponent<SpriteRenderer>();
        if (sprite_rend != null)
        {
            sprite_rend.color = apply_color;
        }

        Text text = GetComponent<Text>();
        if (text != null)
        {
            text.color = apply_color;
        }

        Camera cam = GetComponent<Camera>();
        if (cam!=null)
        {
            cam.backgroundColor = apply_color;
        }
        
        if (color == SkinColors.Particle)
        {
            if (GetComponent<ParticleSystem>()!=null)
            {
                ParticleSystem.ColorOverLifetimeModule part = GetComponent<ParticleSystem>().colorOverLifetime;
                part.color = apply_color;
                ParticleSystem.MainModule part2 = GetComponent<ParticleSystem>().main;
                part2.startColor = apply_color;
            }
            
        }
    }
}
