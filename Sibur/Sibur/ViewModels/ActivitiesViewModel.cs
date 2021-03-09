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
        public ICommand PerformSearchCommand { get; set; }
        public ICommand OpenActivityCommand { get; set; }
        public ICommand EditActivityCommand { get; set; }
        public ICommand DeleteActivityCommand { get; set; }
        Activity selectedact;
        //private bool isBusy;    // идет ли загрузка с сервера
        ActivitiesRequests db = new ActivitiesRequests();
        public event PropertyChangedEventHandler PropertyChanged;
        //public ICommand CreateActivityCommand { get; set; }
        public Collection<ActWithCatGet> allactivities { get; set; }
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
            PerformSearchCommand = new Command(PerformSearch);
        }
        public bool ForAdmin
        {
            get {
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
        private string _searchText { get; set; }
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
                ActivitiesPage.Sucess();
                await GetActivities();
            }
            else
            {
                ActivitiesPage.Fail();
            }
        }
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
            allactivities = new ObservableCollection<ActWithCatGet>(activities);
        }
    }
}
