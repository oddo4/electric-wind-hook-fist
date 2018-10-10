using minesweeper.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace minesweeper
{
    /// <summary>
    /// Interakční logika pro App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Window MainWindow = null;
        public static JsonHelper JsonHelper = new JsonHelper("MinesweeperData");
    }
}
