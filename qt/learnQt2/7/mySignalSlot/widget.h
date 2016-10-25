#ifndef WIDGET_H
#define WIDGET_H

#include <QWidget>
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

protected slots:
    void showValue(int val){
        qDebug()<<val<<endl;
    }
};

#endif // WIDGET_H
