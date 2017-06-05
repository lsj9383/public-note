/********************************************************************************
** Form generated from reading UI file 'hellodialog.ui'
**
** Created by: Qt User Interface Compiler version 5.5.0
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_HELLODIALOG_H
#define UI_HELLODIALOG_H

#include <QtCore/QVariant>
#include <QtWidgets/QAction>
#include <QtWidgets/QApplication>
#include <QtWidgets/QButtonGroup>
#include <QtWidgets/QDialog>
#include <QtWidgets/QHeaderView>
#include <QtWidgets/QLabel>

QT_BEGIN_NAMESPACE

class Ui_hellodialog
{
public:
    QLabel *label;

    void setupUi(QDialog *hellodialog)
    {
        if (hellodialog->objectName().isEmpty())
            hellodialog->setObjectName(QStringLiteral("hellodialog"));
        hellodialog->resize(400, 300);
        label = new QLabel(hellodialog);
        label->setObjectName(QStringLiteral("label"));
        label->setGeometry(QRect(120, 120, 141, 16));

        retranslateUi(hellodialog);

        QMetaObject::connectSlotsByName(hellodialog);
    } // setupUi

    void retranslateUi(QDialog *hellodialog)
    {
        hellodialog->setWindowTitle(QApplication::translate("hellodialog", "Dialog", 0));
        label->setText(QApplication::translate("hellodialog", "hello_world!\345\223\210\345\223\210", 0));
    } // retranslateUi

};

namespace Ui {
    class hellodialog: public Ui_hellodialog {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_HELLODIALOG_H
