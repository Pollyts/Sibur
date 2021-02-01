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
    public class AddDeleteEditActivitiesViewModel
    {
        public ICommand CreateActivityCommand { get; set; }
        public ICommand CreateCategoryCommand { get; set; }
        public ICommand SaveCategoriesCommand { get; set; }
        public ICommand DeleteActivityCommand { get; set; }
        public ICommand EditActivityCommand { get; set; }
        public ICommand AddCategoryCommand { get; set; }
        public ICommand GoToActivitiesCommand { get; set; }
        //private bool isBusy;    // идет ли загрузка с сервера
        ActivitiesRequests db = new ActivitiesRequests();
        public Activity currentact;
        public Category NewCategory { get; set; }


        public ObservableCollection<Category> categories { get; set; }
        public ObservableCollection<Category> selectedcats { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public INavigation Navigation { get; set; }
        public Registration View { get; set; }

        public AddDeleteEditActivitiesViewModel()
        {
            currentact = new Activity();
            categories = new ObservableCollection<Category>();
            CreateActivityCommand = new Command(CreateActivity);
            AddCategoryCommand = new Command(AddCategory);
        }
        public async Task GetCategories()
        {
            IEnumerable<Category> cats = await db.GetCategories();
            // очищаем список
            //activities.Clear();
            while (categories.Any())
                categories.RemoveAt(categories.Count - 1);

            // добавляем загруженные данные
            foreach (Category a in cats)
                categories.Add(a);
        }


        private async void CreateActivity()
        {
            //await db.Add();
        }
        private async void DeleteActivity()
        {
            //await db.Add();
        }
        private async void AddCategory()
        {
            CreateCategoryCommand = new Command(CreateCategory);
            GoToActivitiesCommand = new Command(GoToActivities);
            SaveCategoriesCommand = new Command(SaveCategories);
            NewCategory = new Category() { Id=0};
            selectedcats = new ObservableCollection<Category>();
            //Перемещение на страницу со списком категорий
            await Navigation.PushModalAsync(new CategoryPage(this));

        }
        private async void GoToActivities()
        {
            selectedcats.Clear();
            await Navigation.PopModalAsync();
        }
        private async void SaveCategories()
        {
            await Navigation.PopModalAsync();
        }
        private async void CreateCategory()
        {
            categories.Add(NewCategory);
            NewCategory = new Category() { Id = 0 };
        }
        private async void EditActivity()
        {
            //await db.Add();
        }
    }
}
