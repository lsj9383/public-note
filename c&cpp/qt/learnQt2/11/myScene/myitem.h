#ifndef MYITEM_H
#define MYITEM_H

#include<QGraphicsItem>
#include<QPainter>

/*
 * boundingRect paint都是QGraphicsItem中的纯虚公共函数
 * boundingRect 返回图像的外部边界定义为一个矩形,所有的绘图操作都必须限制在图形项的边界矩形中
 * paint被QGraphicsView调用，用来在本地坐标中绘制图形项中的内容。
*/
class MyItem:public QGraphicsItem
{
public:
    MyItem(){    }
    QRectF boundingRect() const{
        qreal penWidth = 1;
        return QRectF(0-penWidth/2, 0-penWidth/2, 20+penWidth, 20+penWidth);
    }
    void paint(QPainter *painter, const QStyleOptionGraphicsItem *option, QWidget *widget){
        painter->setBrush(Qt::red);
        painter->drawRect(0,0,20,20);
    }
};

#endif // MYITEM_H
