#include "mainwindow.h"
#include "ui_mainwindow.h"

MainWindow::MainWindow(QWidget *parent) :
    QMainWindow(parent),
    ui(new Ui::MainWindow)
{
    ui->setupUi(this);

    button = new QPushButton(this);
    button->setText("count++");
    connect(button, SIGNAL(clicked()), this, SLOT(labButton()));

    label = new QLabel(this);
    label->setGeometry(QRect(100, 100, 200, 30));
}

MainWindow::~MainWindow()
{
    delete ui;
}

void MainWindow::labButton(void)
{
    static int count = 0;

    label->setText(QString::number(count));
    count++;
}
