using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Pause : MonoBehaviour
{

    public void PauseGame()
    {
        //print("pause");
        //UIController.ui.Pause();
        GameController.game_controller.Pause();
        
    }
}
