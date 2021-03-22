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
    public class UserActivitiesViewModel : INotifyPropertyChanged
    {
        public ICommand CreateActivityCommand { get; set; }
        public ICommand PerformSearchCommand { get; set; }
        public ICommand OpenActivityCommand { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand EditActivityCommand { get; set; }
        private string _searchText { get; set; }
        public ObservableCollection<string> categories { get; set; }
        public ICommand DeleteActivityCommand { get; set; }

        private bool isBusy;    // идет ли загрузка с сервера

        ActivitiesRequests db = new ActivitiesRequests();
        public Collection<ActWithCatGet> allactivities { get; set; }
        public ObservableCollection<ActWithCatGet> activities { get; set; }
        public UserActivitiesPage activitiesPage { get; set; }
        public INavigation Navigation { get; set; }

        public UserActivitiesViewModel()
        {
            isBusy = false;
            activities = new ObservableCollection<ActWithCatGet>();
            categories = new ObservableCollection<string>();
            OpenActivityCommand = new Command(OpenActivity);
            EditActivityCommand = new Command(EditActivity);
            DeleteActivityCommand = new Command(DeleteActivity);
            PerformSearchCommand = new Command(PerformSearch);
        }
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        public bool ForAdmin
        {
            get
            {
                if (Globals.CurrentUser.Role != null)
                    return true;
                else return false;
            }
        }
        public async Task GetCategories()
        {
            IEnumerable<Category> cats = await db.GetCategories();
            categories.Clear();
            categories.Add("Все категории");
            foreach (Category a in cats)
                categories.Add(a.Name);
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
        private async void OpenActivity(object actobject)
        {
            ActWithCatGet currentact = actobject as ActWithCatGet;
            if (currentact != null)
                await Navigation.PushModalAsync(new CurrentActivity(currentact));
        }
        private async void EditActivity(object actobject)
        {
            ActWithCatGet currentact = actobject as ActWithCatGet;
            if (currentact != null)
                await Navigation.PushModalAsync(new ActivityCreation(currentact));
        }
        private async void DeleteActivity(object actobject)
        {
            ActWithCatGet currentact = actobject as ActWithCatGet;
            if (currentact != null)
            {
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
        public void SortByDate()
        {
            var oldacts = new ObservableCollection<ActWithCatGet>(activities.OrderByDescending(i => i.startD));
            activities.Clear();
            foreach (ActWithCatGet a in oldacts)
                activities.Add(a);
        }
        public void SortByName()
        {
            var oldacts = new ObservableCollection<ActWithCatGet>(activities.OrderBy(i => i.name));
            activities.Clear();
            foreach (ActWithCatGet a in oldacts)
                activities.Add(a);
        }
        public void SelectCategory(string selectedcat)
        {
            if (selectedcat == "Все категории")
            {
                activities.Clear();
                foreach (ActWithCatGet a in allactivities)
                    activities.Add(a);
            }
            else
            {
                activities.Clear();
                var temp = (from act in allactivities where act.categories.ToList().Contains(selectedcat) select act);
                // добавляем загруженные данные
                foreach (ActWithCatGet a in temp)
                    activities.Add(a);
            }
        }
    }
}
