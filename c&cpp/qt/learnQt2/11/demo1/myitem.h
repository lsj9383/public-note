#ifndef MYITEM_H
#define MYITEM_H

#include <QGraphicsObject>
#include <QPainter>

class myItem:public QGraphicsObject
{
public:
    myItem(QColor clp):QGraphicsObject(),brushColor(clp) {    }

    QRectF boundingRect() const{
        return QRectF(-10-0.5,-10-0.5,20+1,20+1);
    }
    void paint(QPainter *painter, const QStyleOptionGraphicsItem *option, QWidget *widget){
        painter->setBrush(brushColor);
        painter->drawRect(-10,-10,20,20);
    }

private:
    QColor brushColor;
};

#endif // MYITEM_H
