#ifndef WIDGET_H
#define WIDGET_H

#include <QWidget>
#include <QMouseEvent>
#include <QApplication>
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
    QPoint offset;

protected:
    void mousePressEvent(QMouseEvent *event){
        if(event->button() == Qt::LeftButton){
            QCursor cursor;
            cursor.setShape(Qt::ClosedHandCursor);
            QApplication::setOverrideCursor(cursor);        //暂时覆盖, 在释放时 会放开
            offset = event->globalPos() - pos();            //

            qDebug()<<event->globalPos()<<endl;             //globalpos()是在屏幕上的位置
            qDebug()<<event->pos()<<endl;                   //pos()是在当前部件上的相对位置
        }
    }
    void mouseMoveEvent(QMouseEvent *event){
        if(event->buttons() & Qt::LeftButton){
            QPoint temp;
            temp = event->globalPos() - offset;
            move(temp);
        }
    }
    void mouseReleaseEvent(QMouseEvent *event){
        QApplication::restoreOverrideCursor();          //恢复
    }
    void mouseDoubleClickEvent(QMouseEvent *event){
        if(event->button() == Qt::LeftButton){
            if(windowState() != Qt::WindowFullScreen)
                setWindowState(Qt::WindowFullScreen);
            else
                setWindowState(Qt::WindowNoState);
        }
    }
};

#endif // WIDGET_H
