#include "mainwindow.h"
#include <QApplication>

/******************
多窗体程序：
按下按键打开新窗体
*******************/

int main(int argc, char *argv[])
{
    QApplication a(argc, argv);
    MainWindow w;
    w.show();

    return a.exec();
}
