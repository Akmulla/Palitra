using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Pause : MonoBehaviour
{
    public void PauseGame()
    {
        GameController.game_controller.Pause();
    }
}
