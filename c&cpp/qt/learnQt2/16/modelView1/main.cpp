
#include <QApplication>
#include <QFileSystemModel>
#include <QStandardItem>
#include <QStandardItemModel>
#include <QTreeView>
#include <QListView>

#include <QDebug>

int demo1(int argc, char *argv[])
{
    QApplication app(argc, argv);

    //model
    QFileSystemModel model;
    model.setRootPath(QDir::currentPath());

    //Tree View
    QTreeView tree;
    tree.setModel(&model);
    tree.setRootIndex(model.index(QDir::currentPath()));

    //List View
    QListView list;
    list.setModel(&model);
    list.setRootIndex(model.index(QDir::currentPath()));

    //show it
    tree.show();
    list.show();

    return app.exec();
}

int demo2(int argc, char *argv[])
{
    QApplication app(argc, argv);

    QStandardItemModel model;
    QStandardItem *parentItem = model.invisibleRootItem();

    //item0
    QStandardItem *item0 = new QStandardItem;
    item0->setText("A");
    QPixmap pixmap0(50, 50);
    pixmap0.fill("red");
    item0->setIcon(QIcon(pixmap0));
    item0->setToolTip("indexA");

    parentItem->appendRow(item0);
    parentItem=item0;

    //item1
    QStandardItem *item1 = new QStandardItem;
    item1->setText("B");
    QPixmap pixmap1(50, 50);
    pixmap1.fill("blue");
    item1->setIcon(QIcon(pixmap1));
    item1->setToolTip("indexB");
    parentItem->appendRow(item1);

    //item2
    QStandardItem *item2 = new QStandardItem;
    QPixmap pixmap2(50, 50);
    pixmap2.fill("green");
    item2->setData("C", Qt::EditRole);
    item2->setData("indexC", Qt::ToolTipRole);
    item2->setData(QIcon(pixmap2), Qt::DecorationRole);
    parentItem->appendRow(item2);

    //tree View
    QTreeView view;
    view.setModel(&model);
    view.show();

    //get data
    QModelIndex indexA = model.index(0, 0, QModelIndex());
    qDebug()<<"indexA row count: "<<model.rowCount(indexA)<<endl;
    QModelIndex indexB = model.index(0, 0, indexA);
    qDebug()<<"indexB text: "<<model.data(indexB, Qt::EditRole).toString()<<endl;
    qDebug()<<"indexB toolTip: "<<model.data(indexB, Qt::ToolTipRole).toString();

    return app.exec();
}

int main(int argc, char *argv[])
{
    return demo2(argc, argv);
}
