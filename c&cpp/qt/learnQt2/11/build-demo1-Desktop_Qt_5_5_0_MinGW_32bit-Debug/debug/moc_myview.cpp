/****************************************************************************
** Meta object code from reading C++ file 'myview.h'
**
** Created by: The Qt Meta Object Compiler version 67 (Qt 5.5.0)
**
** WARNING! All changes made in this file will be lost!
*****************************************************************************/

#include "../../demo1/myview.h"
#include <QtCore/qbytearray.h>
#include <QtCore/qmetatype.h>
#if !defined(Q_MOC_OUTPUT_REVISION)
#error "The header file 'myview.h' doesn't include <QObject>."
#elif Q_MOC_OUTPUT_REVISION != 67
#error "This file was generated using the moc from 5.5.0. It"
#error "cannot be used with the include files from this version of Qt."
#error "(The moc has changed too much.)"
#endif

QT_BEGIN_MOC_NAMESPACE
struct qt_meta_stringdata_myView_t {
    QByteArrayData data[8];
    char stringdata0[62];
};
#define QT_MOC_LITERAL(idx, ofs, len) \
    Q_STATIC_BYTE_ARRAY_DATA_HEADER_INITIALIZER_WITH_OFFSET(len, \
    qptrdiff(offsetof(qt_meta_stringdata_myView_t, stringdata0) + ofs \
        - idx * sizeof(QByteArrayData)) \
    )
static const qt_meta_stringdata_myView_t qt_meta_stringdata_myView = {
    {
QT_MOC_LITERAL(0, 0, 6), // "myView"
QT_MOC_LITERAL(1, 7, 13), // "animateFinish"
QT_MOC_LITERAL(2, 21, 0), // ""
QT_MOC_LITERAL(3, 22, 7), // "showRot"
QT_MOC_LITERAL(4, 30, 7), // "showSin"
QT_MOC_LITERAL(5, 38, 7), // "showCos"
QT_MOC_LITERAL(6, 46, 7), // "showPoi"
QT_MOC_LITERAL(7, 54, 7) // "showCir"

    },
    "myView\0animateFinish\0\0showRot\0showSin\0"
    "showCos\0showPoi\0showCir"
};
#undef QT_MOC_LITERAL

static const uint qt_meta_data_myView[] = {

 // content:
       7,       // revision
       0,       // classname
       0,    0, // classinfo
       6,   14, // methods
       0,    0, // properties
       0,    0, // enums/sets
       0,    0, // constructors
       0,       // flags
       0,       // signalCount

 // slots: name, argc, parameters, tag, flags
       1,    0,   44,    2, 0x08 /* Private */,
       3,    0,   45,    2, 0x08 /* Private */,
       4,    0,   46,    2, 0x08 /* Private */,
       5,    0,   47,    2, 0x08 /* Private */,
       6,    0,   48,    2, 0x08 /* Private */,
       7,    0,   49,    2, 0x08 /* Private */,

 // slots: parameters
    QMetaType::Void,
    QMetaType::Void,
    QMetaType::Void,
    QMetaType::Void,
    QMetaType::Void,
    QMetaType::Void,

       0        // eod
};

void myView::qt_static_metacall(QObject *_o, QMetaObject::Call _c, int _id, void **_a)
{
    if (_c == QMetaObject::InvokeMetaMethod) {
        myView *_t = static_cast<myView *>(_o);
        Q_UNUSED(_t)
        switch (_id) {
        case 0: _t->animateFinish(); break;
        case 1: _t->showRot(); break;
        case 2: _t->showSin(); break;
        case 3: _t->showCos(); break;
        case 4: _t->showPoi(); break;
        case 5: _t->showCir(); break;
        default: ;
        }
    }
    Q_UNUSED(_a);
}

const QMetaObject myView::staticMetaObject = {
    { &QGraphicsView::staticMetaObject, qt_meta_stringdata_myView.data,
      qt_meta_data_myView,  qt_static_metacall, Q_NULLPTR, Q_NULLPTR}
};


const QMetaObject *myView::metaObject() const
{
    return QObject::d_ptr->metaObject ? QObject::d_ptr->dynamicMetaObject() : &staticMetaObject;
}

void *myView::qt_metacast(const char *_clname)
{
    if (!_clname) return Q_NULLPTR;
    if (!strcmp(_clname, qt_meta_stringdata_myView.stringdata0))
        return static_cast<void*>(const_cast< myView*>(this));
    return QGraphicsView::qt_metacast(_clname);
}

int myView::qt_metacall(QMetaObject::Call _c, int _id, void **_a)
{
    _id = QGraphicsView::qt_metacall(_c, _id, _a);
    if (_id < 0)
        return _id;
    if (_c == QMetaObject::InvokeMetaMethod) {
        if (_id < 6)
            qt_static_metacall(this, _c, _id, _a);
        _id -= 6;
    } else if (_c == QMetaObject::RegisterMethodArgumentMetaType) {
        if (_id < 6)
            *reinterpret_cast<int*>(_a[0]) = -1;
        _id -= 6;
    }
    return _id;
}
QT_END_MOC_NAMESPACE
