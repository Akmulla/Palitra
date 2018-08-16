//
//  MADSNativeAsset.h
//  MADSDK
//
//  Created by Владислав Рябов on 14.01.16.
//  Copyright © 2016 Владислав Рябов. All rights reserved.
//

#import <Foundation/Foundation.h>

/*!
 @class MADSNativeAd
 
 @abstract
 The MADSNAtiveAsset is abstract class, represents asset metadata.
 */
@interface MADSNativeAdAsset : NSObject

@property (nonatomic) NSUInteger identifier;
@property (nonatomic) BOOL required;

@end
