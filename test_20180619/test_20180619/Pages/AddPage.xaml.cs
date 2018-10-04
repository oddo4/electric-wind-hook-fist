using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using test_20180619.Classes;
using test_20180619.ViewModels;

namespace test_20180619.Pages
{
    /// <summary>
    /// Interakční logika pro AddPage.xaml
    /// </summary>
    public partial class AddPage : Page
    {
        public AddPage(ObservableCollection<Person> col)
        {
            InitializeComponent();
            this.DataContext = new AddPageViewModel(col);
        }
    }
}
