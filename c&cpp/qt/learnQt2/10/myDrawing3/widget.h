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
        QPainter painter;

        painter.begin(this);
        painter.drawLine(QPoint(0,0), QPoint(100, 100));
        painter.end();
    }
    bool eventFilter(QObject *obj, QEvent *event);
};

#endif // WIDGET_H
