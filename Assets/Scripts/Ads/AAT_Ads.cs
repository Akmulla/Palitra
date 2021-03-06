﻿using System.Collections;
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
        DontDestroyOnLoad(gameObject);

        
        // AATKitBinding.InitWithTestMode(this.gameObject.name,49);

        // banner placement
        AATKitBinding.CreatePlacement(bannerId, AATKitBinding.PlacementSize.BannerAuto);
        AATKitBinding.StartPlacementAutoReload(bannerId);

        // fullscreen placement
        AATKitBinding.CreatePlacement(interstitialId, AATKitBinding.PlacementSize.Fullscreen);
        AATKitBinding.StartPlacementAutoReload(interstitialId);

        //multi size placement
        AATKitBinding.CreatePlacement(multiSizeId, AATKitBinding.PlacementSize.MultiSizeBanner);
        AATKitBinding.StartPlacementAutoReload(multiSizeId);

        //AATKitBinding.PreparePromo();
        //AATKitBinding.ShowPromo(true);
        ShowBanner();
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
        AATKitBinding.CreatePlacement(rewardId, AATKitBinding.PlacementSize.Rewarded);
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

    public void ShowPromo()
    {
        //AATKitBinding.PreparePromo();
        //AATKitBinding.ShowPromo();
        //AATKitBinding.EnablePromo();
    }
    
}
