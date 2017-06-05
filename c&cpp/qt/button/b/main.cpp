#include "mainwindow.h"
#include <QApplication>

/*************
按钮空间测试：
点击按钮，按钮上的数字累计
**************/

int main(int argc, char *argv[])
{
    QApplication a(argc, argv);
    MainWindow w;
    w.show();

    return a.exec();
}
