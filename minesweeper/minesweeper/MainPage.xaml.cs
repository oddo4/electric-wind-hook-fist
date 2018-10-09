using System;
using System.Collections.Generic;
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
        public MainPage()
        {
            InitializeComponent();
            GameGrid gameGrid = new GameGrid(8, 8, 1);

            gameGrid.CreateGrid(MainGrid);
            gameGrid.SetElementsInGrid(MainGrid);
            SetClickEvents();
            Debug.WriteLine(MainGrid.Children.Count);
        }
        private void SetClickEvents()
        {
            foreach(UIElement item in MainGrid.Children)
            {
                ((Button)item).Click += ShowBlock_Click;
            }
        }

        private void ShowBlock_Click(object sender, RoutedEventArgs e)
        {
            ((Button)sender).IsEnabled = false;
        }
    }
}
