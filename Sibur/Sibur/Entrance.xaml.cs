using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Sibur.Views;
using Sibur.ViewModels;

namespace Sibur
{
    public partial class Entrance : ContentPage
    {
        public EntranceViewModel evm;
        public Entrance()
        {
            InitializeComponent();            
            NavigationPage.SetHasNavigationBar(this, false);
            evm = new EntranceViewModel() {Navigation = this.Navigation, View = this };
            BindingContext = evm;
        }
        public void ShowPass(object sender, EventArgs args)
        {
            Password.IsPassword = Password.IsPassword ? false : true;
        }
        private async void TapGestureRecognizer_OnTapped(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new Registration(this.Navigation));
        }
        public void Fail()
        {
            DisplayAlert("Провалено", "Косяк в данных, ну там логин или пароль неправильные", "ОK");
        }

    }    
}
