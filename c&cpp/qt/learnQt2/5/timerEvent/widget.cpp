#include "widget.h"
#include "ui_widget.h"
#include <QTimer>
#include <QTime>

Widget::Widget(QWidget *parent) :
    QWidget(parent),id1(0),id2(0),
    ui(new Ui::Widget)
{
    ui->setupUi(this);

    //使用startTimer!
//    id1 = startTimer(1000);
//    id2 = startTimer(2000);

    //使用 QTimer
    QTimer *qt = new QTimer(this);
    connect(qt, SIGNAL(timeout()), this, SLOT(timeUpdate()));
    qt->start(1000);
}

Widget::~Widget()
{
    delete ui;
}
