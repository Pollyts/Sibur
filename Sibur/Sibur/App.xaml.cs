using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Sibur.Views;

namespace Sibur
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new Entrance());
            //MainPage = new NavigationPage(new Activities());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
