using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SkinState { Round,Trian};

public class Button_SwitchSkinMenuMode : MonoBehaviour
{
    public GameObject RoundObj;
    public GameObject TrianObj;
    public Button Button_Round;
    public Button Button_Trian;


    SkinState state=SkinState.Round;

    public void SwitchMode()
    {
        if (state==SkinState.Round)
        {
            RoundObj.SetActive(false);
            TrianObj.SetActive(true);
            Button_Round.interactable = true;
            Button_Trian.interactable = false;
            state = SkinState.Trian;
        }
        else
        {
            RoundObj.SetActive(true);
            TrianObj.SetActive(false);
            Button_Round.interactable = false;
            Button_Trian.interactable = true;
            state = SkinState.Round;
        }
    }

}
