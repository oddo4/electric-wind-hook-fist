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
using System.Windows.Media;
using System.Windows.Shapes;

namespace minesweeper.Model
{
    class GameGrid
    {
        public int SizeX { get; set; }
        public int SizeY { get; set; }
        public int MineCount { get; set; }
        public List<List<Square>> SquareList = new List<List<Square>>();
        public bool GameOver = false;
        public bool Won = false;
        public ObservableCollection<Score> HighScoreCol = new ObservableCollection<Score>();

        public GameGrid(int sizeX, int sizeY, int mineCount)
        {
            SizeX = sizeX;
            SizeY = sizeY;
            MineCount = mineCount;
            SquareList = new List<List<Square>>();
            Random rand = new Random();
            CreateSquareList();
            SetBombs(rand);
        }

        private void CreateSquareList()
        {
            for (int i = 0; i < SizeX; i++)
            {
                List<Square> squareRow = new List<Square>();

                for (int j = 0; j < SizeY; j++)
                {
                    Square square = new Square() { PosX = i, PosY = j };
                    squareRow.Add(square);
                }
                SquareList.Add(squareRow);
            }
        }

        private void SetBombs(Random rand)
        {
            int ctr = 0;
            while (ctr < MineCount)
            {
                for (int i = 0; i < SizeX; i++)
                {
                    List<Square> squareRow = new List<Square>();

                    for (int j = 0; j < SizeY; j++)
                    {
                        if (rand.Next(0, (int)Math.Round((double)SizeX * SizeY)) == 0)
                        {
                            SquareList[i][j].Bomb = true;
                            ctr++;
                        }

                        if (ctr >= MineCount)
                        {
                            return;
                        }
                    }
                }
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
                    //Brush color = Brushes.AliceBlue;
                    if (SquareList[i][j].Bomb)
                    {
                        //color = Brushes.Red;
                        AddValueAround(i, j);
                    }

                    Button block = new Button();// { Background = color };

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
            current.Covered = false;

            if (!current.Mark && current.Bomb)
            {
                blocksList[current.PosX][current.PosY].Background = Brushes.Red;
                EndGame(true, blocksList);
            }
            else if (!current.Mark && current.NearValue == 0)
            {
                CheckSurroundingSquares(PosX, PosY);
                UpdateBlocksList(blocksList);
            }
            else if (!current.Mark)
            {
                blocksList[PosX][PosY].Content = current.NearValue.ToString().Replace("0", "");
                blocksList[PosX][PosY].IsEnabled = false;
                CheckRemainingBlocks(blocksList);
            }
            
        }

        private void CheckSurroundingSquares(int PosX, int PosY)
        {
            List<Square> blocksToCheck = GetSquaresToCheck(PosX, PosY);
            
            foreach(Square square in blocksToCheck)
            {
                if (!square.Bomb)
                {
                    square.Covered = false;
                }

                if (square.NearValue == 0)
                {
                    CheckSurroundingSquares(square.PosX, square.PosY);
                }
            }
        }

        private List<Square> GetSquaresToCheck(int PosX, int PosY)
        {
            List<Square> blocksToCheck = new List<Square>();

            for (int i = PosX - 1; i < PosX + 2; i++)
            {
                for (int j = PosY - 1; j < PosY + 2; j++)
                {
                    if ((i >= 0 && i <= SizeX - 1) && (j >= 0 && j <= SizeY - 1))
                    {
                        if (!SquareList[i][j].Mark && SquareList[i][j].Covered)
                        {
                            blocksToCheck.Add(SquareList[i][j]);
                        }
                    }
                }
            }

            return blocksToCheck;
        }

        private void UpdateBlocksList(List<List<Button>> blocksList)
        {
            foreach(List<Square> row in SquareList)
            {
                foreach (Square square in row)
                {
                    if (!square.Covered)
                    {
                        blocksList[square.PosX][square.PosY].Content = square.NearValue.ToString().Replace("0", "");
                        blocksList[square.PosX][square.PosY].IsEnabled = square.Covered;
                    }
                }
            }
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
        
        private void ShowBomb(Button block, string icon = "💣") //❌
        {
            block.Content = icon;
        }
        public void CheckRemainingBlocks(List<List<Button>> blocksList, bool endGame = false)
        {
            var list = new List<Square>();
            int bomb = 0;
            bool won = true;
            foreach (List<Square> row in SquareList)
            {
                list.AddRange(row.Where(x => x.Covered == true));
                bomb += row.Count(x => x.Bomb == true);
            }

            if ((list.Count == MineCount && bomb == list.Count) || endGame)
            {    
                foreach(List<Square> row in SquareList)
                {
                    foreach (Square square in row)
                    {

                    }
                }

                foreach (Square square in list)
                {
                    if (square.Bomb)
                    {
                        ShowBomb(blocksList[square.PosX][square.PosY]);
                    }
                    else if (square.Bomb && square.Mark)
                    {
                        ShowMark(blocksList[square.PosX][square.PosY], true);
                    }
                }
                EndGame(true, blocksList, won);
            }
        }

        public void OpenRemainingBlocks(List<List<Button>> blocksList)
        {
            bool endGame = true;
            foreach (List<Square> row in SquareList)
            {
                foreach (Square square in row)
                {
                    if (square.Covered)
                    {
                        if (!square.Mark && square.Bomb)
                        {
                            blocksList[square.PosX][square.PosY].Background = Brushes.Red;
                            ShowBomb(blocksList[square.PosX][square.PosY]);
                            endGame = false;
                        }
                        else if (square.Mark && square.Bomb)
                        {
                            ShowBomb(blocksList[square.PosX][square.PosY], "❌");
                        }
                        else
                        {
                            ShowValue(blocksList[square.PosX][square.PosY], blocksList);
                        }
                    }
                }
            }

            CheckRemainingBlocks(blocksList, endGame);
        }

        private void EndGame(bool option, List<List<Button>> blocksList, bool won = false)
        {
            if (option)
            {
                if (won)
                {
                    Won = true;
                    GameOver = true;
                }
                else
                {
                    foreach (List<Square> row in SquareList)
                    {
                        foreach (Square square in row)
                        {
                            if (!square.Mark && square.Bomb)
                            {
                                ShowBomb(blocksList[square.PosX][square.PosY]);
                                GameOver = true;
                            }
                            else if (square.Mark && square.Bomb)
                            {
                                ShowBomb(blocksList[square.PosX][square.PosY], "❌");
                            }
                        }
                    }
                }
            }
        }
    }
}
