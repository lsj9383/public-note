#ifndef MYBUTTON_H
#define MYBUTTON_H

#include <QPushButton>
#include <QString>

class MyButton : public QPushButton
{
public:
    MyButton(QWidget *parent = 0):QPushButton(parent){ }
    QString getName(){
        return "my button!";
    }
};

#endif // MYBUTTON_H
