//
//  MADSNativeDataAsset.h
//  MADSDK
//
//  Created by Владислав Рябов on 14.01.16.
//  Copyright © 2016 Владислав Рябов. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "MADSNativeAdAsset.h"

/*!
 @enum MADSNativeAdDataType
 @abstract enumerates Data Types accordint to Opent RTB Standart
 */
typedef enum{
    MADSNativeAdDataType_unknown = 0,
    MADSNativeAdDataType_sponsored = 1,
    MADSNativeAdDataType_description = 2,
    MADSNativeAdDataType_rating = 3,
    MADSNativeAdDataType_likes = 4,
    MADSNativeAdDataType_downloads = 5,
    MADSNativeAdDataType_price = 6,
    MADSNativeAdDataType_saleprice = 7,
    MADSNativeAdDataType_phone = 8,
    MADSNativeAdDataType_address = 9,
    MADSNativeAdDataType_description2 = 10,
    MADSNativeAdDataType_displayUrl = 11,
    MADSNativeAdDataType_callToAction = 12,
}MADSNativeAdDataType;

/*!
 @class MADSNativeDataAsset
 
 @abstract
 The MADSNativeDataAsset is subclass of MADSNativeAdAsset, represents data asset metadata.
 */
@interface MADSNativeAdDataAsset : MADSNativeAdAsset

@property (nonatomic) MADSNativeAdDataType type;
@property (nonatomic) NSUInteger length;
@property (nonatomic,strong) NSString* value;

+(MADSNativeAdDataAsset *)assetWithType:(MADSNativeAdDataType)type
                             length:(NSUInteger)length
                           required:(BOOL)required;

+(MADSNativeAdDataAsset *)assetWithType:(MADSNativeAdDataType)type
                               required:(BOOL)required;

@end
