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
            InitializeComponent();
            viewmodel = new AddDeleteEditActivitiesViewModel(){ Navigation = this.Navigation };
            BindingContext = viewmodel;
        }
        protected override async void OnAppearing()
        {
            await viewmodel.GetCategories();
            base.OnAppearing();            
        }
    }
}