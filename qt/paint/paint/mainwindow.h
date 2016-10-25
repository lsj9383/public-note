#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
#include <QPainter>
#include <QPushButton>
#include <QTimer>
#include <QLabel>

#include <mainwindow2.h>

namespace Ui {
class MainWindow;
}

class MainWindow : public QMainWindow
{
    Q_OBJECT

public:
    explicit MainWindow(QWidget *parent = 0);
    ~MainWindow();

private:
    Ui::MainWindow *ui;
    QPushButton *button;
    MainWindow2 w2;

protected:
    void paintEvent(QPaintEvent* event);

private slots:
    void buttonClick(void);
};

#endif // MAINWINDOW_H
