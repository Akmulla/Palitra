using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class AATKitBinding : MonoBehaviour
{
    public enum PlacementSize
    {
        Banner320x53 = 0,
        Banner768x90 = 1,
        Banner300x250 = 2,
        Fullscreen = 3,
        MultiSizeBanner = 6,
        Rewarded = 7,
		BannerAuto = 10 // Auto choose banner type. Banner320x53 - iphone, Banner768x90 - ipad
	}

	public enum BannerAlignment
	{
		TopLeft = 0,
		TopCenter = 1,
		TopRight = 2,
		BottomLeft = 3,
		BottomCenter = 4,
		BottomRight = 5
	}

    public enum AdNetwork
    {
		HOUSE = 0,
		INMOBI = 1,
		ADMOB = 2,
		EMPTY = 3,
		APPLOVIN = 4,
		SMARTAD = 5,
		NEXAGE = 6,
		ADX = 7,
		DFP = 8,
		SMAATO = 9,
		FACEBOOK = 10,
		UNITYADS = 11,
		ADCOLONY = 12,
		LOOPME = 13,
		AMAZON = 14,
		MOPUB = 15,
		OPENX = 16,
		FLURRY = 17,
		REVMOB = 18,
		PERMODO = 19,
		APPNEXUS = 20,
		INNERACTIVE = 21,
		APPNEXUSHBMOPUB = 22,
		APPNEXUSHBDFP = 23,
		OGURY = 24,
        CRITEO = 25,
        SPOTX = 26,
        GENERICVAST = 27,
		ONEBYAOL = 28,
        THIRDPRESENCE = 29,
		VUNGLE = 30
	}

	public enum NativeAdType
	{
		APP_INSTALL = 0,
		CONTENT = 1,
		OTHER = 2,
		UNKNOWN = 3,
		VIDEO = 4
	};

	public enum PlacementContentGravity
	{
		Top = 0,
		Bottom = 1,
		Center = 2
	};

    private static readonly string LogPrefix = "AATKitBinding: ";

    private static readonly string CSVSeparator = ";";

	private static readonly string NewLine = "\n";

//-----------------------------------------------------------------------------------------------------
	private static AATKitBinding _instance = null;
	public static AATKitBinding Instance
	{
		get { return _instance; }
	}

    public static bool ScriptLogEnabled
    {
        private get;
        set;
    }

    private static void Log(string message)
    {
        if(ScriptLogEnabled)
        {
            Debug.Log(LogPrefix + message);
        }
    }

	void Awake()
	{
        ScriptLogEnabled = true;
        Log("Awake");

        if (_instance != null && _instance != this)
		{
			Destroy(gameObject);
			return;
		}
		
		_instance = this;
		
		DontDestroyOnLoad(gameObject); // Don't get destroyed when loading a new level.

        #if UNITY_ANDROID
        if (_plugin == null && Application.platform == RuntimePlatform.Android)
        {
            using (AndroidJavaClass pluginClass = new AndroidJavaClass("com.intentsoftware.addapptr.AATKitActivity"))
            {
                _plugin = pluginClass.CallStatic<AndroidJavaObject> ("instance");
            }
        }
        #endif
	}

	/*
	 * aatkit events
	 * */
//	public void OnHaveAd(string placementName) 
//	{
//		Debug.Log("onHaveAd event: " + placementName);
//	}
//
//	public void OnNoAd(string placementName) 
//	{
//		Debug.Log("onNoAd event: " + placementName);
//	}
//
//	public void OnPauseForAd(string placementName) 
//	{
//		Debug.Log("onPauseForAd event: " + placementName);
//	}
//
//	public void OnResumeAfterAd(string placementName) 
//	{
//		Debug.Log("onResumeAfterAd event: " + placementName);
//	}
//
//	public void OnShowingEmpty(string placementName) 
//	{
//		Debug.Log("onShowingEmpty event: " + placementName);
//	}
//
//	public void OnOrientationChange(string orientation)
//	{
//		Debug.Log("onOrientationChange event: " + orientation);
//	}


//===============================================================================================================================================================
	// interface to objective-c runtime (internal use only)
	#region Externals

    [DllImport("__Internal")]
    private static extern void aatkitInitWithConfiguration(string objectName, string configuration);

    [DllImport("__Internal")]
    private static extern void aatkitReconfigureUsingConfiguration(string configuration);

	[DllImport("__Internal")]
	private static extern void aatkitInit(string objectName);

	[DllImport("__Internal")]
	private static extern void aatkitInitWithTestMode(string objectName, int testID);

	[DllImport("__Internal")]
	private static extern void aatkitInitWithRulesCaching(string objectName, bool cachingEnabled, string initialRules);

	[DllImport("__Internal")]
	private static extern void aatkitSetDebugEnabled();

	[DllImport("__Internal")]
	private static extern void aatkitSetDebugShakeEnabled(bool enabled);

	[DllImport("__Internal")]
	private static extern string aatkitGetVersion();

	[DllImport("__Internal")]
	private static extern string aatkitGetDebugInfo();

	[DllImport("__Internal")]
	private static extern bool aatkitIsNetworkEnabled(int network);

	[DllImport("__Internal")]
	private static extern void aatkitSetNetworkEnabled(int network, bool enabled);

	[DllImport("__Internal")]
	private static extern string aatkitCreatePlacement(string placementName, int placementSize);

	[DllImport("__Internal")]
	private static extern void aatkitAddPlacementToView(string placementName);

	[DllImport("__Internal")]
	private static extern void aatkitRemovePlacementFromView(string placementName);

	[DllImport("__Internal")]
	private static extern void aatkitStartPlacementAutoReload(string placementName);
	
	[DllImport("__Internal")]
	private static extern void aatkitStopPlacementAutoReload(string placementName);
	
	[DllImport("__Internal")]
	private static extern bool aatkitReloadPlacement(string placementName);
	
	[DllImport("__Internal")]
	private static extern bool aatkitReloadPlacementForced(string placementName, bool forced);

	[DllImport("__Internal")]
	private static extern bool aatkitHasAdForPlacement(string placementName);
	
	[DllImport("__Internal")]
	private static extern void aatkitStartPlacementAutoReloadWithSeconds(string placementName, int seconds);
	
	[DllImport("__Internal")]
	private static extern void aatkitSetPlacementAlignment(string placementName, int bannerAlignment);

	[DllImport("__Internal")]
	private static extern void aatkitSetMultiSizeAlignment(string placementName, int bannerAlignment);

	[DllImport("__Internal")]
	private static extern void aatkitSetPlacementPosition(string placementName, int posX, int posY);

	[DllImport("__Internal")]
	private static extern void aatkitSetMultiSizePosition(string placementName, int posX, int posY);
	
	[DllImport("__Internal")]
	private static extern bool aatkitShowPlacement(string placementName);
	
	[DllImport("__Internal")]
	private static extern void aatkitEnablePromo();
	
	[DllImport("__Internal")]
	private static extern void aatkitDisablePromo();
	
	[DllImport("__Internal")]
	private static extern void aatkitPreparePromo();

	[DllImport("__Internal")]
	private static extern bool aatkitShowPromo(bool force);

	[DllImport("__Internal")]
	private static extern string aatkitGetNativeAd(string placementName);

	[DllImport("__Internal")]
	private static extern bool aatkitIsNativeAdExpired(string nativeAdId);

	[DllImport("__Internal")]
	private static extern void aatkitReportAdSpaceForNativePlacement(string placementName);

	[DllImport("__Internal")]
	private static extern int aatkitCurrentlyLoadingNativeAdsOnPlacement(string placementName);

	[DllImport("__Internal")]
	private static extern bool aatkitIsFrequencyCapReachedForNativePlacement(string placementName);

	[DllImport("__Internal")]
	private static extern void aatkitAttachNativeAd(string nativeAdId);

	[DllImport("__Internal")]
	private static extern void aatkitDetachNativeAd(string nativeAdId);

	[DllImport("__Internal")]
	private static extern void aatkitUpdateNativeView(string nativeAdId, int x, int y, int width, int height);

	[DllImport("__Internal")]
	private static extern void aatkitSetPlacementContentGravity(string placementName, int gravity);

	[DllImport("__Internal")]
	private static extern void aatkitSetTargetingInfo (string info);

	[DllImport("__Internal")]
	private static extern void aatkitSetTargetingInfoForPlacement (string placementName, string info);

	[DllImport("__Internal")]
	private static extern void aatkitAddAdNetworkForKeywordTargeting (int network);

	[DllImport("__Internal")]
	private static extern void aatkitRemoveAdNetworkForKeywordTargeting (int network);

    #endregion

#if UNITY_ANDROID
	private static AndroidJavaObject _plugin;
#endif

    //===============================================================================================================================================================
#if UNITY_ANDROID
	void OnApplicationPause(bool pause)
	{
        Log("OnApplicationPause pause: " + pause);

		if(Application.platform == RuntimePlatform.Android && _plugin != null)
		{
			if(pause)
			{
				_plugin.Call("aatkitOnPause");
			}
			else
			{
				_plugin.Call("aatkitOnResume");
			}
		}
	}

	void OnApplicationQuit()
	{
        Log("OnApplicationQuit");
		if(Application.platform == RuntimePlatform.Android && _plugin != null)
		{
			_plugin.Call("aatkitOnDestroy");
		}
	}
#endif

    public static void Init(string objectName, AATKitConfiguration configuration)
    {
        string jsonConfiguration = JsonUtility.ToJson(configuration);
        Log("Init objectName: " + objectName + " configuration: " + jsonConfiguration);

        if(configuration.ConsentString != string.Empty && configuration.ConsentAutomatic)
        {
            Debug.LogWarning("Both ConsentString and ConsentAutomatic fields are set. AATKit will only use ConsentAutomatic value.");
        }

        #if UNITY_IOS
        if(Application.platform == RuntimePlatform.IPhonePlayer)
        {
            aatkitInitWithConfiguration(objectName, jsonConfiguration);
        }
        #elif UNITY_ANDROID
        if (Application.platform == RuntimePlatform.Android)
        {
            if (_plugin != null)
            {
                _plugin.Call("aatkitInit", objectName, jsonConfiguration);
            }
        }
        #endif
    }

    public static void ReconfigureUsingConfiguration(AATKitConfiguration configuration)
    {
        string jsonConfiguration = JsonUtility.ToJson(configuration);
        Log("ReconfigureUsingConfiguration configuration: " + jsonConfiguration);

        if (configuration.ConsentString != string.Empty && configuration.ConsentAutomatic)
        {
            Debug.LogWarning("Both ConsentString and ConsentAutomatic fields are set. AATKit will only use ConsentAutomatic value.");
        }

        #if UNITY_IOS
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            aatkitReconfigureUsingConfiguration(jsonConfiguration);
        }
        #elif UNITY_ANDROID
        if (Application.platform == RuntimePlatform.Android)
        {
            if (_plugin != null)
            {
                _plugin.Call("aatkitReconfigureUsingConfiguration", jsonConfiguration);
            }
        }
        #endif
    }

    //===============================================================================================================================================================
    // Initializes the AATKit library. Should be called once during application initialization before any other calls to AATKit.
    [Obsolete]
    public static void Init(string objectName)
	{
        Log("Init objectName: " + objectName);

	#if UNITY_IOS
		if(Application.platform == RuntimePlatform.IPhonePlayer)
		{
			aatkitInit(objectName);
		}
	#elif UNITY_ANDROID
		if(Application.platform == RuntimePlatform.Android)
		{
			if(_plugin != null)
			{
				_plugin.Call("aatkitInit", objectName);
			}
		}
	#endif
	}

	// Enables test ads and initializes the AATKit library. Should be called once during application initialization before any other calls to AATKit. For testing development only!
    [Obsolete]
    public static void InitWithTestMode(string objectName, int testID)
	{
        Log("InitWithTestMode objectName: " + objectName + " testID: " + testID);

    #if UNITY_IOS
		if(Application.platform == RuntimePlatform.IPhonePlayer)
		{
			aatkitInitWithTestMode(objectName, testID);
		}
#elif UNITY_ANDROID
		if(Application.platform == RuntimePlatform.Android)
		{
			if(_plugin != null)
			{
				_plugin.Call("aatkitInitWithTestMode", objectName, testID);
			}
		}
#endif
    }

	// Enables test ads and initializes the AATKit library. Should be called once during application initialization before any other calls to AATKit. For testing development only!
    [Obsolete]
    public static void InitWithRulesCaching(string objectName, bool cachingEnabled, string initialRules)
	{
        Log("InitWithRulesCaching objectName: " + objectName + " cachingEnabled: " + cachingEnabled + " initialRules: " + initialRules);

        #if UNITY_IOS
		if(Application.platform == RuntimePlatform.IPhonePlayer)
		{
			aatkitInitWithRulesCaching(objectName, cachingEnabled, initialRules);
		}
#elif UNITY_ANDROID
		if(Application.platform == RuntimePlatform.Android)
		{
			if(_plugin != null)
			{
				_plugin.Call("aatkitInitWithRulesCaching", objectName, cachingEnabled, initialRules);
			}
		}
#endif
    }

	// Enables debug mode.
	public static void SetDebugEnabled()
	{
        Log("SetDebugEnabled");

	#if UNITY_IOS
		if(Application.platform == RuntimePlatform.IPhonePlayer)
		{
			aatkitSetDebugEnabled();
		}
	#elif UNITY_ANDROID
		if(Application.platform == RuntimePlatform.Android)
		{
			if(_plugin != null)
			{
				_plugin.Call("aatkitSetDebugEnabled");
			}
		}
	#endif
	}

	// Enables/disables debug screen that will show after shaking the device. Only to be used for testing during development.
	public static void SetDebugShakeEnabled(bool enabled)
	{
        Log("SetDebugShakeEnabled enabled: " + enabled);

	#if UNITY_IOS
		if(Application.platform == RuntimePlatform.IPhonePlayer)
		{
			aatkitSetDebugShakeEnabled(enabled);
		}
	#elif UNITY_ANDROID
		if(Application.platform == RuntimePlatform.Android)
		{
			if(_plugin != null)
			{
				_plugin.Call("aatkitSetDebugShakeEnabled", enabled);
			}
		}
	#endif
	}

	// AATKit version in X.Y.Z(internal XXYY) format. For example: 1.0.1 (internal 0243).
	public static string GetVersion()
	{
        Log("GetVersion");

	#if UNITY_IOS
		if(Application.platform == RuntimePlatform.IPhonePlayer)
		{
			return aatkitGetVersion();
		}
	#elif UNITY_ANDROID
		if(Application.platform == RuntimePlatform.Android)
		{
			if(_plugin != null)
			{
				return _plugin.Call<string>("aatkitGetVersion");
			}
		}
	#endif

		return "unknown";
	}

	// Used for obtaining debug information (the same that would be presented in dialog after shaking the device if debug screen is enabled)
	public static string GetDebugInfo()
	{
        Log("GetDebugInfo");

		#if UNITY_IOS
		if(Application.platform == RuntimePlatform.IPhonePlayer)
		{
			return aatkitGetDebugInfo();
		}
		#elif UNITY_ANDROID
		if(Application.platform == RuntimePlatform.Android)
		{
			if(_plugin != null)
			{
				return _plugin.Call<string>("aatkitGetDebugInfo");
			}
		}
		#endif

		return "unknown";
	}

	// Checks if ad network is enabled.
	public static bool IsNetworkEnabled(int network)
	{
        Log("IsNetworkEnabled network:" + network);

	#if UNITY_IOS
		if(Application.platform == RuntimePlatform.IPhonePlayer)
		{
			return aatkitIsNetworkEnabled((int)network);
		}
	#elif UNITY_ANDROID
		if(Application.platform == RuntimePlatform.Android)
		{
			if(_plugin != null)
			{
				return _plugin.Call<bool>("aatkitIsNetworkEnable", (int)network);
			}
		}
	#endif

		return false;
	}
		
	// Allows to enable or disable selected ad networks. By default all networks are enabled.
	public static void SetNetworkEnabled(int network, bool enabled)
	{
        Log("SetNetworkEnabled network:" + network + " enabled: " + enabled);

	#if UNITY_IOS
		if(Application.platform == RuntimePlatform.IPhonePlayer)
		{
			aatkitSetNetworkEnabled((int)network, enabled);
		}
	#elif UNITY_ANDROID
		if(Application.platform == RuntimePlatform.Android)
		{
			if(_plugin != null)
			{
				_plugin.Call("aatkitSetNetworkEnabled", (int)network, enabled);
			}
		}
	#endif
	}

	// Creates placement with given name and size.
	public static Vector2 CreatePlacement(string placementName, PlacementSize placementSize)
	{
        Log("CreatePlacement placementName:" + placementName + " placementSize: " + placementSize);

        string result = "0x0";
		int x, y;

	#if UNITY_IOS
		if(Application.platform == RuntimePlatform.IPhonePlayer)
		{
			result = aatkitCreatePlacement(placementName, (int)placementSize);
		}
	#elif UNITY_ANDROID
		if(Application.platform == RuntimePlatform.Android)
		{
			if(_plugin != null)
			{
				result = _plugin.Call<string>("aatkitCreatePlacement", placementName, (int)placementSize);
			}
		}
	#endif

		char[] delimiterChars = {'x'};
		string[] pos = result.Split(delimiterChars);

		try
		{
			x = int.Parse(pos[0]);
			y = int.Parse(pos[1]);
			
		}
		catch (Exception e)
		{
			Debug.LogException(e);
			x = 0;
			y = 0;
		}

		return new Vector2(x, y);
	}

	// Adds placement with given name to your view.
	public static void AddPlacementToView(string placementName)
	{
        Log("AddPlacementToView placementName:" + placementName);

	#if UNITY_IOS
		if(Application.platform == RuntimePlatform.IPhonePlayer)
		{
			aatkitAddPlacementToView(placementName);
		}
	#elif UNITY_ANDROID
		if(Application.platform == RuntimePlatform.Android)
		{
			if(_plugin != null)
			{
				_plugin.Call("aatkitAddPlacementToView", placementName);
			}
		}
	#endif
	}

	// Removes placement with given name from view.
	public static void RemovePlacementFromView(string placementName)
	{
        Log("RemovePlacementFromView placementName:" + placementName);

	#if UNITY_IOS
		if(Application.platform == RuntimePlatform.IPhonePlayer)
		{
			aatkitRemovePlacementFromView(placementName);
		}
	#elif UNITY_ANDROID
		if(Application.platform == RuntimePlatform.Android)
		{
			if(_plugin != null)
			{
				_plugin.Call("aatkitRemovePlacementFromView", placementName);
			}
		}
	#endif
	}

	// Enables automatic reloading of placement. Autoreloader will use reload time configured on addapptr.com account or fallback to default 30L seconds.
	public static void StartPlacementAutoReload(string placementName)
	{
        Log("StartPlacementAutoReload placementName:" + placementName);

	#if UNITY_IOS
		if(Application.platform == RuntimePlatform.IPhonePlayer)
		{
			aatkitStartPlacementAutoReload(placementName);
		}
	#elif UNITY_ANDROID
		if(Application.platform == RuntimePlatform.Android)
		{
			if(_plugin != null)
			{
				_plugin.Call("aatkitStartPlacementAutoReload", placementName);
			}
		}
	#endif
	}

	// Disables automatic reloading of placement.
	public static void StopPlacementAutoReload(string placementName)
	{
        Log("StopPlacementAutoReload placementName:" + placementName);

	#if UNITY_IOS
		if(Application.platform == RuntimePlatform.IPhonePlayer)
		{
			aatkitStopPlacementAutoReload(placementName);
		}
	#elif UNITY_ANDROID
		if(Application.platform == RuntimePlatform.Android)
		{
			if(_plugin != null)
			{
				_plugin.Call("aatkitStopPlacementAutoReload", placementName);
			}
		}
	#endif
	}

	// Requests placement reload. Works only if automatic reloading is disabled.
	public static bool ReloadPlacement(string placementName)
	{
        Log("ReloadPlacement placementName:" + placementName);

	#if UNITY_IOS
		if(Application.platform == RuntimePlatform.IPhonePlayer)
		{
			return aatkitReloadPlacement(placementName);
		}
	#elif UNITY_ANDROID
		if(Application.platform == RuntimePlatform.Android)
		{
			if(_plugin != null)
			{
				return _plugin.Call<bool>("aatkitReloadPlacement", placementName);
			}
		}
	#endif

		return false;
	}
	
	// Requests placement reload. Works only if automatic reloading is disabled.
	public static bool ReloadPlacementForced(string placementName, bool forced)
	{
        Log("ReloadPlacementForced placementName:" + placementName + " forced: " + forced);

    #if UNITY_IOS
		if(Application.platform == RuntimePlatform.IPhonePlayer)
		{
			return aatkitReloadPlacementForced(placementName, forced);
		}
#elif UNITY_ANDROID
		if(Application.platform == RuntimePlatform.Android)
		{
			if(_plugin != null)
			{
				return _plugin.Call<bool>("aatkitReloadPlacementForced", placementName, forced);
			}
		}
#endif

        return false;
	}

	// Returns true if there is an ad loaded for given placementId.
	public static bool HasAdForPlacement(string placementName)
	{
        Log("HasAdForPlacement placementName:" + placementName);

		#if UNITY_IOS
		if(Application.platform == RuntimePlatform.IPhonePlayer)
		{
			return aatkitHasAdForPlacement(placementName);
		}
		#elif UNITY_ANDROID
		if(Application.platform == RuntimePlatform.Android)
		{
			if(_plugin != null)
			{
				return _plugin.Call<bool>("aatkitHasAdForPlacement", placementName);
			}
		}
		#endif

		return false;
	}

	// Enables automatic reloading of placement and sets custom reload time. This reload time will be used instead of time configured on addapptr.com account.
	public static void StartPlacementAutoReloadWithSeconds(string placementName, int seconds)
	{
        Log("StartPlacementAutoReloadWithSeconds placementName:" + placementName + " seconds: " + seconds);

    #if UNITY_IOS
		if(Application.platform == RuntimePlatform.IPhonePlayer)
		{
			aatkitStartPlacementAutoReloadWithSeconds(placementName, seconds);
		}
#elif UNITY_ANDROID
		if(Application.platform == RuntimePlatform.Android)
		{
			if(_plugin != null)
			{
				_plugin.Call("aatkitStartPlacementAutoReloadWithSeconds", placementName, seconds);
			}
		}
#endif
    }

	// Sets multi size banner placement position on the screen.
	public static void SetMultiSizeAlignment(string placementName, BannerAlignment multiSizeAlignment)
	{
        Log("SetMultiSizeAlignment placementName:" + placementName + " multiSizeAlignment: " + multiSizeAlignment);

		#if UNITY_IOS
		if(Application.platform == RuntimePlatform.IPhonePlayer)
		{
			aatkitSetMultiSizeAlignment(placementName, (int)multiSizeAlignment);
		}
		#elif UNITY_ANDROID
		if(Application.platform == RuntimePlatform.Android)
		{
			if(_plugin != null)
			{
				_plugin.Call("aatkitSetMultiSizeAlignment", placementName, (int)multiSizeAlignment);
			}
		}
		#endif
	}

	// Sets banner placement position on the screen.
	public static void SetPlacementAlignment(string placementName, BannerAlignment bannerAlignment)
	{
        Log("SetPlacementAlignment placementName:" + placementName + " bannerAlignment: " + bannerAlignment);

	#if UNITY_IOS
		if(Application.platform == RuntimePlatform.IPhonePlayer)
		{
			aatkitSetPlacementAlignment(placementName, (int)bannerAlignment);
		}
	#elif UNITY_ANDROID
		if(Application.platform == RuntimePlatform.Android)
		{
			if(_plugin != null)
			{
				_plugin.Call("aatkitSetPlacementAlignment", placementName, (int)bannerAlignment);
			}
		}
	#endif
	}

	// Sets banner placement position on the screen (x, y)
	public static void SetPlacementPosition(string placementName, int posX, int posY)
	{
        Log("SetPlacementPosition placementName:" + placementName + " posX: " + posX + " posY: " + posY);

    #if UNITY_IOS
		if(Application.platform == RuntimePlatform.IPhonePlayer)
		{
			aatkitSetPlacementPosition(placementName, posX, posY);
		}
#elif UNITY_ANDROID
		if(Application.platform == RuntimePlatform.Android)
		{
			if(_plugin != null)
			{
				_plugin.Call("aatkitSetPlacementPosition", placementName, posX, posY);
			}
		}
#endif
    }

	// Sets multi size banner placement position on the screen (x, y)
	public static void SetMultiSizePosition(String placementName, int posX, int posY)
	{
        Log("SetMultiSizePosition placementName:" + placementName + " posX: " + posX + " posY: " + posY);

		#if UNITY_IOS
		if(Application.platform == RuntimePlatform.IPhonePlayer)
		{
			aatkitSetMultiSizePosition(placementName, posX, posY);
		}
		#elif UNITY_ANDROID
		if(Application.platform == RuntimePlatform.Android)
		{
			if(_plugin != null)
			{
				_plugin.Call("aatkitSetMultiSizePosition", placementName, posX, posY);
			}
		}
		#endif
	}

	// Shows interstitial ad if ad is ready.
	public static bool ShowPlacement(string placementName)
	{
        Log("ShowPlacement placementName:" + placementName);

	#if UNITY_IOS
		if(Application.platform == RuntimePlatform.IPhonePlayer)
		{
			return aatkitShowPlacement(placementName);
		}
	#elif UNITY_ANDROID
		if(Application.platform == RuntimePlatform.Android)
		{
			if(_plugin != null)
			{
				return _plugin.Call<bool>("aatkitShowPlacement", placementName);
			}
		}
	#endif

		return false;
	}

	// Enables promo ads. AATKit may show a promo interstitial from now on until disablePromo() is called.
	[Obsolete("Will be removed in a future release: Use preparePromo and showPromo: instead.")]
	public static void EnablePromo()
	{
        Log("EnablePromo");

	#if UNITY_IOS
		if(Application.platform == RuntimePlatform.IPhonePlayer)
		{
			aatkitEnablePromo();
		}
	#elif UNITY_ANDROID
		if(Application.platform == RuntimePlatform.Android)
		{
			if(_plugin != null)
			{
				_plugin.Call("aatkitEnablePromo");
			}
		}
	#endif
	}

	// Disables promo ads. AATKit will no longer show promo interstitials.
	[Obsolete("Will be removed in a future release: Use preparePromo and showPromo: instead.")]
	public static void DisablePromo()
	{
        Log("DisablePromo");

	#if UNITY_IOS
		if(Application.platform == RuntimePlatform.IPhonePlayer)
		{
			aatkitDisablePromo();
		}
	#elif UNITY_ANDROID
		if(Application.platform == RuntimePlatform.Android)
		{
			if(_plugin != null)
			{
				_plugin.Call("aatkitDisablePromo");
			}
		}
	#endif
	}

	// Prepares promo ad without enabling it.
	public static void PreparePromo()
	{
        Log("PreparePromo");

	#if UNITY_IOS
		if(Application.platform == RuntimePlatform.IPhonePlayer)
		{
			aatkitPreparePromo();
		}
	#elif UNITY_ANDROID
		if(Application.platform == RuntimePlatform.Android)
		{
			if(_plugin != null)
			{
				_plugin.Call("aatkitPreparePromo");
			}
		}
	#endif
	}

	// Shows promo ad if it is ready. Note that promo can appear automatically if enablePromo() is called, so in such case there is no need to call this method.
	public static bool ShowPromo(bool force = false)
	{
        Log("ShowPromo force: " + force);

	#if UNITY_IOS
		if(Application.platform == RuntimePlatform.IPhonePlayer)
		{
			return aatkitShowPromo(force);
		}
	#elif UNITY_ANDROID
		if(Application.platform == RuntimePlatform.Android)
		{
			if(_plugin != null)
			{
				return _plugin.Call<bool>("aatkitShowPromo", force);
			}
		}
	#endif

		return false;
	}
		

	public static void AddAdNetworkForKeywordTargeting(AdNetwork network)
	{
        Log("AddAdNetworkForKeywordTargeting network: " + network);

		#if UNITY_IOS
		if(Application.platform == RuntimePlatform.IPhonePlayer)
		{
			aatkitAddAdNetworkForKeywordTargeting((int) network);
		}
		#elif UNITY_ANDROID
		if(Application.platform == RuntimePlatform.Android)
		{
			if(_plugin != null)
			{
				_plugin.Call("aatkitAddAdNetworkForKeywordTargeting", (int) network);
			}
		}
		#endif
	}

	public static void RemoveAdNetworkForKeywordTargeting(AdNetwork network)
	{
        Log("RemoveAdNetworkForKeywordTargeting network: " + network);

		#if UNITY_IOS
		if(Application.platform == RuntimePlatform.IPhonePlayer)
		{
			aatkitRemoveAdNetworkForKeywordTargeting((int) network);
		}
		#elif UNITY_ANDROID
		if(Application.platform == RuntimePlatform.Android)
		{
			if(_plugin != null)
			{
				_plugin.Call("aatkitRemoveAdNetworkForKeywordTargeting", (int) network);
			}
		}
		#endif
	}

	public static void SetTargetingInfo(String placementName, Dictionary<string, List<string>> info)
	{
        Log("SetTargetingInfo placementName: " + placementName + " info: " + CreateCSVForTargetInfo(info));

		#if UNITY_IOS
		if(Application.platform == RuntimePlatform.IPhonePlayer)
		{
			aatkitSetTargetingInfoForPlacement(placementName, CreateCSVForTargetInfo(info));
		}
		#elif UNITY_ANDROID
		if(Application.platform == RuntimePlatform.Android)
		{
			if(_plugin != null)
			{
				_plugin.Call("aatkitSetTargetingInfo", placementName, info);
			}
		}
		#endif
	}

	public static void SetTargetingInfo(Dictionary<String, List<string>> info)
	{
		Log("SetTargetingInfo info: " + CreateCSVForTargetInfo(info));
		#if UNITY_IOS
		if(Application.platform == RuntimePlatform.IPhonePlayer)
		{
			aatkitSetTargetingInfo(CreateCSVForTargetInfo(info));
		}
		#elif UNITY_ANDROID
		if(Application.platform == RuntimePlatform.Android)
		{
			if(_plugin != null)
			{
				_plugin.Call("aatkitSetTargetingInfo", CreateAndroidTargetInfo(info));
			}
		}
		#endif
	}

	private static string CreateCSVForTargetInfo(Dictionary<String, List<string>> info)
	{
		string result = "";
		int lineNumber = 0;

		foreach (KeyValuePair<string, List<string>> entry in info) 
		{
			if (lineNumber != 0) 
			{
				result += NewLine;
			}
			result += entry.Key;
			lineNumber++;

			List<string> values = entry.Value;
			for(int i=0; i<values.Count; i++)
			{
				result += CSVSeparator + values [i];
			}
		}

		return result;
	}

	private static AndroidJavaObject CreateAndroidTargetInfo(Dictionary<string, List<string>> info)
	{
		AndroidJavaObject map = new AndroidJavaObject ("java.util.HashMap");

		foreach(KeyValuePair<string, List<string>> entry in info)
		{
			AndroidJavaObject list = new AndroidJavaObject("java.util.LinkedList");
			foreach (String infoValue in entry.Value) 
			{
				AndroidJavaObject infoValueString = new AndroidJavaObject ("java.lang.String", infoValue);
				list.Call<bool>("add", infoValueString);
			}


			AndroidJavaObject keyString = new AndroidJavaObject ("java.lang.String", entry.Key);
			IntPtr mapMethodPut = AndroidJNIHelper.GetMethodID(map.GetRawClass(), "put", "(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;");

			object[] args = new object[2];
			args[0] = keyString;
			args[1] = list;

			AndroidJNI.CallObjectMethod(map.GetRawObject(), mapMethodPut, AndroidJNIHelper.CreateJNIArgArray(args));
		}

		return map;
	}

	public static void SetPlacementContentGravity(string placementName, PlacementContentGravity gravity)
	{
        Log("SetPlacementContentGravity placementName: " + placementName + " gravity: " + gravity);

		#if UNITY_IOS
		if(Application.platform == RuntimePlatform.IPhonePlayer)
		{
			aatkitSetPlacementContentGravity(placementName, (int)gravity);
		}
		#elif UNITY_ANDROID
		if(Application.platform == RuntimePlatform.Android)
		{
			if(_plugin != null)
			{
				_plugin.Call("aatkitSetPlacementContentGravity", placementName, (int)gravity);
			}
		}
		#endif
	}
}
