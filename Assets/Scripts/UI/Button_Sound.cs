using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public GameObject on;
    public GameObject off;

    bool _on = true;
	
    public void Click()
    {
        if (_on)
            TurnOff();
        else
            TurnOn();
    }

	void TurnOn()
    {
        _on = true;
        SoundManager.soundManager.On();
        on.SetActive(true);
        off.SetActive(false);
    }

    void TurnOff()
    {
        _on = false;
        SoundManager.soundManager.Off();
        on.SetActive(false);
        off.SetActive(true);
    }
}
