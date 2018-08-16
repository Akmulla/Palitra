//
//  MADSBannerView.h
//  MADSDK
//
//  Created by Владислав Рябов on 09.11.15.
//  Copyright © 2015 Владислав Рябов. All rights reserved.
//

#import <UIKit/UIKit.h>
@class MADSAdView;

typedef enum{
    ///Full screen interstitial ad
    MADSAdTypeInterstitial = 1,
    /// Banner ad
    MADSAdTypeBanner = 0
}MADSAdType;

@protocol MADSAdViewDelegate <NSObject>

-(void)MADSAdView:(MADSAdView *)bannerView didFailAdRequestWithError:(NSError *)error;

-(void)MADSAdViewDidRecieveAd:(MADSAdView *)adView;
-(void)MADSAdViewDidFinishLoading:(MADSAdView *)adView;

/*! Called just before your app will enter background or terminate
 * because the user tapped the ad, that will open Safari or AppStore.
 * The normal UIApplicationDelegate methods, like applicationWillEnterBackground:,
 * will be called immideately after this.
 */
-(void)MADSAdWillLeaveApplication:(MADSAdView *)ad;

@optional


-(void)MADSAdShouldClose:(MADSAdView *)ad;

/*! Called when the ad view is about to close.
 * when the user taps Safari Done button
 */
-(void)MADSAdDidClose:(MADSAdView *)ad;

//Videoplayer delegates
-(void)MADSAdViewVideoPlayerManagerWillPresentVideo;
-(void)MADSAdViewVideoPlayerManagerDidDismissVideo;
-(void)MADSAdViewVideoPlayerManagerdidFailToPlayVideoWithErrorMessage:(NSString*)message;
//Resize
-(void)MADSAdView:(MADSAdView *)adView willResizeToFrame:(CGRect)frame;
-(void)MADSAdView:(MADSAdView *)adView didResizeToFrame:(CGRect)frame;
-(void)MADSAdViewDidResizedClose:(MADSAdView *)adView;
//Expand
-(void)MADSAdViewDidExpand:(MADSAdView *)adView;
-(void)MADSAdViewDidExpandClose:(MADSAdView *)adView;
@end

@interface MADSAdView : UIView

@property (weak,nonatomic) id<MADSAdViewDelegate> delegate;
@property (nonatomic, assign) MADSAdType placementType;
///ID of publication zone
@property (strong, nonatomic) NSString* publicationID;

///ID of placement
@property (strong, nonatomic) NSString* placementID;

@property (nonatomic) NSTimeInterval updateTimeInterval;

/*!
 @brief Dimension w/h strict or not. No by default.
 @param YES Ad creative strictly equals size of adView
 @param NO Ad creative can be smaller that size of adView
 */
@property BOOL dimesionStrict;

/// Request ad from server and renders it if success.
-(void)requestAd;

///Loads empty HTML in adWebView
-(void)clearAd;

/// Unregister url protocol that inject mraid.js in ad on request
+(void)unregisterMADSUrlProtocol;

@end
