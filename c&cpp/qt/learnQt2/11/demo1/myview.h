#ifndef MYVIEW_H
#define MYVIEW_H

#include <QGraphicsView>
#include <QGraphicsScene>
#include <QGraphicsProxyWidget>

#include <QDebug>
#include <QPushButton>
#include <QPropertyAnimation>
#include <QParallelAnimationGroup>

#include "myitem.h"
#include <math.h>
#include <QVector>
#include <QtAlgorithms>

#define pi 3.141592653
#define blockN 101

class myView : public QGraphicsView
{
    Q_OBJECT
    /* SLOT */
private slots:

    void animateFinish(void){
        qDebug()<<"finished!"<<endl;
    }

    void showRot(void){
        QVector<unsigned> *seq = createSingleRand();

        for(unsigned i=0; i<blockN; i++){
            unsigned itemI = i;
            animation[itemI]->setPropertyName("rotation");
            animation[itemI]->setDuration(2000);
            animation[itemI]->setStartValue(0);    //从当前位置开始
            animation[itemI]->setEndValue(360);    //旋转到360
        }
        delete seq;
        grp->start();
    }

    void showSin(void){
        QVector<unsigned> *seq = createSingleRand();

        for(unsigned i=0; i<blockN; i++){
            unsigned itemI = (*seq)[i];
            item[itemI]->setZValue(i);

            animation[itemI]->setPropertyName("pos");
            animation[itemI]->setDuration(2000);
            animation[itemI]->setStartValue(item[itemI]->pos());
            animation[itemI]->setEndValue(QPoint(50+250/blockN*i, 150+100*sin(3*pi*i/blockN)));
        }
        delete seq;
        grp->start();
    }

    void showCos(void){
        QVector<unsigned> *seq = createSingleRand();

        for(unsigned i=0; i<blockN; i++){
            unsigned itemI = (*seq)[i];
            item[itemI]->setZValue(i);

            animation[itemI]->setPropertyName("pos");
            animation[itemI]->setDuration(2000);
            animation[itemI]->setStartValue(item[itemI]->pos());
            animation[itemI]->setEndValue(QPoint(50+250/blockN*i, 150+100*cos(3*pi*i/blockN)));
        }
        delete seq;
        grp->start();
    }

    void showPoi(void){
        QVector<unsigned> *seq = createSingleRand();

        for(unsigned i=0; i<blockN; i++){
            unsigned itemI = (*seq)[i];
            item[itemI]->setZValue(i);

            animation[itemI]->setPropertyName("pos");
            animation[itemI]->setDuration(2000);
            animation[itemI]->setStartValue(item[itemI]->pos());
            animation[itemI]->setEndValue(QPoint(150, 150));
        }
        delete seq;
        grp->start();
    }

    void showCir(void){
        QVector<unsigned> *seq = createSingleRand();


        for(unsigned i=0; i<blockN; i++){
            unsigned itemI = (*seq)[i];
            item[itemI]->setZValue(i);

            animation[itemI]->setPropertyName("pos");
            animation[itemI]->setDuration(2000);
            animation[itemI]->setStartValue(item[itemI]->pos());

            double theta = 2*pi/blockN*i;
            animation[itemI]->setEndValue(QPoint(150+cos(theta)*100, 150+sin(theta)*100));
        }
        delete seq;
        grp->start();
    }

    /* construct and destory */
public:
    myView(QWidget *parent=0):QGraphicsView(parent),
        scene(new QGraphicsScene),
        btnRot(new QPushButton("rot")),btnSin(new QPushButton("sin")),btnPoi(new QPushButton("point")),
        btnCir(new QPushButton("circle")),btnCos(new QPushButton("cos")),
        grp(new QParallelAnimationGroup){

        //view初始化设置
        scene->setSceneRect(0, 0, 400, 350);
        QGraphicsProxyWidget *graBtnRot = scene->addWidget(btnRot);
        QGraphicsProxyWidget *graBtnSin = scene->addWidget(btnSin);
        QGraphicsProxyWidget *graBtnPoi = scene->addWidget(btnPoi);
        QGraphicsProxyWidget *graBtnCir = scene->addWidget(btnCir);
        QGraphicsProxyWidget *graBtnCos = scene->addWidget(btnCos);

        for(int i=0; i<blockN; i++)
        {
            item[i] = new myItem(QColor(qrand()%256, qrand()%256, qrand()%256));
            scene->addItem(item[i]);

            double theta = 2*pi/blockN*i;
            item[i]->setPos(150+cos(theta)*100, 150+sin(theta)*100);
        }

        //item初始化设置
        graBtnRot->moveBy(300, 150);
        graBtnSin->moveBy(300, 50);
        graBtnPoi->moveBy(300, 200);
        graBtnCir->moveBy(300, 100);
        graBtnCos->moveBy(300, 250);

        connect(btnRot, SIGNAL(clicked()), this, SLOT(showRot()));
        connect(btnSin, SIGNAL(clicked()), this, SLOT(showSin()));
        connect(btnPoi, SIGNAL(clicked()), this, SLOT(showPoi()));
        connect(btnCir, SIGNAL(clicked()), this, SLOT(showCir()));
        connect(btnCos, SIGNAL(clicked()), this, SLOT(showCos()));
        //view初始化设置
        this->setScene(scene);
        this->setBackgroundBrush(QColor(0, 0, 0, 100));

        //animate初始化设置
        for(int i=0; i<blockN; i++)
        {
            animation[i] = new QPropertyAnimation;
            animation[i]->setTargetObject(item[i]);
            grp->addAnimation(animation[i]);
        }
        connect(grp, SIGNAL(finished()), this, SLOT(animateFinish()));
    }
    /* member fun */
private:
    QVector<unsigned> *createSingleRand(void){
        QVector<unsigned> uvec1;
        QVector<unsigned> uvec2;
        QVector<unsigned> *sort = new QVector<unsigned>(blockN);

        for(unsigned i=0; i<blockN; i++)
            uvec1.push_back(qrand());
        uvec2 = uvec1;

        qSort(uvec1.begin(), uvec1.end());

        for(unsigned i=0; i<blockN; i++){
            for(unsigned j=0; j<blockN; j++){
                if(uvec1[i]==uvec2[j]){
                    (*sort)[i]=j;
                    break;
                }
            }
        }

        return sort;
    }
    /* member var */
private:
    QGraphicsScene *scene;
    QPushButton *btnRot, *btnSin, *btnPoi, *btnCir, *btnCos;
    myItem *(item[blockN]);
    QPropertyAnimation *animation[blockN];
    QParallelAnimationGroup *grp;

    /* event filter*/
    bool eventFilter(QObject *obj, QEvent *event){
       return false;
    }
};

#endif // MYVIEW_H
