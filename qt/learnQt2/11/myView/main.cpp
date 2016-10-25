
#include <QGraphicsView>
#include <QGraphicsScene>
#include <QGraphicsItem>
#include <QApplication>
#include "myitem.h"
#include "myview.h"
#include <QTimer>

int main(int argc, char *argv[])
{
    QApplication app(argc, argv);
    QGraphicsScene scene;
    scene.setSceneRect(-200, -150, 400, 300);
    for (int i=0; i<5; i++)
    {
        MyItem *item = new MyItem;
        item->setPos(i*50-90, -50);
        scene.addItem(item);
    }

    MyView view;
    view.setScene(&scene);
    view.setBackgroundBrush(QPixmap("../myView/background.png"));
    view.show();

    QTimer timer;
    QObject::connect(&timer, SIGNAL(timeout()), &scene, SLOT(advance()));
    timer.start(300);

    return app.exec();
}
