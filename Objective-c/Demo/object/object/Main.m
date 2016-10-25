//
//  Main.m
//  object
//
//  Created by Oliver on 16/10/16.
//  Copyright © 2016年 lsj. All rights reserved.
//

#import <Foundation/Foundation.h>

typedef enum{sCIRCLE, sRECT, sEGG} ShapeType;
typedef enum{RED, GREEN, BLUE} ColorType;

typedef struct{int x; int y; int width; int height} RECT;

NSString *ColorName(ColorType type)
{
    switch(type)
    {
        case RED:
            return @"red";
            break;
        case GREEN:
            return @"green";
            break;
        case BLUE:
            return @"blue";
            break;
    }
}

//*************************************

@interface Circle :NSObject
{
    RECT bounds;
    ColorType color;
}
- (void)drow;
- (void)setFillColor:(ColorType)color;
- (void)setBounds:(RECT)rect;
@end

@implementation Circle
- (void)draw
{
    NSLog(@"draw a circle : %d %d %d %d with %@", bounds.x, bounds.y, bounds.width, bounds.height, ColorName(color));
}

- (void)setFillColor:(ColorType)color
{
    self->color = color;
}

- (void)setBounds:(RECT)rect
{
    self->bounds = rect;
}
@end

//************************************

@interface Egg :NSObject
{
    RECT bounds;
    ColorType color;
}
- (void)drow;
- (void)setFillColor:(ColorType)color;
- (void)setBounds:(RECT)rect;
@end

@implementation Egg
- (void)draw
{
    NSLog(@"draw a Egg : %d %d %d %d with %@", bounds.x, bounds.y, bounds.width, bounds.height, ColorName(color));
}

- (void)setFillColor:(ColorType)color
{
    self->color = color;
}

- (void)setBounds:(RECT)rect
{
    self->bounds = rect;
}
@end

//************

@interface Rectangle :NSObject
{
    RECT bounds;
    ColorType color;
}
- (void)drow;
- (void)setFillColor:(ColorType)color;
- (void)setBounds:(RECT)rect;
@end

@implementation Rectangle
- (void)draw
{
    NSLog(@"draw a rect : %d %d %d %d with %@", bounds.x, bounds.y, bounds.width, bounds.height, ColorName(color));
}

- (void)setFillColor:(ColorType)color
{
    self->color = color;
}

- (void)setBounds:(RECT)rect
{
    self->bounds = rect;
}
@end


int main()
{

    id shapes[3];
    
    RECT rect0 = {0, 0, 100, 30};
    RECT rect1 = {300, 400, 20, 20};
    RECT rect2 = {100, 200, 50, 30};
    
    shapes[0] = [Circle new];
    [shapes[0] setFillColor:RED];
    [shapes[0] setBounds:rect0];

    shapes[1] = [Rectangle new];
    [shapes[1] setFillColor:GREEN];
    [shapes[1] setBounds:rect1];
    
    shapes[2] = [Egg new];
    [shapes[2] setFillColor:BLUE];
    [shapes[2] setBounds:rect2];
    
    for(int i=0; i<3; i++)
    {
        [shapes[i] draw];
    }
    
    NSLog(@"done");
    return 0;
}
