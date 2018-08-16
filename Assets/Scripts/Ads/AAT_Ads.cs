using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AAT_Ads : MonoBehaviour
{

    const string bannerId = "Color_Banner";
    const string interstitialId = "FullscreenPlacement";
    const string multiSizeId = "MultiSizePlacement";

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

        AATKitBinding.ShowPlacement(bannerId);
    }
}
