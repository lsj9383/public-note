#include "mainwindow2.h"
#include "ui_mainwindow2.h"

MainWindow2::MainWindow2(QWidget *parent) :
    QMainWindow(parent),
    ui(new Ui::MainWindow2)
{
    ui->setupUi(this);

    resize(600, 500);    //窗口大小设置为600*500
    pix = QPixmap(200, 200);
    pix.fill(Qt::white);
}

MainWindow2::~MainWindow2()
{
    delete ui;
}

void MainWindow2::paintEvent(QPaintEvent* event)
{
    //设置QPixmap缓冲区像素.(设置缓冲区像素时通过QPainter对象实现的)
    QPainter pp(&pix);  //pp是基于QPixmap的绘制器
    QPoint sp;
    QPoint ep;

    sp.setX(10);sp.setY(10);
    ep.setX(50);ep.setY(50);
    pp.drawLine(sp, ep);

    //显示缓冲区图像
    QPainter painter(this);
    painter.drawPixmap(0, 0, pix);
}
