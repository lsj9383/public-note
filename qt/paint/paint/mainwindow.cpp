#include "mainwindow.h"
#include "ui_mainwindow.h"

MainWindow::MainWindow(QWidget *parent) :
    QMainWindow(parent),
    ui(new Ui::MainWindow)
{
    ui->setupUi(this);

    button = new QPushButton(this);
    button->setText("w2");
    connect(button, SIGNAL(clicked()), this, SLOT(buttonClick()));
}

MainWindow::~MainWindow()
{
    delete ui;
}

void MainWindow::buttonClick(void)
{
    w2.show();
}

void MainWindow::paintEvent(QPaintEvent* event)
{
    QPainter painter(this);
    QRect rect(50, 50, 100, 100);
    QPainterPath path;

    painter.drawRect(rect);                 //设置矩形
    painter.setPen(QColor(Qt::red));        //设置画笔
    painter.drawText(rect, Qt::AlignHCenter, "i love u");       //字符串显示在在矩形中，并且居中

    path.addRect(50, 50, 40, 40);           //画完矩形后，画笔位置在矩形左上角
    path.lineTo(200, 200);                  //在左上角位置画到新位置
    path.moveTo(70, 50);                    //移动画笔位置到新位置
    path.lineTo(220, 200);
    painter.drawPath(path);
}
