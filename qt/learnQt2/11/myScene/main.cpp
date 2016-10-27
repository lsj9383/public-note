#include <QApplication>
#include <QGraphicsScene>
#include <QGraphicsRectItem>
#include <QGraphicsView>
#include <QDebug>
#include "myitem.h"
#include "myview.h"
int main(int argc,char* argv[ ])
{
    QApplication app(argc,argv);

    //新建场景
    QGraphicsScene scene;
    scene.setSceneRect(0,0,400,300);
    MyItem *item = new MyItem;
    //将图形项添加到场景中
    scene.addItem(item);
    item->setPos(10,10);
//    item->setZValue(1); //z轴坐标
    QGraphicsRectItem *rectItem = scene.addRect(QRect(0,0,100,100), QPen(Qt::blue),
                                                QBrush(Qt::green));
    rectItem->setPos(20,20);

    item->setParentItem(rectItem);
    rectItem->moveBy(20,20);
 //   rectItem->rotation();   //父项旋转,子项相对父项的位置是不变的(父项移动，子项跟着移动)

    //为场景创造视图
    MyView view;
    view.setScene(&scene);
//    view.scale(2, 2);
    //设置场景的前景色
    view.setForegroundBrush(QColor(255,255,255,100));
    //设置场景的背景图片
    view.setBackgroundBrush(QPixmap("D:\\DESIGN\\cpp\\learnQt2\\11\\myScene\\background.png"));
    view.resize(400, 300);
    view.show();

    return app.exec();
}

