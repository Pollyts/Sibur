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
    class ActivitiesViewModel : INotifyPropertyChanged
    {
        public ICommand CreateActivityCommand { get; set; }
        public ICommand OpenActivityCommand { get; set; }
        public ICommand EditActivityCommand { get; set; }
        public ICommand DeleteActivityCommand { get; set; }
        Activity selectedact;
        //private bool isBusy;    // идет ли загрузка с сервера
        ActivitiesRequests db = new ActivitiesRequests();
        public event PropertyChangedEventHandler PropertyChanged;
        //public ICommand CreateActivityCommand { get; set; }
        public ObservableCollection<ActWithCatGet> activities { get; set; }
        public INavigation Navigation { get; set; }
        public Activities ActivitiesPage;

        public ActivitiesViewModel()
        {
            activities = new ObservableCollection<ActWithCatGet>();
            CreateActivityCommand = new Command(CreateActivity);
            OpenActivityCommand = new Command(OpenActivity);
            EditActivityCommand = new Command(EditActivity);
            DeleteActivityCommand = new Command(DeleteActivity);
        }

        private async void CreateActivity()
        {
            await Navigation.PushModalAsync(new ActivityCreation());
        }
        private async void OpenActivity(object actobject)
        {
            ActWithCatGet currentact = actobject as ActWithCatGet;
            await Navigation.PushModalAsync(new CurrentActivity(currentact));
        }
        private async void EditActivity()
        {
            await Navigation.PushModalAsync(new ActivityCreation());
        }
        private async void DeleteActivity(object actobject)
        {
            ActWithCatGet currentact = actobject as ActWithCatGet;
            await db.Delete(currentact.id);
        }



        //public Activity SelectedActivity
        //{
        //    get { return selectedact; }
        //    set
        //    {
        //        if (selectedact != value)
        //        {
        //            Activity tempact = new Activity()
        //            {
        //                Id = value.Id,
        //                Name = value.Name,
        //                Description = value.Description,
        //                StartD=value.StartD,
        //                EndD=value.EndD,

        //            };
        //            selectedact = null;
        //            Navigation.PushAsync(new CurrentActivity(tempact));
        //            selectedact = null;
        //        }
        //    }
        //}
        public async Task GetActivities()
        {
            IEnumerable<ActWithCatGet> acts = await db.Get();
            // очищаем список
            //activities.Clear();
            while (activities.Any())
             activities.RemoveAt(activities.Count - 1);

            // добавляем загруженные данные
            foreach (ActWithCatGet a in acts)
                activities.Add(a);
        }
    }
}
