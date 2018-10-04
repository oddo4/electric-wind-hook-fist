using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using test_20180619.Classes;
using test_20180619.Pages;

namespace test_20180619.ViewModels
{
    class MainPageViewModel : ViewModelBase
    {
        private ObservableCollection<Person> personsList = new ObservableCollection<Person>();

        public ObservableCollection<Person> PersonsList
        {
            get { return personsList; }
            set
            {
                personsList = value;
                RaisePropertyChanged("PersonsList");
            }
        }
        private Person selectedPerson;

        public Person SelectedPerson
        {
            get { return selectedPerson; }
            set
            {
                selectedPerson = value;
                RaisePropertyChanged("SelectedPerson");
            }
        }
        private RelayCommand addPersonCommand;

        public RelayCommand AddPersonCommand
        {
            get { return addPersonCommand; }
            set
            {
                addPersonCommand = value;
                RaisePropertyChanged("AddPersonCommand");
            }
        }
        private RelayCommand editPersonCommand;

        public RelayCommand EditPersonCommand
        {
            get { return editPersonCommand; }
            set
            {
                editPersonCommand = value;
                RaisePropertyChanged("EditPersonCommand");
            }
        }
        private RelayCommand deletePersonCommand;

        public RelayCommand DeletePersonCommand
        {
            get { return deletePersonCommand; }
            set
            {
                deletePersonCommand = value;
                RaisePropertyChanged("DeletePersonCommand");
            }
        }

        public MainPageViewModel()
        {
            PersonsList = App.JsonHelper.ReadFile();
            AddPersonCommand = new RelayCommand(AddPerson, true);
            EditPersonCommand = new RelayCommand(EditPerson, true);
            DeletePersonCommand = new RelayCommand(DeletePerson, true);
        }

        private void AddPerson()
        {
            NavigationServiceSingleton.GetNavigationService().NavigateToPage(new AddPage(PersonsList));
        }

        private void EditPerson()
        {
            Debug.WriteLine(SelectedPerson.Firstname);
            NavigationServiceSingleton.GetNavigationService().NavigateToPage(new EditPage(SelectedPerson, PersonsList));
        }

        private void DeletePerson()
        {
            PersonsList.Remove(SelectedPerson);
            App.JsonHelper.WriteFile(PersonsList);
        }

    }
}
