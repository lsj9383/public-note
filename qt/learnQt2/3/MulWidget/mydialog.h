#ifndef MYDIALOG_H
#define MYDIALOG_H

#include <QDialog>

namespace Ui {
class myDialog;
}

class myDialog : public QDialog
{
    Q_OBJECT

public:
    explicit myDialog(QWidget *parent = 0);
    ~myDialog();
    void close(void){
        delete this;
    }

private:
    Ui::myDialog *ui;
};

#endif // MYDIALOG_H
