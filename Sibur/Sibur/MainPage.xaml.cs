using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Sibur.Views;

namespace Sibur
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();            
            NavigationPage.SetHasNavigationBar(this, false);
        }
        public void ShowPass(object sender, EventArgs args)
        {
            Password.IsPassword = Password.IsPassword ? false : true;
        }
        private async void GoButton(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new Tabs());
        }
        public void GotoFacebook(object sender, EventArgs args)
        {
           
        }
        private async void TapGestureRecognizer_OnTapped(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new Registration());
        }

    }    
}
