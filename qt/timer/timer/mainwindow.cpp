#include "mainwindow.h"
#include "ui_mainwindow.h"

MainWindow::MainWindow(QWidget *parent) :
    QMainWindow(parent),
    ui(new Ui::MainWindow)
{
    ui->setupUi(this);

    //label初始化
    label = new QLabel(this);

    //timer初始化
    timer = new QTimer;
    connect(timer, SIGNAL(timeout()), this, SLOT(timerTime()));
    timer->start(1000);
}

MainWindow::~MainWindow()
{
    delete ui;
}

void MainWindow::timerTime(void)
{
    static int hour=0, min=0, sec=0;

    sec++;
    if (sec==60)
    {
        sec = 0;min++;
        if (min==60)
        {
            min=0;hour++;
            if (hour==24)   hour=0;
        }
    }

    label->setText(QString::number(hour)+":"+
                   QString::number(min)+":"+
                   QString::number(sec));
}
