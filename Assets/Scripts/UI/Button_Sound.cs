using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Sound : MonoBehaviour
{
    public GameObject On;
    public GameObject Off;

    bool on = true;

    
    void Start ()
    {
		
	}
	
    public void Click()
    {
        if (on)
            TurnOff();
        else
            TurnOn();
    }
	void TurnOn()
    {
        on = true;
        SoundManager.sound_manager.On();
        On.SetActive(true);
        Off.SetActive(false);
    }

    void TurnOff()
    {
        on = false;
        SoundManager.sound_manager.Off();
        On.SetActive(false);
        Off.SetActive(true);
    }
}
