//
//  MADSNativeAdVideoAsset.h
//  MADSDK
//
//  Created by Владислав Рябов on 22.01.16.
//  Copyright © 2016 Владислав Рябов. All rights reserved.
//

#import "MADSNativeAdAsset.h"

@interface MADSNativeAdVideoAsset : MADSNativeAdAsset

@property (nonatomic) NSUInteger minDuration;
@property (nonatomic) NSUInteger maxDuration;
@property (strong,nonatomic) NSString *vasttag; //Vast XML

+(MADSNativeAdVideoAsset *) videoAssetWithMinDuration:(NSUInteger)min
                                          maxDuration:(NSUInteger)max
                                             required:(BOOL)required;

+(NSArray<NSString *> *)videoMimes;

@end
