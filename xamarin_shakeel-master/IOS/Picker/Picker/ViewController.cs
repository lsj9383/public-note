using System;
using System.Collections.Generic;
using UIKit;

namespace Picker
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
            pickerView.Model = new PickerModel(this);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        /*****************************************************Picker Model******************************************************/
        private class PickerModel : UIPickerViewModel
        {
            private ViewController parentController;
            private List<string> transportList;
            private List<string> distanceList;
            private List<string> unitList;
            string transportSelected;
            string distanceSelected;
            string unitSelected;

            public PickerModel(ViewController controller)
            {
                parentController = controller;
                transportList = new List<string>() { "Foot", "Bicycle", "Motor", "Car", "Bus"};
                distanceList = new List<string>() { "0.5", "1", "5", "10", "100"};
                unitList = new List<string>() { "m", "km"};

                transportSelected = transportList[0];
                distanceSelected = distanceList[0];
                unitSelected = unitList[0];
            }

            //下面组件的含义是类型
            public override nint GetComponentCount(UIPickerView pickerView)
            {   //设置列数，每一列就是一个组件列表
                return 3;
            }

            public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
            {   //设置每个组件列表的数量，也就是每列的行数。不同的组件列表数量或许会不同
                switch (component)
                {
                    case 0:
                        return transportList.Count;
                    case 1:
                        return distanceList.Count;
                    default:
                        return unitList.Count;
                }
            }

            public override string GetTitle(UIPickerView pickerView, nint row, nint component)
            {
                switch (component)
                {
                    case 0:
                        return transportList[(int)row];
                    case 1:
                        return distanceList[(int)row];
                    default:
                        return unitList[(int)row];
                }
            }

            public override void Selected(UIPickerView pickerView, nint row, nint component)
            {   //对自定义选择器实现响应
                switch (component)
                {
                    case 0:
                        transportSelected = this.transportList[(int)row];break;
                    case 1:
                        distanceSelected = this.distanceList[(int)row]; break;
                    default:
                        unitSelected = this.unitList[(int)row]; break;
                }

                parentController.label.Text = String.Format("Transport:{0}\nDistance:{1}{2}", transportSelected, distanceSelected, unitSelected);
            }
        }
    }
}