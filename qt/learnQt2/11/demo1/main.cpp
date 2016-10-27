
#include <QApplication>
#include "myview.h"

int main(int argc, char *argv[])
{
    QApplication app(argc, argv);

    myView view;
    view.show();

    return app.exec();
}
