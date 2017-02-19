using System;
using System.Drawing;
using Mono.Data.Sqlite;
using System.IO;
using UIKit;

namespace SQLite
{
    public partial class ViewController : UIViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            string sqlitePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "MyDatabase.db3");  //database位置
            
            label.Text = sqlitePath;
            btnCreate.TouchUpInside += (sender, e) =>
            {
                CreateSqliteDataBase(sqlitePath);   //在指定路径创建数据库
            };
            btnInsert.TouchUpInside += (sender, e) =>
            {
                InsertSqliteDataBase(sqlitePath);   //在指定数据库中插入数据
            };
            btnQuery.TouchUpInside += (sender, e) =>
            {
                QuerySqliteDataBase(sqlitePath);
            };
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        private void CreateSqliteDataBase(string databaseFile)
        {
            Console.WriteLine(databaseFile);
            if (!File.Exists(databaseFile))
            {   //数据库不存在, 则创建数据库
                SqliteConnection.CreateFile(databaseFile);          //创建数据库文件
                SqliteConnection sqlCon = new SqliteConnection(string.Format("Data source = {0}", databaseFile));   //创建与数据库的连接
                sqlCon.Open();                  //打开连接
                SqliteCommand sqlCom = new SqliteCommand(sqlCon);   //将sql命令行sqlCom和数据库连接
                sqlCom.CommandText = "CREATE TABLE Customers (ID INTEGER PRIMARY KEY, FirstName VARCHAR(20), LastName VARCHAR(20))";    //创建Customers表
                sqlCom.ExecuteNonQuery();       //执行sqlCom
                sqlCon.Close();
                this.lblStatus.Text = "Database Created";
            }
            else
            {
                this.lblStatus.Text = "Database already exists!";
            }
        }

        private void InsertSqliteDataBase(string databaseFile)
        {
            if (File.Exists(databaseFile))
            {
                SqliteConnection sqlCon = new SqliteConnection(string.Format("Data source = {0}", databaseFile));
                sqlCon.Open();
                SqliteCommand sqlCom = new SqliteCommand(sqlCon);   //将sql命令行sqlCom和数据库连接
                sqlCom.CommandText = "INSERT INTO Customers(FirstName, LastName) VALUES ('Dimitris', 'Tavlikos')";
                sqlCom.ExecuteNonQuery();       //执行sqlCom
                sqlCon.Close();
                this.lblStatus.Text = "Insert One";
            }
            else
            {
                this.lblStatus.Text = "Database not exists!";
            }
        }

        private void QuerySqliteDataBase(string databaseFile)
        {
            if (File.Exists(databaseFile))
            {
                SqliteConnection sqlCon = new SqliteConnection(string.Format("Data source = {0}", databaseFile));
                sqlCon.Open();
                SqliteCommand sqlCom = new SqliteCommand(sqlCon);   //将sql命令行sqlCom和数据库连接
                sqlCom.CommandText = "SELECT * FROM Customers WHERE FirstName='Dimitris'";
                SqliteDataReader dbReader = sqlCom.ExecuteReader();
                while (dbReader.Read())
                {
                    lblStatus.Text = String.Format("First Name : {0}\nLast Name = {1}", dbReader["FirstName"], dbReader["LastName"]);
                }
                
                sqlCon.Close();
            }
            else
            {
                this.lblStatus.Text = "Database not exists!";
            }
        }
    }
}