#include "mainwindow.h"
#include <QApplication>

/************************
单行文本测试：
点击按钮，交换改变两文本中的内容
*************************/

int main(int argc, char *argv[])
{
    QApplication a(argc, argv);
    MainWindow w;
    w.show();

    return a.exec();
}
