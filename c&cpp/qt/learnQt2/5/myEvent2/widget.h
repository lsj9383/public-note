#ifndef WIDGET_H
#define WIDGET_H

#include <QWidget>
#include <mylineedit.h>
#include <QKeyEvent>

namespace Ui {
class Widget;
}

class Widget : public QWidget
{
    Q_OBJECT

public:
    explicit Widget(QWidget *parent = 0);
    ~Widget();

private:
    Ui::Widget *ui;
    myLineEdit *lineEdit;

public:
    void keyPressEvent(QKeyEvent *event){
        qDebug()<<"widget pressed"<<endl;
    }

    bool eventFilter(QObject *obj, QEvent *event){
        if(obj==lineEdit){
            if(event->type() == QEvent::KeyPress){
                qDebug()<<"widget event filter"<<endl;
            }
        }
        return QWidget::eventFilter(obj, event);        //父类事件过滤器
    }
};

#endif // WIDGET_H
