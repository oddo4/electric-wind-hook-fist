using GalaSoft.MvvmLight;
using Mice.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mice.ViewModels
{
    class MainPageViewModel : ViewModelBase
    {
        private JsonHelper jsonHelper = new JsonHelper("Mice_Data");
        private Win32API win32API = new Win32API();
        private MouseSettings currentSetting = new MouseSettings();
        private ObservableCollection<MouseSettings> settingsList;

        public ObservableCollection<MouseSettings> SettingsList
        {
            get { return settingsList; }
            set { settingsList = value; RaisePropertyChanged("SettingsList"); }
        }

        private int mouseSpeedValue = 10;

        public int MouseSpeedValue
        {
            get { return mouseSpeedValue; }
            set
            {
                mouseSpeedValue = value;
                win32API.SetMouseSpeed(mouseSpeedValue);
                RaisePropertyChanged("MouseSpeedValue");
            }
        }

        private int doubleClickSpeed = 500;

        public int DoubleClickSpeed
        {
            get { return doubleClickSpeed; }
            set
            {
                doubleClickSpeed = value;
                win32API.SetDoubleClickSpeed(doubleClickSpeed);
                RaisePropertyChanged("DoubleClickSpeed");
            }
        }

        private int scrollWheelSpeed = 3;

        public int ScrollWheelSpeed
        {
            get { return scrollWheelSpeed; }
            set
            {
                scrollWheelSpeed = value;
                win32API.SetScrollWheelSpeed(scrollWheelSpeed);
                RaisePropertyChanged("ScrollWheelSpeed");
            }
        }

        public MainPageViewModel()
        {
            settingsList = jsonHelper.ReadFile();
        }
    }
}
