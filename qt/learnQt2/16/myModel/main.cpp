
#include <QApplication>
#include <QListView>
#include <QTableView>
#include "stringlistmodel.h"
int main(int argc, char *argv[])
{
    QApplication app(argc, argv);

    //data source
    QStringList list;
    list<<"a"<<"b"<<"c";

    //model
    StringListModel model(list);

    //list view
    QListView viewList;
    viewList.setModel(&model);
    viewList.show();

    //table view
    QListView viewTable;
    viewTable.setModel(&model);
    viewTable.show();

    model.insertRows(3,2);
    model.removeRows(0,1);

    return app.exec();
}
