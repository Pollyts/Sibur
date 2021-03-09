using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sibur.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Sibur.ViewModels;

namespace Sibur.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserActivitiesPage : ContentPage
    {
        UserActivitiesViewModel viewModel;
        public UserActivitiesPage()
        {
            InitializeComponent();
            viewModel = new UserActivitiesViewModel() { Navigation = this.Navigation };
            BindingContext = viewModel;
        }
        protected override async void OnAppearing()
        {
            await viewModel.GetActivities();
            base.OnAppearing();
        }
        private async void SelectItemCheck(object sender, SelectedItemChangedEventArgs e)
        {
            if (!viewModel.ForAdmin)
            {
                ActWithCatGet currentact = ActivitiesList.SelectedItem as ActWithCatGet;
                await Navigation.PushModalAsync(new CurrentActivity(currentact));
            }
        }
        public void Fail()
        {
            DisplayAlert("Провалено", "Косяк в данных", "ОK");
        }
        public void Sucess()
        {
            DisplayAlert("Успешно", "Ура, получилось", "ОК");
        }
    }
}