#include "mainwindow.h"
#include "ui_mainwindow.h"

/*
透明窗体
*/

MainWindow::MainWindow(QWidget *parent) :
    QMainWindow(parent),
    ui(new Ui::MainWindow)
{
    ui->setupUi(this);

    //设置窗体图标
    this->setWindowTitle("Qt");
    this->setWindowIcon(QIcon(":/new/prefix1/ico"));

    //载入图片并且 设置窗体透明
    this->setWindowFlags(Qt::FramelessWindowHint);
    this->setAttribute(Qt::WA_TranslucentBackground, true);
    this->setStyleSheet("background-image:url(:/new/prefix1/ico);background-repeat:no-repeat");
}

MainWindow::~MainWindow()
{
    delete ui;
}
