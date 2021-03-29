using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Sibur.Models;
using Sibur.ViewModels;

namespace Sibur.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profile : ContentPage
    {
        public Profile()
        {
            InitializeComponent();
            GetImage();
        }
        protected override void OnAppearing()
        {            
            BindingContext = new ProfileViewModel() { Navigation = this.Navigation, View=this };            
            base.OnAppearing();
        }
        public void GetImage()
        {
            if (Globals.CurrentUser.UserImgs.Count!=0)
            {
                Image_Avatar.Source = ImageSource.FromStream(() => new MemoryStream(Globals.CurrentUser.UserImgs.ToArray()[0].Img));
            }
        }
        public void UpdateInfo()
        {
            L_FIO.Text = Globals.CurrentUser.Name.Trim();
            L_Mail.Text = Globals.CurrentUser.Mail.Trim();
        }

    }
}