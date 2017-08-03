using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Start_panel : MonoBehaviour
{
    public Image BG;

    void OnEnable()
    {
        BG.color = SkinManager.skin_manager.GetCurrentSkin().bg_color;
    }

}
