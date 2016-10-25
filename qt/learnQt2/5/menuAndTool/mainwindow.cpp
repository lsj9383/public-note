#include "mainwindow.h"
#include "ui_mainwindow.h"
#include <QToolButton>
#include <QSpinBox>
#include <QTextEdit>
#include <QMdiSubWindow>
#include <QLabel>

/*
菜单栏  包含所有的动作，每个动作都是一个QAction类
工具栏  包含着常用的动作(QAction)
状态栏  显示当前的状态信息
中心部件 是应用程序的主要功能实现区域
Dock部件 停靠窗口，类似工具栏的作用
*/

MainWindow::MainWindow(QWidget *parent) :
    QMainWindow(parent),
    ui(new Ui::MainWindow)
{
    ui->setupUi(this);

    //添加编辑菜单
    QMenu *editMenu = ui->menuBar->addMenu("编辑(&E)");                                       //在菜单栏中添加编辑

    //添加打开动作
    QAction *action_Open = editMenu->addAction(QIcon(":/myImage/images/open.png"), "打开文件");      //在编辑中添加打开操作
    ui->mainToolBar->addAction(action_Open);                                                //在工具栏中添加动作

    //添加动作组和间隔
    QActionGroup *group = new QActionGroup(this);
    QAction *action_L = group->addAction("左对齐(&L)");action_L->setCheckable(true);
    QAction *action_R = group->addAction("右对齐(&R)");action_R->setCheckable(true);
    QAction *action_C = group->addAction("居中(&C)");action_C->setCheckable(true);
    editMenu->addSeparator();         //分隔符，主要是将动作分隔
    editMenu->addAction(action_L);
    editMenu->addAction(action_R);
    editMenu->addAction(action_C);

    //创建QToolButton
    QToolButton *toolBtn = new QToolButton(this);toolBtn->setText("颜色");
    QMenu *colorMenu = new QMenu(this);     //创建一个菜单
    colorMenu->addAction("红色");
    colorMenu->addAction("绿色");
    toolBtn->setMenu(colorMenu);
    toolBtn->setPopupMode((QToolButton::MenuButtonPopup));  //设置弹窗模式
    ui->mainToolBar->addWidget(toolBtn);    //工具栏中添加toolBtn
    QSpinBox *spinBox = new QSpinBox(this);
    ui->mainToolBar->addWidget(spinBox);    //工具栏中添加spinBox

    //状态栏的使用
        //1.永久信息，显示在右下角，不会被临时信息覆盖.
        //2.正常信息，显示在左下角，一直存在，直到被临时信息覆盖, 临时信息结束后，恢复.
        //3.临时信息，显示在左下角，有生存周期.
    QLabel *permanent = new QLabel(this);
    permanent->setFrameStyle(QFrame::Box | QFrame::Sunken);
    permanent->setText("www.lsj.com");
    ui->statusBar->addPermanentWidget(permanent);            //显示永久信息,不会被临时消息覆盖
    QLabel *lab=new QLabel(this);
    lab->setText("lsj");                                    //正常信息
    ui->statusBar->addWidget(lab);
}

MainWindow::~MainWindow()
{
    delete ui;
}

void MainWindow::on_action_New_triggered()
{
    //新建文本编辑器
    QTextEdit *edit = new QTextEdit(this);
//    edit->show();
    //将刚刚生成的文本编辑器，添加到mdiArea的子窗口中
    QMdiSubWindow *child = ui->mdiArea->addSubWindow(edit);
    child->setWindowTitle("多文档编辑器子窗口");
    child->show();
    ui->statusBar->showMessage("创建新文本!", 2000);
}

void MainWindow::on_action_dock_triggered()
{
    ui->dockWidget_4->show();
}
