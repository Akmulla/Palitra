using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stroke : MonoBehaviour
{
    public Image image;

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
        image.color = SkinManager.skin_manager.GetCurrentSkin().particle_color;
    }
}
