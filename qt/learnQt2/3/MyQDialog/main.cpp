#include "mywidget.h"
#include "mydialog.h"
#include <QApplication>

int main(int argc, char *argv[])
{
    QApplication a(argc, argv);
    myWidget w;
    myDialog dialog;

    if(dialog.exec()==QDialog::Accepted)
    {
        w.show();
        return a.exec();
    }

    return 0;
}
