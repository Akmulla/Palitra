using UnityEngine;

public class Pause : MonoBehaviour
{
    public void PauseGame()
    {
        GameController.gameController.Pause();
    }
}
