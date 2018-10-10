using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace minesweeper
{
    /// <summary>
    /// Interakční logika pro MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        GameGrid gameGrid;
        List<List<Button>> blocksList = new List<List<Button>>();
        public MainPage()
        {
            InitializeComponent();
            gameGrid = new GameGrid(8, 8, 10);

            gameGrid.CreateGrid(MainGrid);
            gameGrid.SetElementsInGrid(MainGrid, blocksList);
            SetClickEvents();
            Debug.WriteLine(MainGrid.Children.Count);
        }
        private void SetClickEvents()
        {
            foreach(UIElement item in MainGrid.Children)
            {
                ((Button)item).Click += ShowBlock_Click;
                ((Button)item).MouseRightButtonDown += MarkBlock_Click;
            }
        }

        private void ShowBlock_Click(object sender, RoutedEventArgs e)
        {
            gameGrid.ShowValue((Button)sender, blocksList);

        }

        private void MarkBlock_Click(object sender, MouseButtonEventArgs e)
        {
            int X = Grid.GetRow((Button)sender);
            int Y = Grid.GetColumn((Button)sender);
            Square block = gameGrid.SquareList[X][Y];
            block.SetMark();
            gameGrid.ShowMark(((Button)sender), block.Mark);
        }
    }
}
