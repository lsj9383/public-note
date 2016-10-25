#include "widget.h"
#include "ui_widget.h"
#include <QLineEdit>

Widget::Widget(QWidget *parent) :
    QWidget(parent),
    ui(new Ui::Widget)
{
    ui->setupUi(this);
    QLineEdit *line = new QLineEdit(this);

    QCursor cursor;
    cursor.setShape(Qt::OpenHandCursor);
    setCursor(cursor);
}

Widget::~Widget()
{
    delete ui;
}
