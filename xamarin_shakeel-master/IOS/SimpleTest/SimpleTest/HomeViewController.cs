using Foundation;
using System;
using UIKit;

namespace SimpleTest
{
    public partial class HomeViewController : UITableViewController
    {
        private HomeDataSource DataSource = new HomeDataSource(true);

        public HomeViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            HomeCustomData[] items = new HomeCustomData[] {
                                                            new HomeCustomData("拍照", "照相"),
                                                            new HomeCustomData("汉语", "汉人说的"),
                                                            new HomeCustomData("英语", "英国人说的"),
                                                            new HomeCustomData("日语", "日本人说的"),
                                                            new HomeCustomData("Lisp", "多范式编程语言"),
                                                            new HomeCustomData("JAVA", "跨平台的oop语言"),
                                                            new HomeCustomData("C#", "微软开发的oop语言")};

            DataSource.Add("呃", items[0]);
            DataSource.Add("Human", items[1]);
            DataSource.Add("Human", items[2]);
            DataSource.Add("Human", items[3]);
            DataSource.Add("Computer", items[4]);
            DataSource.Add("Computer", items[5]);
            DataSource.Add("Computer", items[6]);

            /*
                        DataSource.Add("人类语言", "汉语");
                        DataSource.Add("人类语言", "英语");
                        DataSource.Add("人类语言", "日语");
                        DataSource.Add("机器语言", "Lisp");
                        DataSource.Add("机器语言", "JAVA");
                        DataSource.Add("机器语言", "C#");
             */

            this.TableView.DataSource = DataSource;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            if (DataSource.Get(indexPath) is HomeCustomData)
            {
                return 64;
            }
            return base.GetHeightForRow(tableView, indexPath);
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            if (DataSource.Get(indexPath) is HomeCustomData)
            {
                if (((HomeCustomData)DataSource.Get(indexPath)).Title.Equals("拍照"))
                {
                    Console.WriteLine("打开拍照视图控制器");
                }
            }
            Console.WriteLine("clicked me...");
        }
    }
}