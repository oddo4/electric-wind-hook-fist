using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace minesweeper
{
    class GameGrid
    {
        public int SizeX { get; set; }
        public int SizeY { get; set; }
        public int MineCount { get; set; }
        public List<List<Square>> SquareList = new List<List<Square>>();

        public GameGrid(int sizeX, int sizeY, int mineCount)
        {
            SizeX = sizeX;
            SizeY = sizeY;
            MineCount = mineCount;
            SquareList = new List<List<Square>>();
            Random rand = new Random();
            CreateSquareList(rand);
        }

        private void CreateSquareList(Random rand)
        {
            int ctr = 0;
            for (int i = 0; i < SizeX; i++)
            {
                List<Square> squareRow = new List<Square>();

                for (int j = 0; j < SizeY; j++)
                {
                    Square square = new Square();
                    if (rand.Next(0, (int)Math.Round((double)MineCount/2)) == 0 && ctr < MineCount)
                    {
                        square.Bomb = true;
                        ctr++;
                    }

                    squareRow.Add(square);
                }
                SquareList.Add(squareRow);
            }
        }

        public void CreateGrid(Grid grid)
        {
            for (int j = 0; j < SizeY; j++)
            {
                ColumnDefinition column = new ColumnDefinition();
                GridLengthConverter gridLengthConverter = new GridLengthConverter();
                column.Width = (GridLength)gridLengthConverter.ConvertFrom("30");


                grid.ColumnDefinitions.Add(column);
            }

            for (int i = 0; i < SizeX; i++)
            {
                RowDefinition row = new RowDefinition();

                grid.RowDefinitions.Add(row);
                GridLengthConverter gridLengthConverter = new GridLengthConverter();
                row.Height = (GridLength)gridLengthConverter.ConvertFrom("30");
            }
        }

        public void SetElementsInGrid(Grid grid, List<List<Button>> blocksList)
        {
            for (int i = 0; i < SizeX; i++)
            {
                List<Button> row = new List<Button>();
                for (int j = 0; j < SizeY; j++)
                {
                    Brush color = Brushes.AliceBlue;
                    if (SquareList[i][j].Bomb)
                    {
                        color = Brushes.Red;
                        AddValueAround(i, j);
                    }

                    Button block = new Button() { Background = color};

                    Grid.SetRow(block, i);
                    Grid.SetColumn(block, j);

                    grid.Children.Add(block);
                    row.Add(block);
                }
                blocksList.Add(row);
            }
        }

        private void AddValueAround(int PosX, int PosY)
        {
            for (int i = PosX - 1; i < PosX + 2; i++)
            {
                for (int j = PosY - 1; j < PosY + 2; j++)
                {
                    if ((i >= 0 && i <= SizeX-1) && (j >= 0 && j <= SizeY-1))
                    {
                        SquareList[i][j].AddValue();
                    }
                }
            }
        }
        public void ShowValue(Button clicked, List<List<Button>> blocksList)
        {
            int PosX = Grid.GetRow(clicked);
            int PosY = Grid.GetColumn(clicked);
            Square current = SquareList[PosX][PosY];

            blocksList[PosX][PosY].Content = current.NearValue.ToString().Replace("0", "");

            if (current.IsBomb())
            {
                //game over
            }
            else if (current.NearValue == 0)
            {
                CheckNearValue(PosX, PosY);
            }

            blocksList[PosX][PosY].IsEnabled = false;
        }

        private void CheckNearValue(int PosX, int PosY, List<Square>CheckedList = null)
        {
            if (CheckedList == null)
            {
                CheckedList = new List<Square>();
                CheckedList.Add(SquareList[PosX][PosY]);
            }
            
            
            

            
            /*for (int i = PosX - 1; i < PosX + 2; i++)
            {
                for (int j = PosY - 1; j < PosY + 2; j++)
                {
                    if ((i >= 0 && i <= SizeX - 1) && (j >= 0 && j <= SizeY - 1))
                    {
                        blocksList[i][j].Content = SquareList[i][j].NearValue.ToString().Replace("0", "");
                        blocksList[i][j].IsEnabled = false;
                    }
                }
            }*/


        }

        public void ShowMark(Button block, bool option)
        {
            if (option)
            {
                block.Content = "🚩";
            }
            else
            {
                block.Content = "";
            }
        }
    }
}
