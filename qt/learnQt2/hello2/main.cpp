/*
#include<QApplication>
#include<QDialog>
#include<QLabel>


int main(int argc, char *argv[])
{
    QApplication a(argc, argv);
    QDialog w;
    QLabel label(&w);

    label.setText("hello world!你好QT");
    w.show();

    return a.exec();
}
*/
#include"ui_hellodialog.h"

int main(int argc, char *argv[])
{
    QApplication a(argc, argv);
    QDialog w;

    Ui::hellodialog ui;             //通过该对象，生成指定界面！
    ui.setupUi(&w);                 //在w对话框上，生成指定界面！
    w.show();

    return a.exec();
}
