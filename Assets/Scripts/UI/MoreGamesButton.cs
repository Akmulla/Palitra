using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreGamesButton : MonoBehaviour
{
    public void SeeMoreGames()
    {
#if UNITY_ANDROID
       // Application.OpenURL("https://play.google.com/store/apps/details?id=secret.com.secretbox.palette");
#elif UNITY_IPHONE
     //Application.OpenURL("itms-apps://itunes.apple.com/app/id1306735206");
#endif
    }
}
