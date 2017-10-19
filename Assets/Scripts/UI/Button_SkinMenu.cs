using UnityEngine;

public class ButtonSkinMenu : MonoBehaviour
{
    public void ToSkinMenu()
    {
        SoundManager.soundManager.SingleSound(SoundSample.ToSkin);
        GameController.gameController.ToSkinMenu();
    }
}
