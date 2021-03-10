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
    class UserActivitiesViewModel
    {
        public ICommand CreateActivityCommand { get; set; }
        public ICommand PerformSearchCommand { get; set; }
        public ICommand OpenActivityCommand { get; set; }
        public ICommand EditActivityCommand { get; set; }
        private string _searchText { get; set; }
        public ICommand DeleteActivityCommand { get; set; }

        private bool isBusy;    // идет ли загрузка с сервера

        ActivitiesRequests db = new ActivitiesRequests();
        public Collection<ActWithCatGet> allactivities { get; set; }
        public ObservableCollection<ActWithCatGet> activities { get; set; }
        UserActivitiesPage activitiesPage { get; set; }
        public INavigation Navigation { get; set; }

        public UserActivitiesViewModel()
        {
            isBusy = false;
            activities = new ObservableCollection<ActWithCatGet>();
            CreateActivityCommand = new Command(CreateActivity);
            OpenActivityCommand = new Command(OpenActivity);
            EditActivityCommand = new Command(EditActivity);
            DeleteActivityCommand = new Command(DeleteActivity);
            PerformSearchCommand = new Command(PerformSearch);
        }
        public bool ForAdmin
        {
            get
            {
                if (Globals.CurrentUser.Role != null)
                    return true;
                else return false;
            }
            set
            {
                //isBusy = value;
                //OnPropertyChanged("IsBusy");
                //OnPropertyChanged("IsLoaded");
            }
        }
        
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    PerformSearchCommand.Execute(_searchText);
                }
            }
        }
        private void PerformSearch(object search)
        {
            string text = search.ToString();
            if ((text == null) || (text == ""))
            {
                //activities = new ObservableCollection<ActWithCatGet>(allactivities);
                while (activities.Any())
                    activities.RemoveAt(activities.Count - 1);

                // добавляем загруженные данные
                foreach (ActWithCatGet a in allactivities)
                    activities.Add(a);
            }
            else
            {
                var temp = (from act in allactivities where act.name.Contains(text) select act);
                //activities = new ObservableCollection<ActWithCatGet>(temp);      
                while (activities.Any())
                    activities.RemoveAt(activities.Count - 1);

                // добавляем загруженные данные
                foreach (ActWithCatGet a in temp)
                    activities.Add(a);
            }
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
        private async void EditActivity(object actobject)
        {
            ActWithCatGet currentact = actobject as ActWithCatGet;
            await Navigation.PushModalAsync(new ActivityCreation(currentact));
        }
        private async void DeleteActivity(object actobject)
        {
            ActWithCatGet currentact = actobject as ActWithCatGet;
            bool ifcan = await db.Delete(currentact.id);
            if (ifcan)
            {
                activitiesPage.Sucess();
                await GetActivities();
            }
            else
            {
                activitiesPage.Fail();
            }
        }
        public async Task GetActivities()
        {
            while (activities.Any())
                activities.RemoveAt(activities.Count - 1);
            IEnumerable<ActAttending> actattendings = await db.GetUserActivities(Globals.CurrentUser.Id);
            IEnumerable<ActWithCatGet> enumacts = await db.Get();
            ObservableCollection<ActWithCatGet> acts = new ObservableCollection<ActWithCatGet>();
            //while (activities.Any())
            //    activities.RemoveAt(activities.Count - 1);

            // добавляем загруженные данные
            foreach (ActWithCatGet a in enumacts)
                acts.Add(a);
            ObservableCollection<ActAttending> actatt = new ObservableCollection<ActAttending>(actattendings);
            foreach (ActAttending a in actatt)
            {
                var filtered = (from act in acts where act.id == a.ActivityId select act);
                var tmp = new ObservableCollection<ActWithCatGet>(filtered);
                activities.Add(tmp[0]);
            }    
            allactivities = new ObservableCollection<ActWithCatGet>(activities);
        }
    }
}
