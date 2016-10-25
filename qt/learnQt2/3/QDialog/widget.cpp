#include "widget.h"
#include "ui_widget.h"
#include <QDialog>

Widget::Widget(QWidget *parent) :
    QWidget(parent),dialog(this),
    ui(new Ui::Widget)
{
    ui->setupUi(this);
/*
    //非模态
    QDialog *qd = new QDialog(this);
    qd->show();
*/

/*
    //模态
    QDialog qd(this);
    qd.exec();
*/
}

Widget::~Widget()
{
    delete ui;
}
