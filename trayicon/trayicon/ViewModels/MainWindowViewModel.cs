using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace trayicon.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        Window Window;
        TaskbarIcon taskIcon;
        private RelayCommand openWindow;

        public RelayCommand OpenWindow
        {
            get
            {
                return openWindow;
            }
            set
            {
                openWindow = value;
                RaisePropertyChanged("OpenWindow");
            }
        }
        private WindowState windowState = WindowState.Normal;

        public WindowState WindowState
        {
            get
            {
                return windowState;
            }
            set
            {
                windowState = value;
                CheckState();
                RaisePropertyChanged("WindowState");
            }
        }

        public MainWindowViewModel(Window window)
        {
            OpenWindow = new RelayCommand(ShowWindow, true);

            Window = window;
            taskIcon = new TaskbarIcon() { LeftClickCommand = OpenWindow };

            BitmapImage source = new BitmapImage();
            source.BeginInit();
            source.UriSource = new Uri("pack://application:,,,/Resources/waldo.ico");
            source.EndInit();

            taskIcon.IconSource = source;
        }

        public void CheckState()
        {
            if (WindowState == WindowState.Minimized)
            {
                Window.Hide();
                taskIcon.Visibility = Visibility.Visible;
                
            }
        }
        public void ShowWindow()
        {
            WindowState = WindowState.Normal;
            Window.Show();
            taskIcon.Visibility = Visibility.Hidden;
        }
    }
}
