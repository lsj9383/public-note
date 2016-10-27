#ifndef MYITEM_H
#define MYITEM_H

#include <QGraphicsItem>
#include <QPainter>

class myItem : public QGraphicsItem
{
public:
    myItem(QColor cl=Qt::red, QGraphicsItem *parent=0):QGraphicsItem(parent), brushColor(cl){

    }

    QRectF boundingRect() const{
        return QRectF(-10-0.5, -10-0.5, 20+1, 20+1);
    }

    void paint (QPainter *painter, const QStyleOptionGraphicsItem *option, QWidget *widget){
        painter->setBrush(brushColor);
        painter->drawRect(-10, -10, 20, 20);
    }

private:
    QColor brushColor;

};

#endif // MYITEM_H
