#include "mywidget.h"
#include "ui_mywidget.h"

#include <QColorDialog>
#include <QFileDialog>
#include <QFontDialog>
#include <QInputDialog>
#include <QDialog>
#include <QMessageBox>

#include <QDebug>

myWidget::myWidget(QWidget *parent) :
    QWidget(parent),
    ui(new Ui::myWidget)
{
    ui->setupUi(this);
}

myWidget::~myWidget()
{
    delete ui;
}

void myWidget::on_pushButton_clicked()
{
    QColor color = QColorDialog::getColor( Qt::red, this, "颜色对话框" );
    qDebug()<<color<<endl;
}

void myWidget::on_pushButton_2_clicked()
{
    QString fileName = QFileDialog::getOpenFileName(this, "文件对话框", "F:");
    qDebug()<<fileName<<endl;
}

void myWidget::on_pushButton_3_clicked()
{
    bool ok=0;      //用于记住是否单击ok
    QFont font = QFontDialog::getFont(&ok, this);
    if (ok)
        ui->pushButton_3->setFont(font);
    else
        qDebug()<<"no message"<<endl;
}

void myWidget::on_pushButton_4_clicked()
{
    bool ok;        //记录是否ok
    QString string = QInputDialog::getText(this, "输入字符串对话框", "请输入用户名", QLineEdit::Normal, "admin", &ok);
    qDebug()<<(ok?string:QString("no message"))<<endl;

    int value = QInputDialog::getInt(this, "输入整数对话框", "0-120", 100, 0, 120, 10, &ok);
    qDebug()<<(ok?QString(value):QString("no message"))<<endl;

    QStringList items;
    items<<"itme1"<<"itme2";
    QString item = QInputDialog::getItem(this, "输入条目对话框", "请选择", items, 0, true, &ok);
    qDebug()<<(ok?item:QString("no message"))<<endl;
}


void myWidget::on_pushButton_5_clicked()
{
    int ret1 = QMessageBox::question(this, "question", "qt?", QMessageBox::Yes, QMessageBox::No);
    if(ret1 == QMessageBox::Yes) qDebug()<<"en"<<endl;
}
