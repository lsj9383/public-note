#include "mainwindow.h"
#include <QApplication>
//#include <QMotifStyle>

int main(int argc, char *argv[])
{
    QApplication a(argc, argv);
//    a.setStyle(new QMotifStyle);
    MainWindow w;
    w.show();

    return a.exec();
}
