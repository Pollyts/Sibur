using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;
using Sibur.Models;
using Sibur.Views;
using System;

namespace Sibur.ViewModels
{
    class ProfileViewModel
    {
        DBRequests db = new DBRequests();
        public ICommand ChangeProfileInfoCommand { get; set; }
        public ICommand QuitCommand { protected set; get; }
        public INavigation Navigation { get; set; }
        public Profile View { get; set; }
        public User CurrentUser { get; set; }
        public ProfileViewModel()
        {
            ChangeProfileInfoCommand = new Command(ChangeProfileInfo);
            QuitCommand = new Command(Quit);
            CurrentUser = Globals.CurrentUser;
        }
        public string Name
        {
            get 
            {
                if (CurrentUser.Name == null)
                    return "Неизвестно";
                else return CurrentUser.Name;
            }
            set
            {
              CurrentUser.Name = value;
            }
        }
        public string Mail
        {
            get
            {
                return CurrentUser.Mail.Trim();
            }
            set
            {
                CurrentUser.Mail = value;
            }
        }
        public string BirthDate
        {
            get
            {
                //return CurrentUser.BirthDate;
                return "Неизвестно";
            }
            set
            {
                //CurrentUser.BirthDate = value;
            }
        }
        public string RegistrationDate
        {
            get
            {
                //return CurrentUser.RegistrationDate;
                return "Неизвестно";
            }
            set
            {
                //CurrentUser.RegistrationDate = value;
            }
        }
        public string Post
        {
            get
            {
                //return CurrentUser.Post;
                return "Неизвестно";
            }
            set
            {
                //CurrentUser.Post = value;
            }
        }
        private void ChangeProfileInfo(object obj)
        {

        }
        private async void Quit(object obj)
        {
            await Navigation.PushAsync(new Entrance());
        }
    }
}
