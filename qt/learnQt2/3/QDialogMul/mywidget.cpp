#include "mywidget.h"
#include "ui_mywidget.h"

myWidget::myWidget(QWidget *parent) :
    QWidget(parent),
    ui(new Ui::myWidget)
{
    ui->setupUi(this);
//    connect(ui->showChildButton, SIGNAL(clicked()), this, SLOT(showChildDialog()));
}

myWidget::~myWidget()
{
    delete ui;
}

//手动关联
void myWidget::showChildDialog()
{
    QDialog *dialog = new QDialog(this);

    dialog->show();
    dialog->setWindowTitle("dialog111");
}

//自动关联
void myWidget::on_showChildButton_clicked()
{
    QDialog *dialog = new QDialog(this);

    dialog->show();
    dialog->setWindowTitle("dialog112");
}
