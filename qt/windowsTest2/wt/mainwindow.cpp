#include "mainwindow.h"
#include "ui_mainwindow.h"

MainWindow::MainWindow(QWidget *parent) :
    QMainWindow(parent),
    ui(new Ui::MainWindow)
{
    ui->setupUi(this);

    this->setWindowTitle("move window without frame");

    //无边框设定
    this->setWindowFlags(Qt::FramelessWindowHint);

    //按钮设定
    btClose = new QPushButton(this);
    btClose->setText("关闭");

    //设定按钮点击事件
    connect(btClose, SIGNAL(clicked()), this, SLOT(close()));
}

MainWindow::~MainWindow()
{
    delete ui;
}

//获取鼠标点击时窗体的位置
void MainWindow::mousePressEvent(QMouseEvent *e)
{
    last = e->globalPos();
}

void MainWindow::mouseMoveEvent(QMouseEvent *e)
{

    int dx = e->globalX() - last.x();
    int dy = e->globalY() - last.y();

    last = e->globalPos();

    move(x()+dx, y()+dy);
}

void MainWindow::mouseReleaseEvent(QMouseEvent *e)
{
    int dx = e->globalX() - last.x();
    int dy = e->globalY() - last.y();

    move(x()+dx, y()+dy);
}
