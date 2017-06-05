#ifndef MYITEM_H
#define MYITEM_H

#include <QGraphicsObject>
#include <QGraphicsItem>
#include <QPainter>
class MyItem : public QGraphicsObject
{
public:
    MyItem(QGraphicsItem *parent=0):QGraphicsObject(parent){}
    QRectF boundingRect() const{
        return QRectF(-10-0.5,-10-0.5,20+1,20+1);
    }
    void paint(QPainter *painter, const QStyleOptionGraphicsItem *option, QWidget *widget){
        painter->drawRect(-10,-10,20,20);
    }
};

#endif // MYITEM_H
