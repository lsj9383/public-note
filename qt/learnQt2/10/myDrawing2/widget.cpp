#include "widget.h"
#include "ui_widget.h"

/*
 * QImage, QPaintDevice的子类，可以在上面使用QPainter进行绘图, 也可以加载图片, 专门用于处理图像，获取图像信息
 * QPixmap,QPaintDevice的子类，可以再上门使用QPainter进行绘图, 也可以加载图片，可以用fill初始化该对象填充色
 * 双缓冲绘图，其实就是开辟两个QPixmap缓冲区，一个缓冲区用tmp1于保存已经绘制好的图像，另一个缓冲区tmp2用于更新当前的绘制情况
 * 每次update前，都使用tmp2=tmp1，使正在绘图的缓冲区恢复成原始状态。在paintEvent中，根据当前情况，进行tmp2的绘制，并显示。
 * 当绘制结束，tmp1=tmp2.
*/

Widget::Widget(QWidget *parent) :
    QWidget(parent),
    ui(new Ui::Widget)
{
    ui->setupUi(this);
}

Widget::~Widget()
{
    delete ui;
}
