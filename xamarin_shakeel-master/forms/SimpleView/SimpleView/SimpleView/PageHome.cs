using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SimpleView
{
    public class PageHome : ContentPage
    {
        private HomeModel m_homeModel = new HomeModel();
        private ListView m_homeListView = new ListView();
        private HomeTemplateSelector m_templateSelector = new HomeTemplateSelector();

        public PageHome()
        {
            Title = "主页";

            m_homeModel.Add("常用", new HomeDataItem
            {
                ItemImage = ImageSource.FromResource("SimpleView.Resource.red-camera.png"),
                ItemTitle = "拍照",
                ItemDesc = "点击拍照，开启美颜"
            });

            m_homeModel.Add("其他", new HomeDataItem
            {
                ItemImage = ImageSource.FromResource("SimpleView.Resource.red-label.png"),
                ItemTitle = "功能测试一",
                ItemDesc = "暂无描述信息"
            });

            m_homeModel.Add("其他", new HomeDataItem
            {
                ItemImage = ImageSource.FromResource("SimpleView.Resource.red-label.png"),
                ItemTitle = "功能测试二",
                ItemDesc = "暂无描述信息"
            });

            m_homeListView.HasUnevenRows = true;                //设置cell有不同的行高，不受heightrow的影响
            m_homeListView.IsPullToRefreshEnabled = true;       //使能下拉
            m_homeListView.Refreshing += async (sender, e)=>{ await Task.Delay(2000);  m_homeListView.EndRefresh(); };
            m_homeListView.ItemsSource = m_homeModel.Model();   //Model
            m_homeListView.ItemTemplate = m_templateSelector;   //Template

            m_homeListView.ItemSelected += (sender, e) => { ((ListView)sender).SelectedItem = null;  };
            m_homeListView.ItemTapped += (sender, e) =>
            {
                DisplayAlert("ok", "ItemTapped", "cancel");
            };

            Content = m_homeListView;
        }
    }
}
