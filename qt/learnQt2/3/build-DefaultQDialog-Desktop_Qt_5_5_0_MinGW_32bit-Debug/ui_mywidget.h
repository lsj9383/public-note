/********************************************************************************
** Form generated from reading UI file 'mywidget.ui'
**
** Created by: Qt User Interface Compiler version 5.5.0
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_MYWIDGET_H
#define UI_MYWIDGET_H

#include <QtCore/QVariant>
#include <QtWidgets/QAction>
#include <QtWidgets/QApplication>
#include <QtWidgets/QButtonGroup>
#include <QtWidgets/QHeaderView>
#include <QtWidgets/QPushButton>
#include <QtWidgets/QWidget>

QT_BEGIN_NAMESPACE

class Ui_myWidget
{
public:
    QPushButton *pushButton;
    QPushButton *pushButton_2;
    QPushButton *pushButton_3;
    QPushButton *pushButton_4;
    QPushButton *pushButton_5;
    QPushButton *pushButton_6;
    QPushButton *pushButton_7;
    QPushButton *pushButton_8;

    void setupUi(QWidget *myWidget)
    {
        if (myWidget->objectName().isEmpty())
            myWidget->setObjectName(QStringLiteral("myWidget"));
        myWidget->resize(400, 300);
        pushButton = new QPushButton(myWidget);
        pushButton->setObjectName(QStringLiteral("pushButton"));
        pushButton->setGeometry(QRect(70, 60, 75, 23));
        pushButton_2 = new QPushButton(myWidget);
        pushButton_2->setObjectName(QStringLiteral("pushButton_2"));
        pushButton_2->setGeometry(QRect(190, 60, 75, 23));
        pushButton_3 = new QPushButton(myWidget);
        pushButton_3->setObjectName(QStringLiteral("pushButton_3"));
        pushButton_3->setGeometry(QRect(70, 110, 75, 23));
        pushButton_4 = new QPushButton(myWidget);
        pushButton_4->setObjectName(QStringLiteral("pushButton_4"));
        pushButton_4->setGeometry(QRect(190, 110, 75, 23));
        pushButton_5 = new QPushButton(myWidget);
        pushButton_5->setObjectName(QStringLiteral("pushButton_5"));
        pushButton_5->setGeometry(QRect(70, 160, 75, 23));
        pushButton_6 = new QPushButton(myWidget);
        pushButton_6->setObjectName(QStringLiteral("pushButton_6"));
        pushButton_6->setGeometry(QRect(190, 160, 75, 23));
        pushButton_7 = new QPushButton(myWidget);
        pushButton_7->setObjectName(QStringLiteral("pushButton_7"));
        pushButton_7->setGeometry(QRect(70, 210, 75, 23));
        pushButton_8 = new QPushButton(myWidget);
        pushButton_8->setObjectName(QStringLiteral("pushButton_8"));
        pushButton_8->setGeometry(QRect(190, 210, 75, 23));

        retranslateUi(myWidget);

        QMetaObject::connectSlotsByName(myWidget);
    } // setupUi

    void retranslateUi(QWidget *myWidget)
    {
        myWidget->setWindowTitle(QApplication::translate("myWidget", "myWidget", 0));
        pushButton->setText(QApplication::translate("myWidget", "\351\242\234\350\211\262\345\257\271\350\257\235\346\241\206", 0));
        pushButton_2->setText(QApplication::translate("myWidget", "\346\226\207\344\273\266\345\257\271\350\257\235\346\241\206", 0));
        pushButton_3->setText(QApplication::translate("myWidget", "\345\255\227\344\275\223\345\257\271\350\257\235\346\241\206", 0));
        pushButton_4->setText(QApplication::translate("myWidget", "\350\276\223\345\205\245\345\257\271\350\257\235\346\241\206", 0));
        pushButton_5->setText(QApplication::translate("myWidget", "\346\266\210\346\201\257\345\257\271\350\257\235\346\241\206", 0));
        pushButton_6->setText(QApplication::translate("myWidget", "\350\277\233\345\272\246\345\257\271\350\257\235\346\241\206", 0));
        pushButton_7->setText(QApplication::translate("myWidget", "\351\224\231\350\257\257\344\277\241\346\201\257", 0));
        pushButton_8->setText(QApplication::translate("myWidget", "\345\220\221\345\257\274", 0));
    } // retranslateUi

};

namespace Ui {
    class myWidget: public Ui_myWidget {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_MYWIDGET_H
