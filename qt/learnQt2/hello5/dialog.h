#ifndef __DIALOG_H
#define __DIALOG_H

#include<QDialog>

//申明这个命名空间下有个Dialog类
namespace Ui {
class Dialog;
}

//
class dialog:public QDialog
{
/* construct and destory */
public:
    dialog(QWidget *parent=0);
    ~dialog();

/* member var */
private:
    Ui::Dialog *ui;     //ui中保存了相关初始化控件 以及其初始状态
};

#endif
