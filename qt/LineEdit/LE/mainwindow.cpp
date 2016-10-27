#include "mainwindow.h"
#include "ui_mainwindow.h"

MainWindow::MainWindow(QWidget *parent) :
    QMainWindow(parent),
    ui(new Ui::MainWindow)
{
    ui->setupUi(this);

    button = new QPushButton(this);
    button->setText("swap");
    connect(button, SIGNAL(clicked()), this, SLOT(SwapEdit()));

    edit1 = new QLineEdit(this);edit1->setGeometry(QRect(100, 100, 100, 20));edit1->setText("edit1");
    edit2 = new QLineEdit(this);edit2->setGeometry(QRect(100, 130, 100, 20));edit2->setText("edit2");
}

MainWindow::~MainWindow()
{
    delete ui;
}

void MainWindow::SwapEdit(void)
{
    QString buf;

    buf = edit2->text();                //复制对象
    edit2->setText(edit1->text());
    edit1->setText(buf);
}
