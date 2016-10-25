#ifndef MYWIDGET_H
#define MYWIDGET_H

#include <QWidget>
#include <QDialog>

namespace Ui {
class myWidget;
}

class myWidget : public QWidget
{
    Q_OBJECT

public:
    explicit myWidget(QWidget *parent = 0);
    ~myWidget();

public slots:
    void showChildDialog();
    void on_showChildButton_clicked();

private:
    Ui::myWidget *ui;
};

#endif // MYWIDGET_H
