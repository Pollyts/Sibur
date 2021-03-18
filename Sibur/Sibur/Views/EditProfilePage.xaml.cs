using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Sibur.Models;
using Xamarin.Essentials;
using System.IO;
using Sibur.ViewModels;

namespace Sibur.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProfilePage : ContentPage
    {
        public FileResult photo;
        public bool changeavatar;
        public EditProfileViewModel viewmodel;
        public EditProfilePage(EditProfileViewModel epvm)
        {
            viewmodel = epvm;
            viewmodel.Navigation = this.Navigation;
            viewmodel.editprofilepage = this;
            BindingContext = viewmodel;
            InitializeComponent();
        }
        public void Fail()
        {
            DisplayAlert("Провалено", "Косяк в данных", "ОK");
        }
        public void Sucess()
        {
            DisplayAlert("Успешно", "Ура, получилось", "ОК");
        }

        private async void B_EditAvatar(object sender, EventArgs e)
        {
            changeavatar = false;
            photo = await MediaPicker.PickPhotoAsync();
            if(photo!=null)
            {
                Avatar_Image.Source = ImageSource.FromFile(photo.FullPath);
                changeavatar = true;
            }            
        }
    }
}