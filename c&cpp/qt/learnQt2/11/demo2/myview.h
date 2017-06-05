#ifndef MYVIEW_H
#define MYVIEW_H

#include <QGraphicsView>
#include <QGraphicsScene>
#include <QGraphicsProxyWidget>
#include "myitem.h"
#include <QDebug>
#include <QMouseEvent>
#include <QTimer>
#define itemN 151

class myView : public QGraphicsView
{
    Q_OBJECT
    /* slot */
private slots:
    void timerSLOT(void){
        for(int i=itemN-1; i>0; i--)
        {
            myItem *it=item[i];
            it->setPos(item[i-1]->pos());
        }
        item[0]->setPos(posAtScene);
    }
public:
    myView(QWidget *parent=0):QGraphicsView(parent), scene(new QGraphicsScene),
        posAtScene(QPoint(150, 150)){
        //scene default
        scene->setSceneRect(0, 0, 400, 300);
        for(int i=0; i<itemN; i++)
        {
            item[i] = new myItem(QColor(qrand()%256,qrand()%256,qrand()%256));
            item[i]->setPos(150, 150);
            scene->addItem(item[i]);
        }

        //item default


        //view default
        this->setScene(scene);
        timer = new QTimer;
        connect(timer, SIGNAL(timeout()), this, SLOT(timerSLOT()));
        timer->start(1);
    }

    /* member var */
private:
    QGraphicsScene *scene;
    myItem *item[itemN];
    QTimer *timer;
    QPointF posAtScene;

    /* event */
    void mouseMoveEvent(QMouseEvent *event){
        posAtScene = mapToScene(event->pos());
        qDebug()<<posAtScene<<endl;
    }
};

#endif // MYVIEW_H
