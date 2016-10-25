#include <QApplication>
#include "myview.h"

int main(int argc, char *argv[]){
    QApplication a(argc, argv);

    QWidget mywidget;

    myView view(&mywidget);
    view.show();

    mywidget.show();

    return a.exec();
}
