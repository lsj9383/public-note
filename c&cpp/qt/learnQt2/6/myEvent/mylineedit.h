#ifndef MYLINEEDIT_H
#define MYLINEEDIT_H

#include <QLineEdit>
#include <QDebug>
#include <QKeyEvent>

class myLineEdit : public QLineEdit
{
public:
    myLineEdit(QWidget *parent = 0):QLineEdit(parent){}
protected:
    //父类也有keyPressEvent， 这里将覆盖掉父类的行为
    void keyPressEvent(QKeyEvent *event){
        //
        qDebug()<<"myLineEdit pressed"<<endl;
        QLineEdit::keyPressEvent(event);            //调用父类处理函数,显示输入
        //将事件传至父部件(父部件，不是父类)
        event->ignore();
    }
};

#endif // MYLINEEDIT_H
