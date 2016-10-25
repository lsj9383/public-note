#ifndef MYVIEW_H
#define MYVIEW_H

#include <QWidget>
#include <QGraphicsView>
#include <QGraphicsScene>
#include <QGraphicsProxyWidget>
#include "mylinedit.h"
#include <QKeyEvent>

class myView : public QGraphicsView
{
    /* construct and destory */
public:
    myView(QWidget *parent=0):QGraphicsView(parent),
        scene(new QGraphicsScene), lineEdit(new myLineEdit){

        scene->setSceneRect(-10, -10, 200, 300);
        QGraphicsProxyWidget *graphLineEdit = scene->addWidget(lineEdit);
        graphLineEdit->moveBy(10, 10);
        lineEdit->installEventFilter(this);
        this->installEventFilter(this);
        this->setScene(scene);
    }

    /* member var */
private:
    QGraphicsScene *scene;
    myLineEdit *lineEdit;

    /* event */
protected:
    void keyPressEvent(QKeyEvent *event){
        qDebug()<<"view have key pressed!"<<endl;
        QGraphicsView::keyPressEvent(event);
    }
    bool eventFilter(QObject *obj, QEvent * event){
        if(obj == lineEdit){
            if(event->type()==QEvent::KeyPress){
                qDebug()<<"event filter"<<endl;
                return false;
            }
        }
        else if(obj == this){
            if(event->type()==QEvent::KeyPress){
                qDebug()<<"this event filter"<<endl;
                return false;
            }
        }

    }
    void mousePressEvent(QMouseEvent *event){
        qDebug()<<"mouse"<<endl;
        QGraphicsView::mousePressEvent(event);
    }
    bool event(QEvent *event){
        if(event->type() == QEvent::KeyPress){
            qDebug()<<"view event"<<endl;
        }
        return QGraphicsView::event(event);
    }

};

#endif // MYVIEW_H
