using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rate : MonoBehaviour
{

    public void RateGame()
    {
        // Application.OpenURL("market://details?id=" + Application.productName);
#if UNITY_ANDROID
     //Application.OpenURL("market://details?id=" + Application.productName);
        Application.OpenURL("https://play.google.com/store/apps/details?id=secret.com.secretbox.palette"); 
#elif UNITY_IPHONE
     Application.OpenURL("itms-apps://itunes.apple.com/app/id1306735206");
#endif
    }
}
