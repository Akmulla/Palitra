using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AAT_Ads : MonoBehaviour
{

    // const string bannerId = "ColorPalette_Banner";
    const string bannerId = "ColorPalette_Banner";
    const string interstitialId = "ColorPalette_Fullscreen";
    const string multiSizeId = "MultiSizePlacement";
    const string rewardId = "ColorPalette_Rewarded";

    void Start()
    {
        AATKitConfiguration aatkitConfiguration = new AATKitConfiguration
        {
            SimpleConsent = AATKitConfiguration.Consent.OBTAINED,
            ConsentRequired = true,

        };

        AATKitBinding.Init(gameObject.name, aatkitConfiguration);

        DontDestroyOnLoad(gameObject);


        // AATKitBinding.InitWithTestMode(this.gameObject.name,49);

        // banner placement
        //AATKitBinding.CreatePlacement(bannerId, AATKitBinding.PlacementSize.BannerAuto);
        //AATKitBinding.StartPlacementAutoReload(bannerId);

        // fullscreen placement
        AATKitBinding.CreatePlacement(interstitialId, AATKitBinding.PlacementSize.Fullscreen);
        AATKitBinding.StartPlacementAutoReload(interstitialId);

        AATKitBinding.CreatePlacement(rewardId, AATKitBinding.PlacementSize.Rewarded);
        AATKitBinding.StartPlacementAutoReload(rewardId);

        //multi size placement
        //AATKitBinding.CreatePlacement(multiSizeId, AATKitBinding.PlacementSize.MultiSizeBanner);
        //AATKitBinding.StartPlacementAutoReload(multiSizeId);

        AATKitBinding.PreparePromo();
        AATKitBinding.ShowPromo();
        //ShowBanner();
    }

    public void ShowBanner()
    {
        AATKitBinding.ShowPlacement(bannerId);
    }

    public void ShowFullscreen()
    {
        AATKitBinding.ShowPlacement(interstitialId);
    }

    public void ShowRewardVideo()
    {
        GameController.game_controller.continued = true;
        GameController.game_controller.Pause();
        AATKitBinding.ShowPlacement(rewardId);
    }

    public void OnUserEarnedIncentive(string placementName)
    {
        if (placementName==rewardId)
        {
            GameController.game_controller.ResumeForBanner();
        }
        else
        {
            GameController.game_controller.Continue();
        }
    }

    public void OnResumeAfterAd(string placementName)
    {
        GameController.game_controller.Continue();
    }

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

    public void OnShowingEmpty(string placementName)
    {
        Debug.Log("onShowingEmpty event: " + placementName);
    }

    public void OnObtainedAdRules(string fromTheServer)
    {
        bool value = fromTheServer == "true";
        Debug.Log("onObtainedAdRules event: " + fromTheServer);
    }

}