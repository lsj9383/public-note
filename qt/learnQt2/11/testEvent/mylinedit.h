#ifndef MYLINEEDIT_H
#define MYLINEEDIT_H

#include <QLineEdit>
#include <QKeyEvent>
#include <QDebug>

class myLineEdit : public QLineEdit
{
public:
    myLineEdit(QWidget *parent=0):QLineEdit(parent){}
protected:
    void keyPressEvent(QKeyEvent *event){
        qDebug()<<"LineEdit keyPressedEvent"<<endl;
        QLineEdit::keyPressEvent(event);
    }

    bool event(QEvent *event){
        if(event->type() == QEvent::KeyPress){
            qDebug()<<"LineEdit event"<<endl;
        }
        return QLineEdit::event(event);
    }
};

#endif // MYLINEEDIT_H
