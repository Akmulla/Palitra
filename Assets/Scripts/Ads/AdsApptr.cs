using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsApptr : MonoBehaviour
{

    const string bannerId = "BannerPlacement";
    const string interstitialId = "FullscreenPlacement";
    const string multiSizeId = "MultiSizePlacement";
    const string fsBanner = "banner";

    void Start()
    {
        AATKitBinding.Init(this.gameObject.name);

        // banner placement
        AATKitBinding.CreatePlacement(bannerId, AATKitBinding.PlacementSize.BannerAuto);
        AATKitBinding.StartPlacementAutoReload(bannerId);

        // fullscreen placement
        AATKitBinding.CreatePlacement(interstitialId, AATKitBinding.PlacementSize.Fullscreen);
        AATKitBinding.StartPlacementAutoReload(interstitialId);

        //multi size placement
        AATKitBinding.CreatePlacement(multiSizeId, AATKitBinding.PlacementSize.MultiSizeBanner);
        AATKitBinding.StartPlacementAutoReload(multiSizeId);

        AATKitBinding.ShowPlacement(fsBanner);
    }

    public void ShowBanner()
    {
        AATKitBinding.ShowPlacement(bannerId);
    }

    public void ShowFullscreen()
    {
        AATKitBinding.ShowPlacement(interstitialId);
    }

    public void ShowMultiSize()
    {
        AATKitBinding.ShowPlacement(multiSizeId);
    }
}
