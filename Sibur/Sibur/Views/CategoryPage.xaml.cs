﻿using System;
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
        public CategoriesViewModel viewmodel;
        public CategoryPage(CategoriesViewModel vm)
        {
            viewmodel = vm;
            viewmodel.categoryPage = this;
            BindingContext = viewmodel;
            //helloworld
            InitializeComponent();            
        }
        protected override async void OnAppearing()
        {
            await viewmodel.GetCategories();
            base.OnAppearing();
        }
        void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            viewmodel.Selectedcats.Clear();
            lbl.Text = "Выбранные: ";
            foreach (object cat in e.CurrentSelection)
            {
                viewmodel.Selectedcats.Add(cat as Category);
                lbl.Text += viewmodel.Selectedcats[viewmodel.Selectedcats.Count - 1].Name+"; ";
            }
        }
        public void UpdateCollection()
        {
            CatCollection.SelectedItems = null;            
        }
    }
}