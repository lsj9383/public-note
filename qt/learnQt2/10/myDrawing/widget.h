#ifndef WIDGET_H
#define WIDGET_H

#include <QWidget>
#include <QPaintEvent>
#include <QPainter>

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

protected:
    void paintEvent(QPaintEvent *event){
        /*
        //将painter制定了绘图设备this
        QPainter painter(this);
        painter.drawLine(QPoint(0,0), QPoint(100, 100));
        */
        QPainter painter;
        painter.begin(this);        //painter和this绑定
        painter.setPen(QPen(Qt::green, 5, Qt::DotLine, Qt::RoundCap, Qt::RoundJoin));
        painter.drawLine(QPoint(0,0), QPoint(100, 100));
        painter.end();              //painter和this解除绑定
    }
};

#endif // WIDGET_H
