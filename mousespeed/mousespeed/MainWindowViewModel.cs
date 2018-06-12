using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace mousespeed
{
    class MainWindowViewModel : ViewModelBase
    {
        [DllImport("User32.dll")]
        static extern Boolean SystemParametersInfo(
            UInt32 uiAction,
            UInt32 uiParam,
            UInt32 pvParam,
            UInt32 fWinIni);

        public const UInt32 SPI_SETMOUSESPEED = 0x0071;
        public const UInt32 SPI_GETMOUSESPEED = 0x0070;

        private int minValue = 1;

        public int MinValue
        {
            get
            {
                return minValue;
            }
            set
            {
                minValue = value;
                RaisePropertyChanged("MinValue");
            }
        }

        private int maxValue = 20;

        public int MaxValue
        {
            get
            {
                return maxValue;
            }
            set
            {
                maxValue = value;
                RaisePropertyChanged("MaxValue");
            }
        }

        private int speedValue = 10;

        public int SpeedValue
        {
            get
            {
                return speedValue;
            }
            set
            {
                speedValue = value;
                ChangeSpeed();
                RaisePropertyChanged("SpeedValue");
            }
        }

        private void ChangeSpeed()
        {
            SystemParametersInfo(SPI_SETMOUSESPEED, 0, (uint)speedValue, 0);
        }
    }
}
