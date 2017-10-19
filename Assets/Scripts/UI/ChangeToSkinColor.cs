using UnityEngine;
using UnityEngine.UI;

public class ChangeToSkinColor : MonoBehaviour
{
    public enum SkinColors { Color1,Color2,Color3,Background,Particle}
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
        Color applyColor=Color.black;

        if (color==SkinColors.Particle)
        {
            applyColor = SkinManager.skinManager.GetCurrentSkin().particleColor;
        }
        else
        {
            if (color == SkinColors.Background)
            {
                applyColor = SkinManager.skinManager.GetCurrentSkin().bgColor;
            }
            else
            {
                applyColor = SkinManager.skinManager.GetCurrentSkin().colors[(int)color];
            }
        }

        Image image = GetComponent<Image>();
        if (image!=null)
        {
            image.color = applyColor;
        }
        
        SpriteRenderer spriteRend = GetComponent<SpriteRenderer>();
        if (spriteRend != null)
        {
            spriteRend.color = applyColor;
        }

        Text text = GetComponent<Text>();
        if (text != null)
        {
            text.color = applyColor;
        }

        Camera cam = GetComponent<Camera>();
        if (cam!=null)
        {
            cam.backgroundColor = applyColor;
        }
        
        if (color == SkinColors.Particle)
        {
            ParticleSystem.ColorOverLifetimeModule part = GetComponent<ParticleSystem>().colorOverLifetime;
            part.color = applyColor;
            ParticleSystem.MainModule part2 = GetComponent<ParticleSystem>().main;
            part2.startColor = applyColor;
        }
    }
}
