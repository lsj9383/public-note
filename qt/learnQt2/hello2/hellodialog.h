#ifndef HELLODIALOG_H
#define HELLODIALOG_H

#include <QDialog>

namespace Ui {
class hellodialog;
}

class hellodialog : public QDialog
{
    Q_OBJECT

public:
    explicit hellodialog(QWidget *parent = 0);
    ~hellodialog();

private:
    Ui::hellodialog *ui;
};

#endif // HELLODIALOG_H
