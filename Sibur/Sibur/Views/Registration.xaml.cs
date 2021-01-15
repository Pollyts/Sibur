using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Sibur.Models;
using Sibur.ViewModels;

namespace Sibur.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registration : ContentPage
    {
        public User NewUser { get; private set; }
        public RegistrationViewModel ViewModel { get; private set; }
        public Registration(INavigation n)
        {                    
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            NewUser = new User();
            ViewModel = new RegistrationViewModel() {Navigation = n};
            this.BindingContext = this;
        }
    }
}