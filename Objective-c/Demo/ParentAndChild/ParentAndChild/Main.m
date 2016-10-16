//
//  Main.m
//  ParentAndChild
//
//  Created by Oliver on 16/10/16.
//  Copyright © 2016年 lsj. All rights reserved.
//

#import <Foundation/Foundation.h>


//*************************************
@interface Parent : NSObject
- (void)show;
@end

@implementation Parent

- (void) show
{
    NSLog(@"Parent..");
}
@end

//**************************************
@interface Child : Parent
- (void)show:(int)age;
@end

@implementation Child

- (void) show:(int)age
{
    NSLog(@"Child and %d yeas old...", age);
}
@end

//**************************************
int main()
{
    id object = [Child new];
    
    [object show];
    [object show:10];
    
    NSLog(@"Done!");
    
    return 0;
}
