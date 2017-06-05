#ifndef SPINBXDELEGATE_H
#define SPINBXDELEGATE_H

#include <QItemDelegate>
#include <QSpinBox>

class SpinBxDelegate : public QItemDelegate
{
public:
    SpinBxDelegate(QWidget *parent=0):QItemDelegate(parent){}
    //view利用该委托函数进行编辑器的创建, view会自动销毁编辑器的指针
    QWidget *createEditor(QWidget *parent, const QStyleOptionViewItem &option, const QModelIndex &index) const{
        QSpinBox *editor = new QSpinBox(parent);

        editor->setMinimum(0);
        editor->setMaximum(100);

        return editor;
    }
    //为编辑器设置数据
    void setEditorData(QWidget *editor, const QModelIndex &index) const{
        int value = index.model()->data(index, Qt::EditRole).toInt();       //获取当前的数据
        QSpinBox *spinBox = static_cast<QSpinBox*>(editor);     //获取编辑器指针，且强制转换为已知的类型.
        spinBox->setValue(value);                               //为编辑器设置数据
    }
    //将数据写入到模型, 标准的QItemDelegate在完成编辑后会发射closeEditor信号来告知视图,视图确保编辑器部件被关闭和销毁
    void setModelData(QWidget *editor, QAbstractItemModel *model, const QModelIndex &index) const{
        QSpinBox *spinBox = static_cast<QSpinBox*>(editor);     //获取编辑器指针，且强制转换为已知的类型.

        spinBox->interpretText();
        int value = spinBox->value();
        model->setData(index, value, Qt::EditRole);
    }
    void updateEditorGeometry(QWidget *editor, const QStyleOptionViewItem &option, const QModelIndex &index){
        editor->setGeometry(option.rect);
    }
};

#endif // SPINBXDELEGATE_H
