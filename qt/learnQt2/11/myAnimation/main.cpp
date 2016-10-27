
#include <QApplication>
#include <QPushButton>
#include <QPropertyAnimation>
#include <QParallelAnimationGroup>
#include <QSequentialAnimationGroup>

#include <QDebug>

int animation(int argc, char *argv[])
{
    QApplication a(argc, argv);

    QPushButton button("Animated Button!");
    button.show();
    QPropertyAnimation animation(&button, "geometry");
    animation.setDuration(10000);
//    animation.setStartValue(QRect(0,0,100,30));
//    animation.setEndValue(QRect(250,250,200,60));
    animation.setKeyValueAt(0, QRect(0,0,100,30));
    animation.setKeyValueAt(0.8,QRect(250,250,200,60));
    animation.setKeyValueAt(1, QRect(0,0,100,30));
    animation.start();      //非阻塞
    return a.exec();
}

int animationGroup(int argc, char *argv[])
{
    QApplication a(argc, argv);

    QPushButton b1("b1");b1.show();
    QPushButton b2("b2");b2.show();

    QPropertyAnimation *a1 = new QPropertyAnimation(&b1, "geometry");
    a1->setDuration(2000);
    a1->setStartValue(QRect(250,0,100,30));
    a1->setEndValue(QRect(250,300,100,30));

    QPropertyAnimation *a2 = new QPropertyAnimation(&b2, "geometry");
    a2->setDuration(2000);
    a2->setStartValue(QRect(400,300,100,30));
    a2->setEndValue(QRect(400,300,200,60));

    QParallelAnimationGroup apGroup;
    apGroup.addAnimation(a1);
    apGroup.addAnimation(a2);
/*
    QSequentialAnimationGroup asGroup;
    asGroup.addAnimation(a1);
    asGroup.addAnimation(a2);
*/
//    asGroup.start();
    apGroup.start();

    return a.exec();
}

int main(int argc, char *argv[])
{
//    return animation(argc, argv);
    return animationGroup(argc, argv);
}
