#include "widget.h"
#include "ui_widget.h"
#include "mydialog.h"

Widget::Widget(QWidget *parent) :
    QWidget(parent),
    ui(new Ui::Widget)
{
    ui->setupUi(this);
    MyDialog *dlg = new MyDialog(this);
    connect(dlg, SIGNAL(dlgReturn(int)), this, SLOT(showValue(int)));
    dlg->show();
    //发射信号
    emit dlg->dlgReturn(10);
}

Widget::~Widget()
{
    delete ui;
}
