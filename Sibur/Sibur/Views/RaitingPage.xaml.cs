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
            viewmodel = new RaitingViewModel();
            BindingContext = viewmodel;            
        }
        protected override async void OnAppearing()
        {
            await viewmodel.GetRanks();
            base.OnAppearing();
        }
    }
}