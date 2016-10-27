#ifndef WIDGET_H
#define WIDGET_H

#include <QWidget>
#include <QTimerEvent>
#include <QDebug>

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
    int id1;
    int id2;

protected:
    void timerEvent(QTimerEvent *event){
        if(event->timerId() == id1){
            qDebug()<<"first timer!"<<endl;
        }
        else if(event->timerId() == id2){
            qDebug()<<"second timer!"<<endl;
        }
    }
protected slots:
    void timeUpdate(void){
        qDebug()<<"QTimer!"<<endl;
    }
};

#endif // WIDGET_H
