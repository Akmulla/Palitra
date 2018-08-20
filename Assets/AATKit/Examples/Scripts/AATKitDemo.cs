using UnityEngine;
using UnityEngine.UI;

public class AATKitDemo : MonoBehaviour
{
	public enum DemoState
	{
		Init = 0,
		Banner = 1,
		Fullscreen = 2,
		Multisize = 3
	}

	DemoState currentState = DemoState.Init;
	string version = "unknown";
	const string bannerPlacement = "BannerPlacement";
	const string interstitialPlacement = "FullscreenPlacement";
	const string multiSizePlacement = "MultiSizePlacement";
	const string nativePlacement = "NativePlacement";
	bool initialized = false;
	bool shakeEnabled = false;
	AATKitBinding.BannerAlignment bannerAlignment = AATKitBinding.BannerAlignment.BottomCenter;
	AATKitBinding.BannerAlignment multiSizeBannerAlignment = AATKitBinding.BannerAlignment.BottomCenter;
	bool bannerAutoreload = false;
	bool multiSizeAutoreload = false;
	bool interstitialAutoreload = false;
	
	int buttonWidth = 256;
	int buttonHeight = 64;

	string shakeText = "Enable Debug Shake";
	string autoreloadBannerText = "Enable Banner Autoreload";
	string autoreloadMultiSizeText = "Enable Multi Size Banner Autoreload";
	string autoreloadInterstitialText = "Enable Interstitial Autoreload";

	void Start()
	{
		version = AATKitBinding.GetVersion();
	}

	void OnGUI()
	{
		GUI.skin.label.fontSize = 40;
		GUI.skin.button.fontSize = 30;

		buttonWidth = Screen.width / 2;
		buttonHeight = buttonWidth / 4;

		// Add back button
		if(GUI.Button (new Rect (10, 10, 200, 100), "BACK"))
		{
			currentState = DemoState.Init;
		}

		// Show aatkit version
		GUI.Label(new Rect(250, 35, Screen.width-260, 45), "AATKit Unity " + version);



		/*
		 * buttons actions
		 */

		if (currentState == DemoState.Init)
		{
//			nativeView.SetActive (false);

			if (GUI.Button (new Rect (Screen.width / 2 - (buttonWidth / 2), 140, buttonWidth, buttonHeight), "Init AATKit"))
			{
				if (!initialized)
				{
                    AATKitConfiguration aatkitConfiguration = new AATKitConfiguration
                    {
                        TestModeAccountId = 74
                    };

                    AATKitBinding.Init(gameObject.name, aatkitConfiguration);
					Vector2 size = AATKitBinding.CreatePlacement (bannerPlacement, AATKitBinding.PlacementSize.BannerAuto);
					Vector2 size2 = AATKitBinding.CreatePlacement (interstitialPlacement, AATKitBinding.PlacementSize.Fullscreen);
					AATKitBinding.CreatePlacement (multiSizePlacement, AATKitBinding.PlacementSize.MultiSizeBanner);

					Debug.Log ("Banner size: " + size.x + " x " + size.y);
					Debug.Log ("Fullscreen size: " + size2.x + " x " + size2.y);
					
					initialized = true;
				}
			}

			if (GUI.Button (new Rect (Screen.width / 2 - (buttonWidth / 2), buttonHeight + 150, buttonWidth, buttonHeight), "Enable Debug Log"))
			{
				if (initialized)
				{
					AATKitBinding.SetDebugEnabled ();
				}
			}

			if (GUI.Button (new Rect (Screen.width / 2 - (buttonWidth / 2), buttonHeight * 2 + 160, buttonWidth, buttonHeight), shakeText))
			{
				if (initialized)
				{
					if (shakeEnabled)
					{
						AATKitBinding.SetDebugShakeEnabled (false);
						shakeEnabled = false;
						shakeText = "Enable Debug Shake";
					}
					else
					{
						AATKitBinding.SetDebugShakeEnabled (true);
						shakeEnabled = true;
						shakeText = "Disable Debug Shake";
					}
				}
			}

			if (GUI.Button (new Rect (Screen.width / 2 - (buttonWidth / 2), buttonHeight * 3 + 200, buttonWidth, buttonHeight), "BANNER"))
			{
				currentState = DemoState.Banner;
			}
				
			if (GUI.Button (new Rect (Screen.width / 2 - (buttonWidth / 2), buttonHeight * 4 + 220, buttonWidth, buttonHeight), "FULLSCREEN"))
			{
				currentState = DemoState.Fullscreen;
			}

			if (GUI.Button (new Rect (Screen.width / 2 - (buttonWidth / 2), buttonHeight * 5 + 230, buttonWidth, buttonHeight), "MULTISIZE"))
			{
				currentState = DemoState.Multisize;
			}
		}




		if (currentState == DemoState.Banner)
		{
			if (GUI.Button (new Rect (Screen.width / 2 - (buttonWidth / 2), 140, buttonWidth, buttonHeight), "Reload Banner"))
			{
				if (initialized)
				{
					AATKitBinding.ReloadPlacement (bannerPlacement);
				}
			}

			if (GUI.Button (new Rect (Screen.width / 2 - (buttonWidth / 2), buttonHeight + 150, buttonWidth, buttonHeight), autoreloadBannerText))
			{
				if (initialized)
				{
					if (bannerAutoreload)
					{
						AATKitBinding.StopPlacementAutoReload (bannerPlacement);
						bannerAutoreload = false;
						autoreloadBannerText = "Enable Banner Autoreload";
					}
					else
					{
						AATKitBinding.StartPlacementAutoReload (bannerPlacement);
						bannerAutoreload = true;
						autoreloadBannerText = "Disable Banner Autoreload";
					}
				}
			}

			if (GUI.Button (new Rect (Screen.width / 2 - (buttonWidth / 2), buttonHeight * 2 + 160, buttonWidth, buttonHeight), "Change Banner Alignment"))
			{
				if (initialized)
				{
					bannerAlignment = GetNewAlignment (bannerAlignment);
					AATKitBinding.SetPlacementAlignment (bannerPlacement, bannerAlignment);
				}
			}
		}

		if (currentState == DemoState.Fullscreen)
		{
			if (GUI.Button (new Rect (Screen.width / 2 - (buttonWidth / 2), 140, buttonWidth, buttonHeight), "Reload Interstitial"))
			{
				if (initialized)
				{
					AATKitBinding.ReloadPlacement (interstitialPlacement);
				}
			}

			if (GUI.Button (new Rect (Screen.width / 2 - (buttonWidth / 2), buttonHeight + 150, buttonWidth, buttonHeight), autoreloadInterstitialText))
			{
				if (initialized)
				{
					if (interstitialAutoreload)
					{
						AATKitBinding.StopPlacementAutoReload (interstitialPlacement);
						interstitialAutoreload = false;
						autoreloadInterstitialText = "Enable Interstitial Autoreload";
					}
					else
					{
						AATKitBinding.StartPlacementAutoReload (interstitialPlacement);
						interstitialAutoreload = true;
						autoreloadInterstitialText = "Disable Interstitial Autoreload";
					}
				}
			}

			if (GUI.Button (new Rect (Screen.width / 2 - (buttonWidth / 2), buttonHeight * 2 + 160, buttonWidth, buttonHeight), "Show Interstitial"))
			{
				if (initialized)
				{
					AATKitBinding.ShowPlacement (interstitialPlacement);
				}
			}
		}

		if (currentState == DemoState.Multisize) 
		{
			if (GUI.Button (new Rect (Screen.width / 2 - (buttonWidth / 2), 140, buttonWidth, buttonHeight), "Reload MultiSize"))
			{
				if (initialized)
				{
					AATKitBinding.ReloadPlacement (multiSizePlacement);
				}
			}

			if (GUI.Button (new Rect (Screen.width / 2 - (buttonWidth / 2), buttonHeight + 150, buttonWidth, buttonHeight), autoreloadMultiSizeText))
			{
				if (initialized)
				{
					if (multiSizeAutoreload)
					{
						AATKitBinding.StopPlacementAutoReload (multiSizePlacement);
						multiSizeAutoreload = false;
						autoreloadMultiSizeText = "Enable Multi Size Banner Autoreload";
					}
					else
					{
						AATKitBinding.StartPlacementAutoReload (multiSizePlacement);
						multiSizeAutoreload = true;
						autoreloadMultiSizeText = "Disable Multi Size Banner Autoreload";
					}
				}
			}

			if (GUI.Button (new Rect (Screen.width / 2 - (buttonWidth / 2), buttonHeight * 2 + 160, buttonWidth, buttonHeight), "Change MultiSize Alignment"))
			{
				if (initialized)
				{
					multiSizeBannerAlignment = GetNewAlignment (multiSizeBannerAlignment);
					AATKitBinding.SetMultiSizeAlignment(multiSizePlacement, multiSizeBannerAlignment);
				}
			}
		}
	}

