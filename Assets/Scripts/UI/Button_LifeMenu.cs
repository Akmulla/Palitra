using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_LifeMenu : MonoBehaviour
{
	public void Open()
    {
        GameController.game_controller.ToLifeMenu();
    }

    public void Close()
    {
        GameController.game_controller.ToMainMenu();
    }
}
