#include <QApplication>
#include <QGraphicsScene>
#include <QGraphicsView>
#include <QGraphicsWidget>
#include <QTextEdit>
#include <QPushButton>
#include <QGraphicsProxyWidget>
#include <QGraphicsLinearLayout>
#include <QObject>

int main(int argc, char *argv[])
{
    QApplication a(argc, argv);

    QGraphicsScene scene;
    //QWidget
    QTextEdit *edit = new QTextEdit;
    QPushButton *button = new QPushButton("clear");
    QObject::connect(button, SIGNAL(clicked()), edit, SLOT(clear()));
    QGraphicsProxyWidget *graphEdit = scene.addWidget(edit);
    QGraphicsProxyWidget *graphButton = scene.addWidget(button);

    //将部件添加到部件管理器

    QGraphicsLinearLayout *layout = new QGraphicsLinearLayout;
    layout->addItem(graphEdit);
    layout->addItem(graphButton);

    //
    QGraphicsProxyWidget *form = new QGraphicsProxyWidget;
    form->setWindowFlags(Qt::Window);
    form->setWindowTitle("Widget Item");
    form->setLayout(layout);
    scene.addItem(form);

    //视图显示场景
    QGraphicsView view(&scene);
    view.show();

    return a.exec();
}
