#include "mainwindow.h"
#include "ui_mainwindow.h"

MainWindow::MainWindow(QWidget *parent) :
    QMainWindow(parent),
    ui(new Ui::MainWindow)
//    w2(this)                              //初始化表中，若把窗体2的父对象设为窗体1，则窗体1关闭时，窗体2也会关闭
{
    ui->setupUi(this);

    //初始化按钮
    button = new QPushButton(this);
    button->setGeometry(QRect(50, 50, 100, 25));
    button->setText("按钮");
    connect(button, SIGNAL(clicked()), this, SLOT(showMainwindow2()));
}

MainWindow::~MainWindow()
{
    delete ui;
}

void MainWindow::showMainwindow2(void)
{
    w2.show();
}
