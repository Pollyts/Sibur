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
        public ObservableCollection<string> categories { get; set; }

        private bool isBusy;    // идет ли загрузка с сервера
        ActivitiesRequests db = new ActivitiesRequests();
        public event PropertyChangedEventHandler PropertyChanged;
        public Collection<ActWithCatGet> allactivities { get; set; }
        public ObservableCollection<ActWithCatGet> activities { get; set; }
        public INavigation Navigation { get; set; }
        public Activities ActivitiesPage;

        public ActivitiesViewModel()
        {
            
            categories = new ObservableCollection<string>();
            IsBusy = false;
            activities = new ObservableCollection<ActWithCatGet>();
            CreateActivityCommand = new Command(CreateActivity);
            OpenActivityCommand = new Command(OpenActivity);
            EditActivityCommand = new Command(EditActivity);
            DeleteActivityCommand = new Command(DeleteActivity);
            PerformSearchCommand = new Command(PerformSearch);
        }

        //Проверка на админа
        public bool ForAdmin
        {
            get {
                if (Globals.CurrentUser.Role != null)
                    return true;
                else return false;
            }
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
        public async Task GetCategories()
        {
            IEnumerable<Category> cats = await db.GetCategories();
            categories.Clear();
            categories.Add("Все категории");
            foreach (Category a in cats)
                categories.Add(a.Name);
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
            if (selectedcat=="Все категории")
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
        private void PerformSearch(object search)
        {
            string text = search.ToString();
            if ((text == null) || (text == ""))
            {
                activities.Clear();
                foreach (ActWithCatGet a in allactivities)
                    activities.Add(a);
            }
            else
            {
                activities.Clear();
                var temp = (from act in allactivities where act.name.Contains(text) select act); 

                // добавляем загруженные данные
                foreach (ActWithCatGet a in temp)
                    activities.Add(a);
            }
        }
        private async void CreateActivity()
        {
            await Navigation.PushModalAsync(new ActivityCreation(null));
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
                    ActivitiesPage.Sucess();
                    await GetActivities();
                }
                else
                {
                    ActivitiesPage.Fail();
                }
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
