#include "mainwindow.h"
#include "ui_mainwindow.h"

MainWindow::MainWindow(QWidget *parent) :
    QMainWindow(parent),
    ui(new Ui::MainWindow)
{
    ui->setupUi(this);

    pos1 = new QLabel(this);pos1->setGeometry(QRect(0, 20, 100, 20));pos1->setText("坐标1");
    pos2 = new QLabel(this);pos2->setGeometry(QRect(0, 70, 100, 20));pos2->setText("坐标2");
    pos3 = new QLabel(this);pos3->setGeometry(QRect(0, 120, 100, 20));pos3->setText("坐标3");

    posEdit1 = new QLineEdit(this);posEdit1->setGeometry(QRect(40, 20, 100, 20));posEdit1->setText("edit1");
    posEdit2 = new QLineEdit(this);posEdit2->setGeometry(QRect(40, 70, 100, 20));posEdit2->setText("edit2");
    posEdit3 = new QLineEdit(this);posEdit3->setGeometry(QRect(40, 120, 100, 20));posEdit3->setText("edit3");
}

MainWindow::~MainWindow()
{
    delete ui;
}

void MainWindow::mousePressEvent(QMouseEvent *e)
{
    QPoint op1, op2;

    op1 = e->globalPos();
    op2 = e->pos();

    posEdit1->setText(QString::number(op1.x())+","+QString::number(op1.y())+" "+
                      QString::number(op2.x())+","+QString::number(op2.y()));
}

void MainWindow::mouseMoveEvent(QMouseEvent *e)
{
    QPoint op1, op2;

    op1 = e->globalPos();
    op2 = e->pos();

    posEdit2->setText(QString::number(op1.x())+","+QString::number(op1.y())+" "+
                      QString::number(op2.x())+","+QString::number(op2.y()));
}

void MainWindow::mouseReleaseEvent(QMouseEvent *e)
{
    QPoint op1, op2;

    op1 = e->globalPos();
    op2 = e->pos();

    posEdit3->setText(QString::number(op1.x())+","+QString::number(op1.y())+" "+
                      QString::number(op2.x())+","+QString::number(op2.y()));
}
