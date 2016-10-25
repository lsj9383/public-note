#include <QApplication>
#include <QGraphicsScene>
#include <QGraphicsView>
#include "myitem.h"
#include <QPropertyAnimation>

int main(int argc, char *argv[])
{
    QApplication app(argc, argv);
    //scene
    QGraphicsScene scene;
    scene.setSceneRect(-200,-150,400,300);
    //item
    MyItem *item = new MyItem;
    scene.addItem(item);
    //view
    QGraphicsView view;
    view.setScene(&scene);
    view.show();
    //animation
    QPropertyAnimation *animation=new QPropertyAnimation(item, "rotation");
    animation->setDuration(10000);
    animation->setStartValue(0);
    animation->setEndValue(3600);
    animation->start(QAbstractAnimation::DeleteWhenStopped);
    return app.exec();
}
