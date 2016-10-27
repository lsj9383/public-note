#ifndef WIDGET_H
#define WIDGET_H

#include <QWidget>
#include <QPaintEvent>
#include <QPainter>
#include <QPixmap>
#include <QImage>

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

        QPixmap qmap(100, 100);
        QImage qimg(100, 100, QImage::Format_ARGB32);
        QPainter painter;

        painter.begin(&qmap);
        painter.setPen(Qt::green);
        painter.drawLine(QPoint(0,0), QPoint(100,100));
        painter.end();

        painter.begin(&qimg);
        painter.drawLine(QPoint(0,0), QPoint(100,100));
        painter.end();

        painter.begin(this);
        painter.drawImage(0, 0, qimg);
        painter.drawPixmap(110, 110, qmap);
        painter.end();
    }
};

#endif // WIDGET_H
