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
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }
        protected override void OnAppearing()
        {            
            base.OnAppearing();
            if (photo != null)
            {
                Avatar_Image.Source = ImageSource.FromFile(photo.FullPath);
                changeavatar = true;
            }
            else
            {
                GetImage();
            }
        }
        private async void B_EditAvatar(object sender, EventArgs e)
        {
            changeavatar = false;
            photo = await MediaPicker.PickPhotoAsync();
            if (photo != null)
            {
                Avatar_Image.Source = ImageSource.FromFile(photo.FullPath);
                changeavatar = true;
            }
        }
        public void GetImage()
        {
            if (Globals.CurrentUser.UserImgs.Count != 0)
            {
                Avatar_Image.Source = ImageSource.FromStream(() => new MemoryStream(Globals.CurrentUser.UserImgs.ToArray()[0].Img));
            }
        }
        public async Task Fail()
        {
            await DisplayAlert("Ошибка", "Не удалось соединиться с сервером. Попробуйте позднее", "ОK");
        }
        public async Task Sucess()
        {
            await DisplayAlert("Успешно", "Мероприятие удалено", "ОК");
        }
    }
}