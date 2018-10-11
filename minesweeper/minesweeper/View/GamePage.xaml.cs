using minesweeper.Model;
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
using System.Windows.Threading;

namespace minesweeper.View
{
    /// <summary>
    /// Interakční logika pro GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        GameGrid gameGrid;
        List<List<Button>> blocksList = new List<List<Button>>();
        DispatcherTimer Timer = null;
        int Difficulty;
        int Tick = 0;
        int Marked = 0;
        public GamePage(int X = 9, int Y = 9, int MineCount = 4, int Difficulty = 0)
        {
            InitializeComponent();
            gameGrid = new GameGrid(X, Y, MineCount);
            this.Difficulty = Difficulty;
            if (App.JsonHelper.ReadFile() != null)
            {
                gameGrid.HighScoreCol = App.JsonHelper.ReadFile()[Difficulty];
            }
            else
            {
                ObservableCollection<ObservableCollection<Score>> col = new ObservableCollection<ObservableCollection<Score>>()
                {
                    new ObservableCollection<Score>(),
                    new ObservableCollection<Score>(),
                    new ObservableCollection<Score>()
                };

                App.JsonHelper.WriteFile(col);
            }
            HighScoreListView.ItemsSource = gameGrid.HighScoreCol.OrderByDescending(x => x.Time).ToList();
            Mines.Content = MineCount.ToString().PadLeft(3, '0');

            gameGrid.CreateGrid(MainGrid);
            SetMainWindowSize(X, Y);
            gameGrid.SetElementsInGrid(MainGrid, blocksList);
            SetClickEvents();
            Debug.WriteLine(MainGrid.Children.Count);
        }
        private void SetMainWindowSize(int X, int Y)
        {
            App.MainWindow.Width = 80 * X;
            App.MainWindow.Height = 50 * Y;
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
            if (Timer == null)
            {
                Timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 1) };
                Timer.Tick += new EventHandler(TimeTick);
                Timer.Start();
            }
                
            if(!gameGrid.GameOver)
            {
                gameGrid.ShowValue((Button)sender, blocksList);
                gameGrid.CheckRemainingBlocks(blocksList);

                if(gameGrid.Won)
                {
                    Mines.Content = "000";
                    CreateHighScore();
                }
            }


            //if(gameGrid.)

        }

        private void CreateHighScore()
        {
            Score score = new Score() { Time = Tick, Difficulty = Difficulty };

            gameGrid.HighScoreCol.Add(score);

            var oldData = App.JsonHelper.ReadFile();
            oldData[Difficulty] = gameGrid.HighScoreCol;

            App.JsonHelper.WriteFile(oldData);
        }

        private void SetDifficulty_Click(object sender, RoutedEventArgs e)
        {
            Button button = ((Button)sender);
            switch (button.Tag)
            {
                case "0":
                    NavigationServiceSingleton.GetNavigationService().NavigateToPage(new GamePage());
                    break;
                case "1":
                    NavigationServiceSingleton.GetNavigationService().NavigateToPage(new GamePage(16, 16, 40, int.Parse(button.Tag.ToString())));
                    break;
                case "2":
                    NavigationServiceSingleton.GetNavigationService().NavigateToPage(new GamePage(16, 30, 99, int.Parse(button.Tag.ToString())));
                    break;
            }
        }

        private void TimeTick(object sender, EventArgs e)
        {
            if (gameGrid.GameOver)
            {
                Timer.Stop();
                EndButton.IsEnabled = false;
            }
            else
            {
                Tick++;
                Time.Content = Tick.ToString().PadLeft(3, '0');
            }
        }

        private void MarkBlock_Click(object sender, MouseButtonEventArgs e)
        {
            if (!gameGrid.GameOver)
            {
                int X = Grid.GetRow((Button)sender);
                int Y = Grid.GetColumn((Button)sender);
                Square block = gameGrid.SquareList[X][Y];
                Marked = block.SetMark(Marked);
                gameGrid.ShowMark(((Button)sender), block.Mark);
                if (gameGrid.MineCount - Marked < 0)
                {
                    Mines.Content = "-" + ((gameGrid.MineCount - Marked) * -1).ToString().PadLeft(2, '0');
                }
                else
                {
                    Mines.Content = (gameGrid.MineCount - Marked).ToString().PadLeft(3, '0');
                }

                if(gameGrid.MineCount - Marked == 0)
                {
                    EndButton.IsEnabled = true;
                }
                else
                {
                    EndButton.IsEnabled = false;
                }
            }
        }
        private void OpenRemaining_Click(object sender, RoutedEventArgs e)
        {
            gameGrid.OpenRemainingBlocks(blocksList);
        }
    }
}
