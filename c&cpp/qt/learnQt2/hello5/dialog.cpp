#include "dialog.h"
#include "ui_dialog.h"

dialog::dialog(QWidget *parent):QDialog(parent),ui(new Ui::Dialog)
{
    ui->setupUi(this);  //使用ui对当前窗体进行初始化
}

dialog::~dialog()
{
    delete ui;
}
