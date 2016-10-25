#ifndef STRINGLISTMODEL_H
#define STRINGLISTMODEL_H

#include <QAbstractListModel>
#include <QStringList>

class StringListModel : public QAbstractListModel
{
    Q_OBJECT
public:
    StringListModel(const QStringList &strings, QObject *parent=0):QAbstractListModel(parent),
        stringList(strings){    }
    int rowCount(const QModelIndex &parent) const{return stringList.count();}
    QVariant data(const QModelIndex &index, int role) const{
        if(!index.isValid())                return QVariant();
        if(index.row()>stringList.size())   return QVariant();
        if(role==Qt::DisplayRole||role==Qt::EditRole)           return stringList.at(index.row());
        else                                return QVariant();
    }
    QVariant headerData(int section, Qt::Orientation orientation, int role) const
    {
        if (role != Qt::DisplayRole)
            return QVariant();

        if (orientation == Qt::Horizontal)
            return QString("Column %1").arg(section);
        else
            return QString("Row %1").arg(section);
    }
    Qt::ItemFlags flags(const QModelIndex &index) const{
        if(!index.isValid())    return Qt::ItemIsEditable;
        return QAbstractItemModel::flags(index) | Qt::ItemIsEditable;
    }
    bool setData(const QModelIndex &index, const QVariant &value, int role){
        if(index.row()>stringList.size() || !index.isValid())    return false;
        if(role==Qt::EditRole) {
            stringList.replace(index.row(), value.toString());
            emit dataChanged(index, index);
            return true;
        }else{
            return false;
        }
    }

    bool insertRows(int position, int rows, const QModelIndex &index = QModelIndex()){
        beginInsertRows(QModelIndex(), position, position+rows-1);
        for(int row=0; row<rows; row++){
            stringList.insert(position, "");
        }
        endInsertRows();
        return true;
    }
    bool removeRows(int position, int rows, const QModelIndex &index = QModelIndex()){
        beginRemoveRows(QModelIndex(), position, position+rows-1);
        for(int row=0; row<rows; row++){
            stringList.removeAt(position);
        }
        endRemoveRows();
    }

private:
    QStringList stringList;
};

#endif // STRINGLISTMODEL_H
