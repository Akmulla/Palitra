//
//  MADSInterstitialAd.h
//  MADSDK
//
//  Created by Владислав Рябов on 12.11.15.
//  Copyright © 2015 Владислав Рябов. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "MADSAdView.h"

@class MADSInterstitialAd;
@class MADSAdModalViewController;
// Delegate for receiving state change messages from a MADSInterstitialAd.
@protocol MADSInterstitialDelegate <NSObject>

@optional

/* Called when interstitial ad request succeeded.
 * Show it at the next transition point in your app
 * such as transition between view controllers.
 */
-(void)MADSInterstitialDidRecieveAd:(MADSInterstitialAd *)ad;

-(void)MADSInterstitialDidFinishLoading:(MADSInterstitialAd *)ad;

/* Called when an interstitial ad failed to recieve an ad to show
 * Do not show the interstitial.
 */
-(void)MADSInterstitial:(MADSInterstitialAd *)ad didFailToReceiveAdWithError:(NSError *)error;

/* Called just before presenting an interstitial.
 * Stop animations and save state of your App, in case the user leaves your app during interstitial is on screen.
 */
-(void)MADSInterstitialWillPresentScreen:(MADSInterstitialAd *)ad;

-(void)MADSInterstitialWillDismissScreen:(MADSInterstitialAd *)ad;

-(void)MADSInterstitialDidDismissScreen:(MADSInterstitialAd *)ad;

/* Called just before your app will enter background or terminate 
 * because the user tapped the ad, that will open Safari or AppStore.
 * The normal UIApplicationDelegate methods, like applicationWillEnterBackground:,
 * will be called immideately after this.
 */
-(void)MADSInterstitialWillLeaveApplication:(MADSInterstitialAd *)ad;

/*! Called when the ad view is about to close.
 * when the user taps Safari Done button
 */
-(void)MADSInterstitialDidClose:(MADSInterstitialAd *)ad;
@end

@interface MADSInterstitialAd : NSObject

@property  (weak,nonatomic) id<MADSInterstitialDelegate> delegate;
@property (strong, readonly) MADSAdModalViewController* viewController;

///ID of publication zone
@property (strong, nonatomic) NSString *publicationID;

///ID of placement
@property (strong, nonatomic) NSString* placementID;

/*!
 @brief Dimension w/h strict or not. No by default.
 @param YES Ad creative strictly equals size of adView
 @param NO Ad creative can be smaller that size of adView
 */
@property BOOL dimesionStrict;

-(void)showInViewController:(UIViewController *)controller animated:(BOOL)animated;

-(void)requestAd;

@end
