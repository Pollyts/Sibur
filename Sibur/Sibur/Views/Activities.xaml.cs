using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            viewModel = new ActivitiesViewModel() { Navigation = this.Navigation };
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            await viewModel.GetActivities();
            base.OnAppearing();
        }
    }
}