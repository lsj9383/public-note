#-------------------------------------------------
#
# Project created by QtCreator 2016-02-29T19:59:38
#
#-------------------------------------------------

QT       += core gui

greaterThan(QT_MAJOR_VERSION, 4): QT += widgets

TARGET = mySignalSlot
TEMPLATE = app


SOURCES += main.cpp\
        widget.cpp \
    mydialog.cpp

HEADERS  += widget.h \
    mydialog.h

FORMS    += widget.ui \
    mydialog.ui
