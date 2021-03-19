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
    public partial class Activities : ContentPage
    {
        ActivitiesViewModel viewModel;
        public Activities()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            viewModel = new ActivitiesViewModel() { Navigation = this.Navigation, ActivitiesPage=this };
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            viewModel.IsBusy = true;
            await viewModel.GetCategories();
            await viewModel.GetActivities();            
            base.OnAppearing();
            viewModel.IsBusy = false;
        }
        private async void SelectItemCheck(object sender, SelectedItemChangedEventArgs e)
        {
            if (!viewModel.ForAdmin)
            {
                ActWithCatGet currentact = ActivitiesList.SelectedItem as ActWithCatGet;
                await Navigation.PushModalAsync(new CurrentActivity(currentact));
            }            
        }
        private async void ChangeSort(object sender, EventArgs args)
        {
            if(L_Sort.Text=="По имени")
            {
                viewModel.SortByName();
                L_Sort.Text = "По дате";
            }
            else
            {
                viewModel.SortByDate();
                L_Sort.Text = "По имени";
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