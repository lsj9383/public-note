#include<QApplication>
#include<QWidget>
#include<QDebug>

int main(int argc, char *argv[])
{
    QApplication a(argc, argv);

    QWidget *widget = new QWidget();
    widget->show();

    int x = widget->x();
    int y = widget->y();
    QRect geometry = widget->geometry();
    QRect frame = widget->frameGeometry();

    qDebug()<<"geometry: "<<geometry<<"frame: "<<frame<<endl;
    return a.exec();
}
