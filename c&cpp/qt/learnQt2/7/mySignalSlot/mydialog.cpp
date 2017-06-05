#include "mydialog.h"
#include "ui_mydialog.h"

MyDialog::MyDialog(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::MyDialog)
{
    ui->setupUi(this);
}

MyDialog::~MyDialog()
{
    delete ui;
}

void MyDialog::on_pushButton_clicked()
{
    int value = ui->spinBox->value();       //获取输入值
    //发射信号，注意，这个并不是pushButton发射的，而是dialog发射的。也就是点击按钮后，让dialog发射信号.
    emit dlgReturn(value);
    close();                                //关闭对话框
}
