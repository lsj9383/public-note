#include "widget.h"
#include "ui_widget.h"

Widget::Widget(QWidget *parent) :
    QWidget(parent),
    lineEdit(new myLineEdit(this)),
    ui(new Ui::Widget)
{
    ui->setupUi(this);
    lineEdit->installEventFilter(this);             //为lineEdit在父部件上安装事件过滤器
}

Widget::~Widget()
{
    delete ui;
}
