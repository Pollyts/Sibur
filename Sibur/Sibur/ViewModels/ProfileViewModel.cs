using System.Drawing;
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
using Sibur.Requests;
using System.IO;

namespace Sibur.ViewModels
{
    public class ProfileViewModel: INotifyPropertyChanged
    {
        UserRequests db = new UserRequests();
        public ICommand ChangeProfileInfoCommand { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
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
        public void UpdateAvatar()
        {
            View.GetImage();
        }
        //public void UpdateInfo()
        //{
        //    CurrentUser = Globals.CurrentUser;
        //    OnPropertyChanged("Name");
        //    OnPropertyChanged("Mail");
        //}

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        public string Raiting
        {
            get
            {  
                return CurrentUser.EngPoints.ToString();
            }
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
        private async void ChangeProfileInfo(object obj)
        {
            EditProfileViewModel epvm = new EditProfileViewModel(Globals.CurrentUser, this);
            await Navigation.PushModalAsync(new EditProfilePage(epvm));
        }
        private async void Quit(object obj)
        {
            //await Navigation.PushAsync(new Entrance());
            await Navigation.PopToRootAsync();
        }
    }
}
