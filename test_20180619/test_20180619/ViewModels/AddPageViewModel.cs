using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test_20180619.Classes;

namespace test_20180619.ViewModels
{
    class AddPageViewModel : ViewModelBase
    {
        private ObservableCollection<Person> personsList;
        private string firstname;

        public string Firstname
        {
            get { return firstname; }
            set
            {
                firstname = value;
                RaisePropertyChanged("Firstname");
            }
        }
        private string surname;

        public string Surname
        {
            get { return surname; }
            set
            {
                surname = value;
                RaisePropertyChanged("Surname");
            }
        }
        private string phoneNumber;

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set
            {
                phoneNumber = value;
                RaisePropertyChanged("PhoneNumber");
            }
        }
        private string email;

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                RaisePropertyChanged("Email");
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
        private RelayCommand goBackCommand;

        public RelayCommand GoBackCommand
        {
            get { return goBackCommand; }
            set
            {
                goBackCommand = value;
                RaisePropertyChanged("GoBackCommand");
            }
        }

        public AddPageViewModel(ObservableCollection<Person> col)
        {
            personsList = col;
            AddPersonCommand = new RelayCommand(AddPerson, true);
            GoBackCommand = new RelayCommand(GoBack, true);
        }

        private void AddPerson()
        {
            Contact contact = new Contact() { PhoneNumber = PhoneNumber, Email = Email };
            Person person = new Person() { Firstname = Firstname, Surname = Surname, ContactInfo = contact };

            personsList.Add(person);

            App.JsonHelper.WriteFile(personsList);
            GoBack();
        }

        private void GoBack()
        {
            NavigationServiceSingleton.GetNavigationService().NavigateBack();
        }

    }
}
