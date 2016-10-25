#include<QApplication>
#include"dialog.h"

int main(int argc, char *argv[])
{
    QApplication a(argc, argv);
    dialog dl;

    dl.show();


    return a.exec();
}
