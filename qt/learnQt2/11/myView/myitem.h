#ifndef MYITEM_H
#define MYITEM_H

#include <QGraphicsItem>
#include <QPainter>
#include <QKeyEvent>
#include <QGraphicsSceneMouseEvent>
#include <QGraphicsSceneHoverEvent>
#include <QGraphicsSceneContextMenuEvent>
#include <QMenu>
#include <QCursor>

#include <QGraphicsBlurEffect>

class MyItem : public QGraphicsItem
{
public:
    //初始化
    MyItem(QGraphicsItem *parent=0):QGraphicsItem(parent),brushColor(Qt::red){
        setFlag(QGraphicsItem::ItemIsFocusable);//图形项可以设为焦点
        setFlag(QGraphicsItem::ItemIsMovable);  //图形项可以移动?
        setAcceptHoverEvents(true);             //图形象可以接受鼠标悬停事件
    }
    ~MyItem(){}
    //advance推动动画
    void advance(int phase){
        if(!phase)  return ;

        int value = qrand() % 100;
        if(value<25){
            setTransform(QTransform().rotate(45), true);
            moveBy(qrand()%10, qrand()%10);
        }
        else if(value<50){
            setTransform(QTransform().rotate(-45), true);
            moveBy(-qrand()%10, -qrand()%10);
        }
        else if(value<75)
        {
            setTransform(QTransform().rotate(30), true);
            moveBy(-qrand()%10, qrand()%10);
        }
        else
        {
            setTransform(QTransform().rotate(-30), true);
            moveBy(qrand()%10, -qrand()%10);
        }
    }
    //返回图形项的形状!
    QPainterPath shape() const{
        QPainterPath path;
        path.addRect(-10, -10, 20, 20);
        return path;
    }

    //图形项边界确定
    QRectF boundingRect() const{
        qreal adjust = 0.5;
        return QRectF(-10-adjust, -10-adjust, 20+adjust, 20+adjust);
    }
    //图像项的paintEvent!!!
    void paint(QPainter *painter, const QStyleOptionGraphicsItem *option, QWidget *widget){
        if (hasFocus() || !collidingItems().isEmpty()){
            painter->setPen(QPen(QColor(255,255,255,200)));
        }else{
            painter->setPen(QPen(QColor(100,100,100,100)));
        }
        painter->setBrush(brushColor);
        painter->drawRect(-10, -10, 20, 20);
    }
private:
    QColor brushColor;

protected:
    void mousePressEvent(QGraphicsSceneMouseEvent *event){  //点击，设为焦点
        setFocus();
        setCursor(Qt::ClosedHandCursor);
    }
    void hoverEnterEvent(QGraphicsSceneHoverEvent *event){
        setCursor(Qt::OpenHandCursor);
        setToolTip("I am item");
    }
    void contextMenuEvent(QGraphicsSceneContextMenuEvent *event){
        QMenu menu;

        QAction *moveAction = menu.addAction("move back");
        QAction *selectAction = menu.exec(event->screenPos());
        if(selectAction == moveAction){
            setPos(0,0);
        }
    }
    void keyPressEvent(QKeyEvent *event){
        switch(event->key())
        {
        case Qt::Key_1:{
            QGraphicsBlurEffect *blurEffect = new QGraphicsBlurEffect;
            blurEffect->setBlurHints(QGraphicsBlurEffect::QualityHint);
            blurEffect->setBlurRadius(8);
            setGraphicsEffect(blurEffect);
            break;
        }
        case Qt::Key_Down:  moveBy(0, 10);break;
        case Qt::Key_Right: moveBy(10, 0);break;
        default:
            break;
        }
    }
};

#endif // MYITEM_H
