#include "mywidget.h"
#include "ui_mywidget.h"

myWidget::myWidget(QWidget *parent) :
    QWidget(parent),
    ui(new Ui::myWidget)
{
    ui->setupUi(this);
    ui->frame->setFrameShape(QFrame::Box);
    ui->frame->setFrameShadow(QFrame::Sunken);
}

myWidget::~myWidget()
{
    delete ui;
}
