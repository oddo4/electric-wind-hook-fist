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
        public List<Square> SquareList = new List<Square>();
        public bool GameOver = false;
        public bool Won = false;
        public bool Start = false;
        public ObservableCollection<Score> HighScoreCol = new ObservableCollection<Score>();

        public GameGrid(int sizeX, int sizeY, int mineCount)
        {
            SizeX = sizeX;
            SizeY = sizeY;
            MineCount = mineCount;
            SquareList = new List<Square>();
            CreateSquareList();
        }

        private void CreateSquareList()
        {
            for (int i = 0; i < SizeX; i++)
            {
                for (int j = 0; j < SizeY; j++)
                {
                    Square square = new Square() { PosX = i, PosY = j };

                    SquareList.Add(square);
                }
            }
        }

        private void SetBombs(Random rand, Square square)
        {
            int ctr = 0;

            while (ctr < MineCount)
            {
                var i = rand.Next(0, (SizeX * SizeY) - 1);
                var current = SquareList[i];

                if (!current.Bomb)
                {
                    if (current.PosX != square.PosX || current.PosY != square.PosY)
                    {
                        current.Bomb = true;
                        AddValueAround(current.PosX, current.PosY);

                        ctr++;
                    }
                }

                if (ctr >= MineCount)
                {
                    break;
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
                    Button block = new Button();

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
                        SquareList.First(x => x.PosX == i && x.PosY == j).AddValue();
                    }
                }
            }
        }
        public void ShowValue(Button clicked, List<List<Button>> blocksList)
        {
            int PosX = Grid.GetRow(clicked);
            int PosY = Grid.GetColumn(clicked);
            Square current = SquareList.First(s => s.PosX == PosX && s.PosY == PosY);
            current.Covered = false;

            if (!Start)
            {
                Start = true;
                Random rand = new Random();
                SetBombs(rand, current);
            }

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
                        var square = SquareList.First(s => s.PosX == i && s.PosY == j);
                        if (!square.Mark && square.Covered)
                        {
                            blocksToCheck.Add(square);
                        }
                    }
                }
            }

            return blocksToCheck;
        }

        private void UpdateBlocksList(List<List<Button>> blocksList)
        {
            foreach (Square square in SquareList)
            {
                if (!square.Covered)
                {
                    blocksList[square.PosX][square.PosY].Content = square.NearValue.ToString().Replace("0", "");
                    blocksList[square.PosX][square.PosY].IsEnabled = square.Covered;
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
            foreach (Square square in SquareList)
            {
                if (square.Covered)
                    list.Add(square);

                if (square.Bomb)
                    bomb++;
            }

            if ((list.Count == MineCount && bomb == list.Count) || endGame) // blocksList[square.PosX][square.PosY]
            {    
                foreach (Square square in SquareList)
                {
                    if (list.Count(s => s.Bomb == true) != MineCount && (square.Bomb && !square.Mark))
                    {
                        won = false;
                    }
                }
                EndGame(true, blocksList, won);
            }
        }

        public void OpenRemainingBlocks(List<List<Button>> blocksList)
        {
            bool endGame = true;
            foreach (Square square in SquareList)
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
                    else if (square.Mark && !square.Bomb)
                    {
                        ShowMark(blocksList[square.PosX][square.PosY], true);
                    }
                    else
                    {
                        ShowValue(blocksList[square.PosX][square.PosY], blocksList);
                        square.Covered = false;
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
                foreach (Square square in SquareList)
                {
                    if (square.Mark && square.Bomb && !won)
                    {
                        ShowBomb(blocksList[square.PosX][square.PosY], "❌");
                    }
                    else if (!square.Mark && square.Bomb)
                    {
                        if (won)
                        {
                            ShowMark(blocksList[square.PosX][square.PosY], true);
                        }
                        else
                        {
                            ShowBomb(blocksList[square.PosX][square.PosY]);
                        }
                        GameOver = true;
                    }
                }
            }
        }
    }
}
