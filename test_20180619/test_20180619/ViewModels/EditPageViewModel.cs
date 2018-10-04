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
    class EditPageViewModel : ViewModelBase
    {
        private ObservableCollection<Person> personsList;
        private Person person;
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
        private RelayCommand savePersonCommand;

        public RelayCommand SavePersonCommand
        {
            get { return savePersonCommand; }
            set
            {
                savePersonCommand = value;
                RaisePropertyChanged("SavePersonCommand");
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

        public EditPageViewModel(Person person, ObservableCollection<Person> col)
        {
            personsList = col;
            this.person = person;
            SavePersonCommand = new RelayCommand(SavePerson, true);
            GoBackCommand = new RelayCommand(GoBack, true);

            Firstname = person.Firstname;
            Surname = person.Surname;
            PhoneNumber = person.ContactInfo.PhoneNumber;
            Email = person.ContactInfo.Email;
        }

        private void SavePerson()
        {
            ObservableCollection<Person> col = new ObservableCollection<Person>();
            Contact newContact = new Contact() { PhoneNumber = PhoneNumber, Email = Email };
            Person newPerson = new Person() { Firstname = Firstname, Surname = Surname, ContactInfo = newContact };

            foreach (Person p in personsList)
            {
                if (p == person)
                {
                    col.Add(newPerson);
                }
                else
                {
                    col.Add(p);
                }
            }

            App.JsonHelper.WriteFile(col);

            GoBack();
        }

        private void GoBack()
        {
            NavigationServiceSingleton.GetNavigationService().NavigateBack();
        }
    }
}
