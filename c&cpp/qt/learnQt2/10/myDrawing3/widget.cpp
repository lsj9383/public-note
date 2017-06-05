#include "widget.h"
#include "ui_widget.h"
#include "QDebug"

Widget::Widget(QWidget *parent) :
    QWidget(parent),
    ui(new Ui::Widget)
{
    ui->setupUi(this);
    ui->label->installEventFilter(this);        //让this监听label的事件
    this->installEventFilter(this);             //让this监听自己的事件
}

Widget::~Widget()
{
    delete ui;
}

bool Widget::eventFilter(QObject *obj, QEvent *event)
{
    if(obj == ui->label)
    {
        if (event->type()==QEvent::Paint)
        {
            static int cnt=0;cnt++;
            qDebug()<<"sub paint times: "<<cnt<<endl;
            return true;            //不继续往子部件传递事件(部件的event接收不了)
        }
    }
    else if(obj == this)
    {
        if (event->type()==QEvent::Paint)
        {
            static int cnt=0;cnt++;
            qDebug()<<"Widget paint times: "<<cnt<<endl;
            return true;            //不继续往部件传递事件
        }
    }
    return false;
}
