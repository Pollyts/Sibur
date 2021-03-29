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
            viewModel = new ActivitiesViewModel() { Navigation = this.Navigation, ActivitiesPage=this };
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            ActivitiesList.IsVisible = false;
            viewModel.IsBusy = true;
            ActivitiesList.SelectedItem = null;
            ButtonsVisibility.IsVisible = false;
            picker.SelectedIndex = -1;                 
            base.OnAppearing();
            await viewModel.GetCategories();
            await viewModel.GetActivities();
            ActivitiesList.IsVisible = true;
            viewModel.IsBusy = false;            
        }
        private async void SelectItemCheck(object sender, SelectedItemChangedEventArgs e)
        {
            if (!viewModel.ForAdmin)
            {
                ActWithCatGet currentact = ActivitiesList.SelectedItem as ActWithCatGet;
                viewModel.OpenActivityCommand.Execute(currentact);
            }
            else
            {
                ButtonsVisibility.IsVisible = true;
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
        void picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(picker.SelectedIndex!=-1)
            viewModel.SelectCategory(picker.Items[picker.SelectedIndex]);
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