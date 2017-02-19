using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MVVMApps
{
    class ColorViewModel : INotifyPropertyChanged
    {
        Color color;
        public event PropertyChangedEventHandler PropertyChanged;

        public double Red
        {
            get { return Round(color.R); }
            set { if (Round(color.R) != value) { Color = Color.FromRgba(value, color.G, color.B, color.A); } }
        }

        public double Green
        {
            get { return Round(color.G); }
            set { if (Round(color.G) != value) { Color = Color.FromRgba(color.R, value, color.B, color.A); } }
        }

        public double Blue
        {
            get { return Round(color.B); }
            set { if (Round(color.B) != value) { Color = Color.FromRgba(color.R, color.G, value, color.A); } }
        }

        public double Alpha
        {
            get { return Round(color.A); }
            set { if (Round(color.A) != value) { Color = Color.FromRgba(color.R, color.G, color.B, value); } }
        }

        public Color Color
        {
            get { return color; }
            set
            {
                Color oldColor = color;
                if (color != value)
                {
                    color = value;
                    OnPropertyChanged("Color");
                }

                if (color.R != oldColor.R)
                    OnPropertyChanged("Red");
                if (color.G != oldColor.G)
                    OnPropertyChanged("Green");
                if (color.B != oldColor.B)
                    OnPropertyChanged("Blue");
            }
        }

        double Round(double value)
        {
            return Device.OnPlatform(value, Math.Round(value, 3), value);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
