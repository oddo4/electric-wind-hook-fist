using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace minesweeper
{
    class MainPageViewModel : ViewModelBase
    {
        private RelayCommand<string> showBlockCommand;

        public RelayCommand<string> ShowBlockCommand
        {
            get
            {
                return showBlockCommand;
            }
            set
            {
                showBlockCommand = value;
                RaisePropertyChanged("ShowBlockCommand");
            }
        }

        public MainPageViewModel(Grid MainGrid)
        {
            GameGrid gameGrid = new GameGrid(8, 8, 10);

            gameGrid.CreateGrid(MainGrid);
            gameGrid.SetElementsInGrid(MainGrid);
            Debug.WriteLine(MainGrid.Children.Count);

            ShowBlockCommand = new RelayCommand<string>(ShowBlock, true);
        }

        private void ShowBlock(string sender)
        {
            Debug.WriteLine("click" + sender);
        }
    }
}
