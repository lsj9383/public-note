#include "mainwindow.h"
#include "ui_mainwindow.h"

MainWindow::MainWindow(QWidget *parent) :
    QMainWindow(parent),                    //委托父类的构造函数
    ui(new Ui::MainWindow)                  //初始窗口对象
{
    ui->setupUi(this);
//    ui->label->setText("shidea");
}

MainWindow::~MainWindow()
{
    delete ui;
}
