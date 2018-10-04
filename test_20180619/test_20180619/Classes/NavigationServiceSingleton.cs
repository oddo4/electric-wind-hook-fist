using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace test_20180619.Classes
{
    class NavigationServiceSingleton
    {
        private static NavigationServiceSingleton navigation;
        private Frame frame;
        private Page _currentPage;
        private Page _lastPage;

        private NavigationServiceSingleton()
        {

        }
        public static NavigationServiceSingleton GetNavigationService()
        {
            if (navigation == null)
            {
                navigation = new NavigationServiceSingleton();
            }
            return navigation;
        }

        public void NavigateToPage(Page nextPage)
        {
            _lastPage = _currentPage;
            _currentPage = nextPage;
            frame.NavigationService.Navigate(_currentPage);
        }
        public void NavigateBack()
        {
            frame.NavigationService.GoBack();
        }
        public void SetCurrentPage(Page currentPage)
        {
            _currentPage = currentPage;
        }

        public static NavigationServiceSingleton CreateNavigationService(Frame frame)
        {
            NavigationServiceSingleton x = GetNavigationService();
            x.frame = frame;
            return x;
        }
    }
}
