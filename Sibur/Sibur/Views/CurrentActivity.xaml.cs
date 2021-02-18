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
    public partial class CurrentActivity : ContentPage
    {
        public CurrentActivityViewModel viewmodel;
        public CurrentActivity(ActWithCatGet CurrentAct)
        {
            InitializeComponent();
            viewmodel = new CurrentActivityViewModel(CurrentAct);
            BindingContext = viewmodel;            
        }
    }
}