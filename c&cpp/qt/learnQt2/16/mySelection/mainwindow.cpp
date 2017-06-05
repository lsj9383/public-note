#include "mainwindow.h"
#include "ui_mainwindow.h"
#include "spinbxdelegate.h"

#include <QStandardItemModel>
#include <QStandardItem>
#include <QTableView>
#include <QDebug>

MainWindow::MainWindow(QWidget *parent) :
    QMainWindow(parent),
    ui(new Ui::MainWindow)
{
    ui->setupUi(this);

    //model
    QStandardItemModel *model = new QStandardItemModel(7,4,this);
    for(int row=0; row<7; row++){
        for(int column=0; column<4; column++){
            QStandardItem *item = new QStandardItem(QString("%1").arg(row*4+column));
            model->setItem(row, column, item);
        }
    }

    //view
    tableView = new QTableView(this);
    tableView->setModel(model);
    tableView->show();
    setCentralWidget(tableView);

    //获取视图的项目选择模型
    QItemSelectionModel *selectionModel = tableView->selectionModel();

    //定义左上角和右下角的索引，然后使用这两个索引创建选址
    QModelIndex topLeft;
    QModelIndex bottomRight;

    topLeft = model->index(1,1,QModelIndex());
    bottomRight = model->index(5, 2, QModelIndex());
    QItemSelection selection(topLeft, bottomRight);

    //使用指定的选择模式来选择项目
    selectionModel->select(selection, QItemSelectionModel::Select);
    ui->mainToolBar->addAction("当前选择", this, SLOT(getCurrentItemData()));
    ui->mainToolBar->addAction("切换选择", this, SLOT(toggleSelection()));

    //使用委托
    SpinBxDelegate *delegate = new SpinBxDelegate(this);
    tableView->setItemDelegate(delegate);
}

MainWindow::~MainWindow()
{
    delete ui;
}

void MainWindow::getCurrentItemData()
{
    qDebug()<<tableView->selectionModel()->currentIndex().data().toString()<<endl;
}

void MainWindow::toggleSelection()
{
    QModelIndex topLeft = tableView->model()->index(0, 0, QModelIndex());
    QModelIndex bottomRight = tableView->model()->index(tableView->model()->rowCount(QModelIndex())-1,
                                                        tableView->model()->columnCount(QModelIndex())-1,
                                                        QModelIndex());
    QItemSelection curSelection(topLeft, bottomRight);
    tableView->selectionModel()->select(curSelection, QItemSelectionModel::Toggle);
}
