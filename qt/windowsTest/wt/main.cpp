#include "mainwindow.h"
#include <QApplication>

/******************
    窗体初始化设定：
    标题设定
    尺寸设定(固定)
    初始位置设定
    背景颜色设定
********************/

int main(int argc, char *argv[])
{
    QApplication a(argc, argv);
    MainWindow w;
    w.show();

    return a.exec();
}
