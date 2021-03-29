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
    public partial class RaitingPage : ContentPage
    {
        public RaitingViewModel viewmodel;
        public RaitingPage()
        {
            InitializeComponent();
            viewmodel = new RaitingViewModel() {raitingPage=this };
            BindingContext = viewmodel;            
        }
        private void SelectItemCheck(object sender, SelectedItemChangedEventArgs e)
        {
            if (viewmodel.ForAdmin)
            {
                Grid_ForAdmin.IsVisible = true;
                UserRank ur = RankList.SelectedItem as UserRank;
                if (ur.role!=null)
                {
                    B_MakeAdmin.IsEnabled = false;
                }
                else
                {
                    B_MakeAdmin.IsEnabled = true;
                }
            }
            else
                RankList.SelectedItem = null;
        }
        public void MakeFalseVisibility()
        {
            Grid_ForAdmin.IsVisible = false;
        }
        public void Fail()
        {
            DisplayAlert("Провалено", "Ошибка. Попробуйте еще раз", "ОK");
        }
        public void Sucess()
        {
            DisplayAlert("Успешно", "Данные успешно обновлены", "ОК");
        }
        protected override async void OnAppearing()
        {
            await viewmodel.GetRanks();
            MakeFalseVisibility();
            base.OnAppearing();
        }
    }
}