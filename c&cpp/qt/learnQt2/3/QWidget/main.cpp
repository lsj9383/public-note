
#include<QtGui>
#include<QApplication>
#include<QWidget>
#include<QLabel>

int main(int argc, char *argv[])
{
    QApplication a(argc, argv);

    //设置窗口, 父窗口是0，所以这是个窗口
    QWidget *widget = new QWidget(0, Qt::Dialog);
    widget->setWindowTitle("i'm widget");

    //设置label, 父窗口是0，所以这是个窗口
    QLabel *label=new QLabel(0);
    label->setWindowTitle("i'm label");
    label->setText("label:i'm window");
    label->resize(180, 20);

    //设置label2, 付出口是widget，因此不是窗口
    QLabel *label2=new QLabel(widget);
    label2->setText("i'm not window, and is a sub componment");
    label2->resize(250, 20);

    label->show();
    widget->show();


    int ret = a.exec();
    delete label;
    delete widget;
    return ret;
}
