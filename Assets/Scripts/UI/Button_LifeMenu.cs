using UnityEngine;

public class ButtonLifeMenu : MonoBehaviour
{
	public void Open()
    {
        GameController.gameController.ToLifeMenu();
    }

    public void Close()
    {
        GameController.gameController.ToMainMenu();
    }
}
