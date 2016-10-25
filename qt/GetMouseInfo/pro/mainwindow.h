#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
#include <QMouseEvent>      //鼠标类头文件
#include <QLabel>           //标签类控件
#include <QLineEdit>

namespace Ui {
class MainWindow;
}

class MainWindow : public QMainWindow
{
    Q_OBJECT

public:
    explicit MainWindow(QWidget *parent = 0);
    ~MainWindow();

private:
    Ui::MainWindow *ui;
    QLabel *pos1;
    QLabel *pos2;
    QLabel *pos3;
    QLineEdit *posEdit1;
    QLineEdit *posEdit2;
    QLineEdit *posEdit3;


//引入鼠标事件
protected:
    void mousePressEvent(QMouseEvent *e);       //按下时触发
    void mouseMoveEvent(QMouseEvent *e);        //按下并移动时触发
    void mouseReleaseEvent(QMouseEvent *e);     //释放时触发
};

#endif // MAINWINDOW_H
