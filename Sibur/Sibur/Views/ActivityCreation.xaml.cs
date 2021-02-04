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
    public partial class ActivityCreation : ContentPage
    {
        public AddDeleteEditActivitiesViewModel viewmodel;
        public ActivityCreation()
        {
            viewmodel = new AddDeleteEditActivitiesViewModel() { Navigation = this.Navigation, activityCreationpage = this };
            BindingContext = viewmodel;
            InitializeComponent();            
        }
        protected override async void OnAppearing()
        {
            await viewmodel.GetCategories();
            base.OnAppearing();            
        }
        public void setCategoriesList()
        {
            L_Categories.Text = "Выбранные: ";
            foreach (Category cat in viewmodel.Selectedcats)
            {
                L_Categories.Text += cat.Name + "; ";
            }
        }
        public void Fail()
        {
            DisplayAlert("Провалено", "Косяк в данных", "ОK");
        }
        public void Sucess()
        {
            DisplayAlert("Успешно", "Мероприятие успешно добавлено", "ОK");
        }
    }
}