#ifndef MYDIALOG_H
#define MYDIALOG_H

#include <QDialog>

namespace Ui {
class MyDialog;
}

class MyDialog : public QDialog
{
    Q_OBJECT

public:
    explicit MyDialog(QWidget *parent = 0);
    ~MyDialog();

private:
    Ui::MyDialog *ui;

//自定义信号
signals:
    void dlgReturn(int);
private slots:
    void on_pushButton_clicked();
};

#endif // MYDIALOG_H
