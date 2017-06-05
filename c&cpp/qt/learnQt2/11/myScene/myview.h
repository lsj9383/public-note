#ifndef MYVIEW_H
#define MYVIEW_H

#include <QGraphicsView>
#include <QWidget>
#include <QMouseEvent>
#include <QDebug>
#include <QGraphicsItem>

class MyView : public QGraphicsView
{
public:
    MyView(QWidget *parent=0):QGraphicsView(parent){}
protected:
    void mousePressEvent(QMouseEvent *event){
        QPoint viewPos = event->pos();
        QPointF scenePos = mapToScene(viewPos);
        QGraphicsItem *item = scene()->itemAt(scenePos, QTransform());

        qDebug()<<"viewPos:"<<viewPos<<endl;
        qDebug()<<"scenePos:"<<scenePos<<endl;
        if(item){
            QPointF itemPos = item->mapFromScene(scenePos);
            qDebug()<<"itemPos:"<<itemPos<<endl;
        }
    }
};

#endif // MYVIEW_H
