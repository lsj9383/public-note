using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MVVMApps
{
    class PowersViewModel : ViewModelBase
    {
        double exponent, power;

        public PowersViewModel(double baseValue)
        {
            BaseValue = baseValue;
            Exponent = 0;

            IncreaseExponentCommand = new Command(()=> { Exponent += 1; });
            DecreaseExponentCommand = new Command(()=> { Exponent -= 1; });
        }

        public double BaseValue { private set; get; }
        public double Exponent
        {
            get { return exponent; }
            private set
            {
                if (SetProperty(ref exponent, value))
                {
                    Power = Math.Pow(BaseValue, exponent);
                }
            }
        }

        public double Power
        {
            get { return power; }
            private set { SetProperty(ref power, value); }
        }

        public ICommand IncreaseExponentCommand { set; get; }
        public ICommand DecreaseExponentCommand { set; get; }
    }
}