	AATKitBinding.BannerAlignment GetNewAlignment (AATKitBinding.BannerAlignment alignment)
	{
		AATKitBinding.BannerAlignment newBannerAlignment = alignment;
		switch (alignment) {
		case AATKitBinding.BannerAlignment.BottomCenter:
			newBannerAlignment = AATKitBinding.BannerAlignment.BottomRight;
			break;
		case AATKitBinding.BannerAlignment.BottomRight:
			newBannerAlignment = AATKitBinding.BannerAlignment.BottomLeft;
			break;
		case AATKitBinding.BannerAlignment.BottomLeft:
			newBannerAlignment = AATKitBinding.BannerAlignment.TopCenter;
			break;
		case AATKitBinding.BannerAlignment.TopCenter:
			newBannerAlignment = AATKitBinding.BannerAlignment.TopRight;
			break;
		case AATKitBinding.BannerAlignment.TopRight:
			newBannerAlignment = AATKitBinding.BannerAlignment.TopLeft;
			break;
		case AATKitBinding.BannerAlignment.TopLeft:
			newBannerAlignment = AATKitBinding.BannerAlignment.BottomCenter;
			break;
		}
		return newBannerAlignment;
	}		

	/*
	 * aatkit events
	 * */
	public void OnHaveAd(string placementName) 
	{
		Debug.Log("onHaveAd event: " + placementName);
	}

	public void OnHaveAdOnMultiSizeBanner(string placementName) 
	{
		Debug.Log("onHaveAdOnMultiSizeBanner event: " + placementName);
	}
	
	public void OnNoAd(string placementName) 
	{
		Debug.Log("onNoAd event: " + placementName);
	}
	
	public void OnPauseForAd(string placementName) 
	{
		Debug.Log("onPauseForAd event: " + placementName);
	}
	
	public void OnResumeAfterAd(string placementName) 
	{
		Debug.Log("onResumeAfterAd event: " + placementName);
	}
	
	public void OnShowingEmpty(string placementName) 
	{
		Debug.Log("onShowingEmpty event: " + placementName);
	}

	public void OnUserEarnedIncentive(string placementName) 
	{
		Debug.Log("onUserEarnedIncentive event: " + placementName);
	}

	public void OnObtainedAdRules(string fromTheServer) 
	{
		Debug.Log("onObtainedAdRules event: " + fromTheServer);
	}
}
