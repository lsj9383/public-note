#include "mainwindow.h"
#include "ui_mainwindow.h"

MainWindow::MainWindow(QWidget *parent) :
    QMainWindow(parent),
    ui(new Ui::MainWindow)              //初始化ui对象
{
    ui->setupUi(this);

    //窗体标题
    this->setWindowTitle("Qt5 窗体应用");

    //设置窗体的最大最小尺寸, 若相同时，窗体大小不可改变
    this->setMaximumSize(300, 300);
    this->setMinimumSize(300, 300);

    //默认窗体位置
    this->move(100, 100);

    //背景颜色 红色
    this->setStyleSheet("background:red");
}

MainWindow::~MainWindow()
{
    delete ui;
}
