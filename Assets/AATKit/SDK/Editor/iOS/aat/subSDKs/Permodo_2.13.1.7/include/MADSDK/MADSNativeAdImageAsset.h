//
//  MADSNativeImageAsset.h
//  MADSDK
//
//  Created by Владислав Рябов on 14.01.16.
//  Copyright © 2016 Владислав Рябов. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "MADSNativeAdAsset.h"
#import <UIKit/UIKit.h>

/*!
 @enum MADSNativeAdImageType
 @abstract enumerates Image Types accordint to Opent RTB Standart
 */
typedef enum{
    MADSNativeAdImageType_icon = 1,
    MADSNativeAdImageType_logo = 2,
    MADSNativeAdImageType_main = 3
} MADSNativeAdImageType;

/*!
 @class MADSNativeImageAsset
 
 @abstract
 The MADSNativeImageAsset is subclass of MADSNativeAdAsset, represents image asset metadata.
 */
@interface MADSNativeAdImageAsset : MADSNativeAdAsset

@property (nonatomic) MADSNativeAdImageType type;
@property (nonatomic) CGSize size;
@property (nonatomic, strong) NSString* imageURL;

+(MADSNativeAdImageAsset *)assetWithType:(MADSNativeAdImageType)type
                                                 size:(CGSize)size
                                             required:(BOOL)required;

///Array of image mimes strings, supported image formats.
+(NSArray<NSString *> *)imageMimes;

@end
