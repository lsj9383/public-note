#include "widget.h"
#include "ui_widget.h"

Widget::Widget(QWidget *parent) :
    QWidget(parent),
    ui(new Ui::Widget)
{
    ui->setupUi(this);
    dlg = new myDialog(this);
}

Widget::~Widget()
{
    delete ui;
}

void Widget::on_pushButton_clicked()
{
    if(dlg==NULL)
    {
        dlg = new myDialog(this);
    }
    dlg->show();
}

void Widget::on_pushButton_2_clicked()
{
    dlg->close();
    dlg=NULL;
}

void Widget::on_pushButton_3_clicked()
{
    dlg->accept();
}
