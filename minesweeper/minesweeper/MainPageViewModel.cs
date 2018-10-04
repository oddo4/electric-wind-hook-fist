using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace minesweeper
{
    class MainPageViewModel : ViewModelBase
    {

        public MainPageViewModel(Grid MainGrid)
        {
            GameGrid gameGrid = new GameGrid(8, 8, 10);

            gameGrid.CreateGrid(MainGrid);
            gameGrid.SetElementsInGrid(MainGrid);
        }
    }
}
