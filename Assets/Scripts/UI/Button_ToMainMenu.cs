using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_ToMainMenu : MonoBehaviour
{
    public void ToMainMenu()
    {
        SoundManager.sound_manager.SingleSound(SoundSample.ToMain);
        GameController.game_controller.ToMainMenu();
    }
}
