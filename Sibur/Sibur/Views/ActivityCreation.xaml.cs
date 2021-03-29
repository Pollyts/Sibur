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
        public AddEditActivitiesViewModel viewmodel;
        public ActivityCreation(ActWithCatGet oldactivity)
        {
            viewmodel = new AddEditActivitiesViewModel(oldactivity) { Navigation = this.Navigation, activityCreationpage = this };
            BindingContext = viewmodel;
            InitializeComponent();            
        }
        protected override async void OnAppearing()
        {
            if ((!viewmodel.isedit)&&(viewmodel.currentact.Id!=0))
            {
                viewmodel.isedit = true;
                B_AddorEdit.Text = "Сохранить";
                L_Name.Text = "РЕДАКТИРОВАТЬ МЕРОПРИЯТИЕ";
                await viewmodel.GetCategoriesForEditing();
                setCategoriesList();
            }
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
            DisplayAlert("Ошибка", "Попробуйте позже", "ОK");
        }
        public void Sucess()
        {
            DisplayAlert("Успешно", "Мероприятие удалено", "ОK");
        }
    }
}