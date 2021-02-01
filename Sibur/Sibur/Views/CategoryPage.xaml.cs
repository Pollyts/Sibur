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
    public partial class CategoryPage : ContentPage
    {
        public AddDeleteEditActivitiesViewModel viewmodel;
        public CategoryPage(AddDeleteEditActivitiesViewModel vm)
        {
            InitializeComponent();
            viewmodel = vm;
            BindingContext = viewmodel;
        }
        void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            viewmodel.selectedcats.Clear();
            lbl.Text = "Выбранные: ";
            foreach (object cat in e.CurrentSelection)
            {
                viewmodel.selectedcats.Add(cat as Category);
                lbl.Text += viewmodel.selectedcats[viewmodel.selectedcats.Count - 1].Name+"; ";
            }
        }
    }
}