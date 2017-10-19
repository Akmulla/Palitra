using UnityEngine;
using UnityEngine.UI;

public enum SkinState { Round,Trian}

public class ButtonSwitchSkinMenuMode : MonoBehaviour
{
    public GameObject roundObj;
    public GameObject trianObj;
    public Button buttonRound;
    public Button buttonTrian;

    SkinState _state=SkinState.Round;

    public SkinState State
    {
        get { return _state; }
    }

    public void SwitchMode()
    {
        if (_state==SkinState.Round)
        {
            roundObj.SetActive(false);
            trianObj.SetActive(true);
            buttonRound.interactable = true;
            buttonTrian.interactable = false;
            _state = SkinState.Trian;
        }
        else
        {
            roundObj.SetActive(true);
            trianObj.SetActive(false);
            buttonRound.interactable = false;
            buttonTrian.interactable = true;
            _state = SkinState.Round;
        }
    }

}
