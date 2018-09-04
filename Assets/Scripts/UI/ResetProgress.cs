using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetProgress : MonoBehaviour
{
    SaveLoadGame saveLoad => SaveLoadGame.save_load;
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
