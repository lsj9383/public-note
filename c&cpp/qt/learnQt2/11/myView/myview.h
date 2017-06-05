#ifndef MYVIEW_H
#define MYVIEW_H

#include <QKeyEvent>
#include <QDebug>
#include <QGraphicsView>
#include <QMouseEvent>
#include <QGraphicsItem>

class MyView : public QGraphicsView
{
public:
    MyView(){}

protected:
    void keyPressEvent(QKeyEvent *event){
        switch(event->key())
        {
        case Qt::Key_Plus:
            scale(1.2,1.2);
            break;
        case Qt::Key_Minus:
            scale(1/1.2,1/1.2);
            break;
        case Qt::Key_Right:
            rotate(30);
            break;
        default:
            break;
        }
        QGraphicsView::keyPressEvent(event);        //不执行view的keypress，则场景或图形项无法接受到该事件
    }

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
        QGraphicsView::mousePressEvent(event);      //执行父类event,以传递给|焦点|图形项
    }
};

#endif // MYVIEW_H
