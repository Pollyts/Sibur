using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;
using Sibur.Models;
using Sibur.Views;
using Sibur.Requests;
using System;

namespace Sibur.ViewModels
{
    class AddDeleteEditActivitiesViewModel
    {
        public ICommand CreateActivityCommand { get; set; }
        //private bool isBusy;    // идет ли загрузка с сервера
        ActivitiesRequests db = new ActivitiesRequests();
        public Activity currentact;
        public string[] categories;
        public event PropertyChangedEventHandler PropertyChanged;
        public INavigation Navigation { get; set; }
        public Registration View { get; set; }

        public AddDeleteEditActivitiesViewModel()
        {
            currentact = new Activity();
            CreateActivityCommand = new Command(CreateActivity);
        }       


        private async void CreateActivity()
        {
            //await db.Add();
        }
        private async void DeleteActivity()
        {
            //await db.Add();
        }
        private async void EditActivity()
        {
            //await db.Add();
        }
    }
}
