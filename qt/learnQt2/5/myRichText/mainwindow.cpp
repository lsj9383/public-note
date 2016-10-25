#include "mainwindow.h"
#include "ui_mainwindow.h"

#include <QTextFrame>
#include <QDebug>

/*
富文本：
QTextEdit，就是一个富文本控件，它不仅支持文本，还支持列表、图片。
对富文本控件的控制，主要依赖于两种方法:
    1.插入
        通常定义框架格式(QxxxFrameFormat)来进行插入。
    2.检索
        也就是从当前的文本中读取出内容，可以用迭代器的方式哦，
        检索的时候，通常使用框架(QxxxFrame类型)来获取。
*/

MainWindow::MainWindow(QWidget *parent) :
    QMainWindow(parent),
    ui(new Ui::MainWindow)
{
    ui->setupUi(this);

    QTextDocument *document = ui->textEdit->document();     //获取文档对象
    QTextFrame *rootFrame = document->rootFrame();          //获取根框架
    QTextFrameFormat format;                                //创建框架格式
    format.setBorderBrush(Qt::red);                         //边界颜色
    format.setBorder(3);                                    //边界宽带
    rootFrame->setFrameFormat(format);                      //框架使用

    //子框架
    QTextFrameFormat frameFormat;
    frameFormat.setBackground(Qt::lightGray);
    frameFormat.setMargin(10);
    frameFormat.setPadding(5);
    frameFormat.setBorder(2);
    frameFormat.setBorderStyle(QTextFrameFormat::BorderStyle_Dotted);
    QTextCursor cursor = ui->textEdit->textCursor();        //获取光标
    cursor.insertFrame(frameFormat);

    //添加遍历框架的动作
    QAction *action_TextFrame = new QAction("框架", this);
    connect(action_TextFrame, SIGNAL(triggered()), this, SLOT(showTextFrame()));
    ui->mainToolBar->addAction(action_TextFrame);

    //设置字体格式
    QAction *action_font = new QAction("字体", this);
    action_font->setCheckable(true);                        //设置动作不是点击，而是被选中
    connect(action_font, SIGNAL(toggled(bool)), this, SLOT(setTextFont(bool))); //toggled就是是否被选中动作
    ui->mainToolBar->addAction(action_font);
}

MainWindow::~MainWindow()
{
    delete ui;
}

void MainWindow::setTextFont(bool checked)
{
    if (checked)
    {
        QTextCursor cursor = ui->textEdit->textCursor();
        QTextBlockFormat blockFormat;                       //文本块格式
        QTextCharFormat charFormat;                         //字符格式

        blockFormat.setAlignment(Qt::AlignCenter);          //居中
        cursor.insertBlock(blockFormat);                    //在光标处插入文本块格式

        charFormat.setBackground(Qt::lightGray);
        charFormat.setFontUnderline(true);
        cursor.setCharFormat(charFormat);
        cursor.insertText("test font");
    }
    else
    {

        /* restore */

    }
}

void MainWindow::showTextFrame(void)
{
    QTextDocument *document = ui->textEdit->document();
    QTextFrame *rootFrame = document->rootFrame();

    for (QTextFrame::iterator b=rootFrame->begin(); b!=rootFrame->end(); b++)
    {
        QTextFrame *childFrame = b.currentFrame();
        QTextBlock childBlock = b.currentBlock();

        if (childFrame)
        {
          qDebug()<<"it's a frame"<<endl;
        }
        else if(childBlock.isValid())
        {
            qDebug()<<"block:"<<childBlock.text()<<endl;
        }
    }
}
