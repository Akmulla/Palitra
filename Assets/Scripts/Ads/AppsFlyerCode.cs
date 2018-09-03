using System.Collections;
using UnityEngine;

namespace Ads
{
    public class AppsFlyerCode : MonoBehaviour
    {

        public static AppsFlyerCode Instance;

        void Start()
        {
            DontDestroyOnLoad(gameObject);

            StartCoroutine(TenMinutes());
            StartCoroutine(ThirtyMinutes());

            Instance = this;

            /* Mandatory - set your AppsFlyer’s Developer key. */
            AppsFlyer.setAppsFlyerKey("wMAPF8fviYWeeQaYU4tmj3");
            /* For detailed logging */
#if DEBUG
            AppsFlyer.setIsDebug(true);
#endif
#if UNITY_IOS
/* Mandatory - set your apple app ID
           NOTE: You should enter the number only and not the "ID" prefix */
        AppsFlyer.setAppID("YOUR_APP_ID_HERE");
        AppsFlyer.trackAppLaunch();
#elif UNITY_ANDROID
            /* Mandatory - set your Android package name */
            AppsFlyer.setAppID (Application.identifier);
            /* For getting the conversion data in Android, you need to add the "AppsFlyerTrackerCallbacks" listener.*/
            AppsFlyer.init ("wMAPF8fviYWeeQaYU4tmj3");

#endif
        }

        private IEnumerator TenMinutes()
        {
            yield return new WaitForSeconds(60 * 10);
            AppsFlyer.trackEvent("10Minutes", "Played");
        }

        private IEnumerator ThirtyMinutes()
        {
            yield return new WaitForSeconds(60 * 30);
            AppsFlyer.trackEvent("30Minutes", "Played");
        }

        public void StartGame()
        {
            AppsFlyer.trackEvent("Start Game", "");
        }
    }
}
