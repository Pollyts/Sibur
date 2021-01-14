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
            //Button button = (Button)sender;
            //button.Text = "Нажато!";
            //button.BackgroundColor = Color.Red;
            await Navigation.PushAsync(new TabbedPage1());
        }
        public void GotoFacebook(object sender, EventArgs args)
        {
           
        }
        private void TapGestureRecognizer_OnTapped(object sender, EventArgs args)
        {
            Navigation.PushAsync(new Registration());
        }

    }    
}
