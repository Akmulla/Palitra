using UnityEngine;
using UnityEngine.UI;

public class StartPanel : MonoBehaviour
{
    public Image bg;

    void OnEnable()
    {
        bg.color = SkinManager.skinManager.GetCurrentSkin().bgColor;
    }
}
