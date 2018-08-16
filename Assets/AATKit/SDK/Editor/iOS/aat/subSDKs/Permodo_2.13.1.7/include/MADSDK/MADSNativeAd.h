//
//  MADSNativeAd.h
//  MADSDK
//
//  Created by Владислав Рябов on 28.12.15.
//  Copyright © 2015 Владислав Рябов. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
#import "MADSNativeAdAsset.h"
#import "MADSNativeAdDataAsset.h"
#import "MADSNativeAdImageAsset.h"
#import "MADSNativeAdVideoAsset.h"

@class MADSNativeAd;

@protocol MADSNativeAdDelegate <NSObject>

@optional
/** Sent after an ad has been downloaded and rendered.
 
 @param nativeAd The MADSNativeAd instance sending the message.
 */
- (void)MADSNativeAdDidRecieveAd:(MADSNativeAd *)nativeAd;


/** Sent if an error was encountered while downloading or rendering an ad.
 
 @param nativeAd The MADSNativeAd instance sending the message.
 @param error The error encountered while attempting to receive or render the ad.
 */
- (void)MADSNativeAd:(MADSNativeAd*)nativeAd didFailToReceiveAdWithError:(NSError*)error;

/*!
 @method
 
 @abstract
 Sent after an ad has been clicked by the person.
 
 @param nativeAd An MADSNativeAd object sending the message.
 */
- (void)MADSNativeAdAdDidClick:(MADSNativeAd *)nativeAd;


/*!
 @method
 
 @abstract before presenting modal video view controller
 
 @param nativeAd An MADSNativeAd object sending the message.
 */
- (void)MADSNativeAdAdWillPresentFullscreenVideoAd:(MADSNativeAd *)nativeAd;


/*!
 @method
 
 @abstract sent after dissmis modal video view controller
 
 @param nativeAd An MADSNativeAd object sending the message.
 */
- (void)MADSNativeAdAdDidDismissFullscreenVideoAd:(MADSNativeAd *)nativeAd;


@end


/*!
 @class MADSNativeAd
 
 @abstract
 The MADSNativeAd represents ad metadata to allow you to construct custom ad views.
 */
@interface MADSNativeAd : NSObject

//setters
///---------------------------------------------------------------------------------------
/// @name Required configuration
///---------------------------------------------------------------------------------------


/** Specifies the publication id for the ad network.
 */
@property (nonatomic, assign) NSString *publicationId;

 ///  Updates ad assets
-(void) update;

///ID of placement
@property (strong, nonatomic) NSString* placementID;

/*!
 @method -trackViewForInteractions:withViewController
 @param view - Native Ad View where all native ad components are rendered
 @param viewController - Viewcontroller on which native ad is placed
 @discussion - Method to be called once native ad is sucessfully rendered for sending sucess metric url
 and hence handling user clicks
 */
-(void)trackViewForInteractions:(UIView*)view withViewController:(UIViewController* )viewController;

/*!
 @method -loadInImageView:withURL
 @param imageView - Image View where image is to be rendered
 @param urlString - URL of image which is to be rendered
 @discussion - Method will asyncronously downlsoad and hence render image in imageView.
 This method can be used for rendering icon and cover image
 */
-(void) loadInImageView:(UIImageView *)imageView withURL:(NSString *) urlString;


/*!
 @method - sendImpressionTrackers
 @abstract - Method will be used to send impression tracker metrics url. This is actually done automatically when trackViewForInteractions:withViewController method is called. This method will be used only when adapters are
 not used and publisher wants to himself implement third party mediation */
-(void) sendImpressionTrackers;

/*!
 @method - sendClickTracker
 @discussion - Method will be used to send click tracker metrics url. This is actually done automatically when trackViewForInteractions:withViewController method is called. This method will be used only when adapters are
 not used and publisher wants to himself implement third party mediation */
-(void) sendClickTracker;

/*!
 @method - nativeAdAdDidClick
 @discussion - Method will be used to make nativeAdAdDidClick. */
-(void)nativeAdAdDidClick;

/*!
 @method - playVideo
 @param controller - view controller to present modal video view controller
 @discussion - Method to play video.
 */
-(void)playVideoInViewController:(UIViewController *)controller;

/*!
 @property
 @abstract the delegate
 */
@property (nonatomic, assign) id<MADSNativeAdDelegate> delegate;

/*!
 @property
 
 @description Add MADSNativeAdImageAsset or/and MADSNativeAdDataAsset objects to this array, to load them. Title loads by default.
 */
@property (nonatomic,strong) NSMutableArray<MADSNativeAdAsset *> *assetsToLoad;

// Use these properties only if you want to override the configuration made in UI
@property (nonatomic,assign) NSUInteger nativeAdTitleLength;


///---------------------------------------------------------------------------------------
/// @name Response params , readonly params from response
///---------------------------------------------------------------------------------------

//getters
@property (nonatomic, strong, readonly) NSString *title;
@property (nonatomic, strong, readonly) NSString *adType;
@property (nonatomic, strong, readonly) NSString *adSubType;
@property (nonatomic, strong, readonly) NSString *creativeid;
@property (nonatomic, strong, readonly) NSString *adDescription;
@property (nonatomic, strong, readonly) NSString *callToAction;
@property (nonatomic, strong, readonly) NSString *iconImageURL;
@property (nonatomic, strong, readonly) NSString *mainImageURL;
@property (nonatomic, strong, readonly) NSString *logoImageURL;
@property (nonatomic, strong, readonly) NSNumber *rating;
@property (nonatomic, strong, readonly) NSString *adDescription2;
@property (nonatomic, assign, readonly) NSNumber *likes;
@property (nonatomic, strong, readonly) NSString *sponsored;
@property (nonatomic, strong, readonly) NSNumber *downloads;
@property (nonatomic, strong, readonly) NSNumber *price;
@property (nonatomic, strong, readonly) NSNumber *salePrice;
@property (nonatomic, strong, readonly) NSString *phone;
@property (nonatomic, strong, readonly) NSString *address;
@property (nonatomic, strong, readonly) NSString *displayURL;
@property (nonatomic, strong, readonly) NSString *vasttag;

/** Instructs the ad server to return test ads for the configured zone.
 
 @warning This should never be set to `YES` for application releases.
 */
@property (nonatomic, assign) BOOL test;

#pragma mark Unit testing methods

-(NSString *)jsonStringFromParamsDictForTesting;

-(void)loadAssetsFromJSONString:(NSString *)jsonString;

@end
