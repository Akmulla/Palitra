using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppsFlyerCode : MonoBehaviour
{

    void Start()
    {
        /* Mandatory - set your AppsFlyer’s Developer key. */
        AppsFlyer.setAppsFlyerKey("wMAPF8fviYWeeQaYU4tmj3");
        /* For detailed logging */
        AppsFlyer.setIsDebug(true);
#if UNITY_IOS
        /* Mandatory - set your apple app ID
           NOTE: You should enter the number only and not the "ID" prefix */
        AppsFlyer.setAppID("YOUR_APP_ID_HERE");
        AppsFlyer.trackAppLaunch();
#elif UNITY_ANDROID
   /* Mandatory - set your Android package name */
   AppsFlyer.setAppID ("secret.com.secretbox.palette");
   /* For getting the conversion data in Android, you need to add the "AppsFlyerTrackerCallbacks" listener.*/
   //AppsFlyer.init ("YOUR_DEV_KEY","AppsFlyerTrackerCallbacks");
#endif
    }

}
