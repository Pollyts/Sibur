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
        public User NewUser { get; set;}
        public RegistrationViewModel ViewModel { get; private set; }
        public Registration(INavigation n)
        {                    
            InitializeComponent();
            NewUser = new User(true);
            NavigationPage.SetHasNavigationBar(this, false);
            ViewModel = new RegistrationViewModel() {Navigation = n, View=this};
            this.BindingContext = this;
        }
        public void Success()
        {
            DisplayAlert("Успешно", "Вы зарегистрировались в приложении", "ОK");
        }
        public void Fail()
        {
            DisplayAlert("Провалено", "Где-то косяк", "ОK");
        }
    }
}