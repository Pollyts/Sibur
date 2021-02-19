using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Sibur.ViewModels;
using Sibur.Models;

namespace Sibur.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CurrentActivity : ContentPage
    {
        public CurrentActivityViewModel viewmodel;
        public CurrentActivity(ActWithCatGet CurrentAct)
        {            
            viewmodel = new CurrentActivityViewModel(CurrentAct) { CurrentActivityPage=this, Navigation=this.Navigation};
            BindingContext = viewmodel;
            InitializeComponent();
            viewmodel.GetButtonName();
        }
        protected override async void OnAppearing()
        {
            await viewmodel.GetComments();
            base.OnAppearing();
        }
        public void Fail()
        {
            DisplayAlert("Провалено", "Косяк в данных", "ОK");
        }
        public void Sucess()
        {
            DisplayAlert("Успешно", "Всё супер, живём", "ОК");
        }
        public void ButtonText(bool signin)
        {
            if(signin)
            {
                SignUpButton.Text = "Записаться";
            }
            else
            {
                SignUpButton.Text = "Отменить";
            }
        }
    }
}