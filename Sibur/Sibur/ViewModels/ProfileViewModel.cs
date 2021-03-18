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
    public class ProfileViewModel
    {
        UserRequests db = new UserRequests();
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
        public string Raiting
        {
            get
            {  
                return CurrentUser.EngPoints.ToString();
            }
            set
            {
                //CurrentUser.Name = value;
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
        public void GetImage()
        {
            MemoryStream stream = new MemoryStream(Globals.CurrentUser.UserImgs.ToArray()[0].Img);
            System.Drawing.Image imgfromstream = System.Drawing.Image.FromStream(stream);        
        }
        public string WhatImageShow
        {
            get
            {
               
               return string.Format("prefix-{0}-suffix.png");
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
