using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Sibur.Models;

namespace Sibur.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActivityCreation : ContentPage
    {
        public ActWithCatPost newactivity { get; set; }
        public ActivityCreation()
        {
            InitializeComponent();
        }
    }
}