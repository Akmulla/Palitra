using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreGamesButton : MonoBehaviour
{
    public void SeeMoreGames()
    {
#if UNITY_ANDROID
        Application.OpenURL("https://play.google.com/store/apps/collection/cluster?clp=igM4ChkKEzg2MzMyNzY2MDA1NDYwNDc0MTgQCBgDEhkKEzg2MzMyNzY2MDA1NDYwNDc0MTgQCBgDGAA%3D:S:ANO1ljKITbU");
#elif UNITY_IPHONE
     Application.OpenURL("https://itunes.apple.com/us/developer/lite-games-gmbh/id943875563?mt=8");
#endif
    }
}
