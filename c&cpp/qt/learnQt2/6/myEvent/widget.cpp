#include "widget.h"
#include "ui_widget.h"
#include "mylineedit.h"

Widget::Widget(QWidget *parent) :
    QWidget(parent),
    ui(new Ui::Widget)
{
    ui->setupUi(this);

    myLineEdit *line = new myLineEdit(this);
    QLineEdit *qline = new QLineEdit(this);
    qline->move(50,50);
}

Widget::~Widget()
{
    delete ui;
}
