using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace minesweeper
{
    class GameGrid
    {
        public int SizeX { get; set; }
        public int SizeY { get; set; }
        public int MineCount { get; set; }
        public List<List<Square>> SquareList { get; set; }

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

            //int ctr = 0;

            for (int i = 0; i < SizeX; i++)
            {
                RowDefinition row = new RowDefinition();

                grid.RowDefinitions.Add(row);
                GridLengthConverter gridLengthConverter = new GridLengthConverter();
                row.Height = (GridLength)gridLengthConverter.ConvertFrom("30");

                //kontrola vytáření gridu
                /*for (int k = 0; k < SizeY; k++)
                {
                    Brush color;
                    if (k % 2 == ctr)
                    {
                        color = Brushes.Red;
                    }
                    else
                    {
                         color = Brushes.Green;
                    }
                    
                    Rectangle rect = new Rectangle() { Fill = color };

                    Grid.SetRow(rect, i);
                    Grid.SetColumn(rect, k);

                    grid.Children.Add(rect);
                }
                if (ctr == 0)
                {
                    ctr = 1;
                }
                else
                {
                    ctr = 0;
                }*/
            }
        }

        public void SetElementsInGrid(Grid grid)
        {
            for (int i = 0; i < SizeX; i++)
            {
                for (int j = 0; j < SizeY; j++)
                {
                    Brush color;
                    if (SquareList[i][j].Bomb)
                    {
                        color = Brushes.Red;
                    }
                    else
                    {
                        color = Brushes.Green;
                    }

                    Rectangle rect = new Rectangle() { Fill = color };

                    Grid.SetRow(rect, i);
                    Grid.SetColumn(rect, j);

                    grid.Children.Add(rect);
                }
            }
        }
    }
}
