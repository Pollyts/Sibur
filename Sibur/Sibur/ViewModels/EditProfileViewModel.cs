using System.Collections.Generic;
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

namespace Sibur.ViewModels
{
    public class EditProfileViewModel
    {
        public ICommand EditUserProfileCommand { get; set; }
        public ICommand DeleteUserProfileCommand { get; set; }
        public ICommand GoBackCommand { get; set; }
        public INavigation Navigation { get; set; }

        private bool isBusy;

        UserRequests db = new UserRequests();
        public EditProfilePage editprofilepage;

        public User currentuser { get; set; }
        public EditProfileViewModel(User user)
        {
            currentuser = user;
            EditUserProfileCommand = new Command(EditUserProfile);
            GoBackCommand = new Command(GoBack);
            DeleteUserProfileCommand = new Command(DeleteUserProfile);
        }    
        
        //Изменить информацию в профиле
        private async void EditUserProfile()
        {            
            bool ifcan = await db.Edit(currentuser);
            if (ifcan)
            {
                editprofilepage.Sucess();
            }
            else
            {
                editprofilepage.Fail();
            }
        }
        
        private async void DeleteUserProfile()
        {
            bool ifcan = await db.Delete(currentuser.Id);
            if (ifcan)
            {
                editprofilepage.Sucess();
                await Navigation.PopToRootAsync();
            }
            else
            {
                editprofilepage.Fail();
            }
        }        
        private async void GoBack()
        {
            await Navigation.PopModalAsync();
        }        
    }
}
