#ifndef MYLINEEDIT_H
#define MYLINEEDIT_H

#include<QLineEdit>
#include<QWidget>
#include<QEvent>
#include<QDebug>

class myLineEdit:public QLineEdit
{
public:
    myLineEdit(QWidget *parent=0):QLineEdit(parent){ }

public:
    /* 父类也有这玩意儿，覆盖掉父类 */
    bool event(QEvent *event){
        if(event->type() == QEvent::KeyPress)
        {
            qDebug()<<"myLineEdit pressed"<<endl;
        }
        return QLineEdit::event(event);            //执行父类的event.
    }
};

#endif // MYLINEEDIT_H
