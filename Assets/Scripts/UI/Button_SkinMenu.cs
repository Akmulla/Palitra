using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_SkinMenu : MonoBehaviour
{

    public void ToSkinMenu()
    {
        SoundManager.sound_manager.SingleSound(SoundSample.ToSkin);
        GameController.game_controller.ToSkinMenu();
    }
}
