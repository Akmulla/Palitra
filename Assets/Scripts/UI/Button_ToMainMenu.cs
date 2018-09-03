using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_ToMainMenu : MonoBehaviour
{
    public void ToMainMenu()
    {
        SoundManager.sound_manager.SingleSound(SoundSample.ToMain);
        //GameController.game_controller.ToMainMenu();
        SceneManager.LoadScene("New Main");
    }
}
