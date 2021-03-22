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
            viewModel = new UserActivitiesViewModel() { Navigation = this.Navigation, activitiesPage=this };
            BindingContext = viewModel;
        }
        protected override async void OnAppearing()
        {
            viewModel.IsBusy = true;
            ButtonsVisibility.IsVisible = false;
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
            else
            {
                ButtonsVisibility.IsVisible = true;
            }
        }
        private async void ChangeSort(object sender, EventArgs args)
        {
            if (L_Sort.Text == "По имени")
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
            viewModel.SelectCategory(picker.Items[picker.SelectedIndex]);
        }
        public void Fail()
        {
            DisplayAlert("Ошибка", "Не удалось соединиться с сервером. Попробуйте позднее", "ОK");
        }
        public void Sucess()
        {
            DisplayAlert("Успешно", "Мероприятие удалено", "ОК");
        }
    }
}