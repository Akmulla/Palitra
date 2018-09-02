using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetProgress : MonoBehaviour
{
    [SerializeField] SaveLoadGame saveLoad;
    [SerializeField] GameObject popUp;

	public void OpenPopUp()
    {
        popUp.SetActive(true);
    }

    public void ClosePopUp()
    {
        popUp.SetActive(false);
    }

    public void ConfirmResetProgress()
    {
        saveLoad.ResetProgress();
        ClosePopUp();
    }
}
