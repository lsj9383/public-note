#include "mainwindow.h"
#include "ui_mainwindow.h"

MainWindow::MainWindow(QWidget *parent) :
    QMainWindow(parent),
    ui(new Ui::MainWindow)
{
    ui->setupUi(this);
    button = new QPushButton(this);

    connect(button, SIGNAL(clicked()), this, SLOT(txtButton()));       //连接按键released的事件到txtButton行为
}

MainWindow::~MainWindow()
{
    delete ui;
}

void MainWindow::txtButton(void)
{
    static int count_click=0;

    button->setText(QString::number(count_click));
    count_click++;
}
