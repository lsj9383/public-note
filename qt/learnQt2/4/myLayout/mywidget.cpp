#include "mywidget.h"
#include "ui_mywidget.h"
#include <QHBoxLayout>
#include <QGridLayout>

myWidget::myWidget(QWidget *parent) :
    QWidget(parent),
    ui(new Ui::myWidget)
{
    ui->setupUi(this);
/*
    QHBoxLayout *layout = new QHBoxLayout;  //水平布局管理器
    layout->addWidget(ui->fontComboBox);
    layout->addWidget(ui->textEdit);
    layout->setSpacing(50);                 //设置部件间隔
    layout->setContentsMargins(0,0,50,100); //设置部件管理器到边界距离
    this->setLayout(layout);                //当前窗体设置布局
*/
    QGridLayout *layout = new QGridLayout;
    //从几行几列开始，占据几行几列
    layout->addWidget(ui->fontComboBox, 0, 0, 1, 2);
    layout->addWidget(ui->pushButton, 0, 2, 1, 1);
    layout->addWidget(ui->textEdit, 1, 0, 1, 3);
    this->setLayout(layout);
}

myWidget::~myWidget()
{
    delete ui;
}
