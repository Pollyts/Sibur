﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;
using Sibur.Models;
using Sibur.Views;
using Sibur.Requests;
using System;
using System.Net.Http;

namespace Sibur.ViewModels
{
    public class EditProfileViewModel: INotifyPropertyChanged
    {
        public ICommand EditUserProfileCommand { get; set; }
        public ICommand DeleteUserProfileCommand { get; set; }
        public ICommand GoBackCommand { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public INavigation Navigation { get; set; }
        ProfileViewModel ProfileViewModel;

        private bool isBusy;
        bool changepicture;

        UserRequests db = new UserRequests();
        public EditProfilePage editprofilepage;

        public User currentuser { get; set; }
        public EditProfileViewModel(User user, ProfileViewModel pvm)
        {
            IsBusy = false;
            ProfileViewModel = pvm;
            currentuser = (User)user.Clone();
            EditUserProfileCommand = new Command(EditUserProfile);
            GoBackCommand = new Command(GoBack);
            DeleteUserProfileCommand = new Command(DeleteUserProfile);
        }

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        //Изменить информацию в профиле
        private async void EditUserProfile()
        {
            IsBusy = true;
            bool ifcanavatar=true;
            if(editprofilepage.changeavatar)
            {
                ifcanavatar = await AddUserAvatar();
            }
            bool ifcan = true;
            if ((Globals.CurrentUser.Name!=currentuser.Name)|| (Globals.CurrentUser.Password != currentuser.Password)||(Globals.CurrentUser.Mail != currentuser.Mail))
            { 
                ifcan = await db.Edit(currentuser);
            }
            if (ifcan&&ifcanavatar)
            {
                Globals.CurrentUser = await db.Entry(currentuser.Mail, currentuser.Password);                
                ProfileViewModel.UpdateInfo();
                ProfileViewModel.UpdateAvatar();
                await editprofilepage.Sucess();                
            }
            else
            {
                await editprofilepage.Fail();
            }
            IsBusy = false;
        }
        private async Task<bool> AddUserAvatar()
        {
            var content = new StreamContent(await editprofilepage.photo.OpenReadAsync());
            UserImg usrimg = new UserImg();
            if (Globals.CurrentUser.UserImgs.Count ==0)
            {
                usrimg = new UserImg() { Img = await content.ReadAsByteArrayAsync(), UserId = Globals.CurrentUser.Id};
            }
            else
            {
                usrimg = new UserImg() { Img = await content.ReadAsByteArrayAsync(), UserId = Globals.CurrentUser.Id, Id = Globals.CurrentUser.UserImgs.ToArray()[0].Id };
            }
            var ifcan = await db.AddImage(usrimg);
            return ifcan;
        }

        private async void DeleteUserProfile()
        {
            IsBusy = true;
            bool ifcan = await db.Delete(currentuser.Id);
            if (ifcan)
            {
                editprofilepage.Sucess();
                await Navigation.PopModalAsync();
                IsBusy = false;
                ProfileViewModel.QuitCommand.Execute(null);
            }
            else
            {
                editprofilepage.Fail();
                IsBusy = false;
            }
        }        
        private async void GoBack()
        {
            await Navigation.PopModalAsync();
        }        
    }
}
