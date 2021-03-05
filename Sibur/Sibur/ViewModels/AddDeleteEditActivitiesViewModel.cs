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
    public class AddDeleteEditActivitiesViewModel : INotifyPropertyChanged
    {
        public ICommand CreateActivityCommand { get; set; }
        public ICommand  DeleteCategoriesCommand { get; set; }
        public ICommand CreateCategoryCommand { get; set; }
        public ICommand SaveCategoriesCommand { get; set; }
        public ICommand GoBackCommand { get; set; }
        public ICommand DeleteActivityCommand { get; set; }
        public ICommand EditActivityCommand { get; set; }
        public ICommand AddCategoryCommand { get; set; }
        public ICommand GoToActivitiesCommand { get; set; }
        //private bool isBusy;    // идет ли загрузка с сервера
        ActivitiesRequests db = new ActivitiesRequests();
        public Activity currentact { get; set; }
        public ActivityCreation activityCreationpage;
        public Category NewCategory { get; set; }
        public bool isedit=false;
        public CategoryPage categoryPage { get; set; }
       
        public ObservableCollection<Category> categories { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public INavigation Navigation { get; set; }
        public Registration View { get; set; }

        ObservableCollection<Category> selectedcats;
        public ObservableCollection<Category> Selectedcats
        {
            get
            {
                return selectedcats;
            }
            set
            {
                if (selectedcats != value)
                {
                    selectedcats = value;
                }
            }
        }
        public AddDeleteEditActivitiesViewModel()
        {
            currentact = new Activity();
            categories = new ObservableCollection<Category>();
            CreateActivityCommand = new Command(CreateActivity);
            GoBackCommand = new Command(GoBack);
            AddCategoryCommand = new Command(AddCategory);
        }
        public AddDeleteEditActivitiesViewModel(ActWithCatGet oldact)
        {
            currentact = oldact;
            Selectedcats = new ObservableCollection<Category>();
            categories = new ObservableCollection<Category>();
            CreateActivityCommand = new Command(CreateActivity);
            GoBackCommand = new Command(GoBack);
            AddCategoryCommand = new Command(AddCategory);
        }

        //Получить список категорий
        public async Task GetCategories()
        {
            IEnumerable<Category> cats = await db.GetCategories();
            while (categories.Any())
                categories.RemoveAt(categories.Count - 1);
            foreach (Category a in cats)
                categories.Add(a);
        }

        //Получение текущих категорий при редактировании мероприятия
        public async Task GetCategoriesForEditing()
        {
            IEnumerable<Category> cats = await db.GetCategoriesForEditing(currentact.Id);
            while (Selectedcats.Any())
                Selectedcats.RemoveAt(Selectedcats.Count - 1);
            if (cats != null)
                foreach (Category a in cats)
                    Selectedcats.Add(a);
        }
        //Создать мероприятие ActWithCatPost
        private async void CreateActivity()
        {
            bool ifcan=false;
            if (isedit)
            {
                ActWithCatPost act;
                if (Selectedcats == null)
                {
                    act = new ActWithCatPost(currentact);
                }
                else
                {
                    act = new ActWithCatPost(currentact, Selectedcats.ToArray());
                }
                ifcan = await db.Edit(act);
            }
            else
            {
                ActWithCatPost act;
                if (Selectedcats == null)
                {
                    act = new ActWithCatPost(currentact);
                }
                else
                {
                    act = new ActWithCatPost(currentact, Selectedcats.ToArray());
                }
                ifcan = await db.Add(act);                
            }
            if (ifcan)
                activityCreationpage.Sucess();
            else
                activityCreationpage.Fail();
        }


        //Переход по кнопке "Добавить категории" на страницу CategoriyPage
        private async void AddCategory()
        {
            DeleteCategoriesCommand = new Command(DeleteCategories);
            CreateCategoryCommand = new Command(CreateCategory);
            GoToActivitiesCommand = new Command(GoToActivities);
            SaveCategoriesCommand = new Command(SaveCategories);
            NewCategory = new Category() { Id=0};
            Selectedcats = new ObservableCollection<Category>();
            //if((selectedcats!=null)&&(selectedcats.Count>0))
            //        {

            //}
            //Selectedcats= new ObservableCollection<Category>()
            //{
            //    categories[1], categories[3]
            //};
            //Перемещение на страницу со списком категорий
            await Navigation.PushModalAsync(new CategoryPage(this));

        }

        //Возвращение на страницу с созданием мероприятия
        private async void GoToActivities()
        {
            selectedcats.Clear();
            await Navigation.PopModalAsync();
        }
        private async void GoBack()
        {
            await Navigation.PopModalAsync();
        }
        private async void SaveCategories()
        {
            await Navigation.PopModalAsync();
            activityCreationpage.setCategoriesList();
        }
        private async void DeleteCategories()
        {
            foreach (Category category in Selectedcats.ToList())
            {
                if (category.Id==0)
                {
                    categories.Remove(category);
                    Selectedcats.Remove(category);
                }
                else
                await db.DeleteCategory(category.Id);
            }
            await GetCategories();
            categoryPage.UpdateCollection();
            //await Navigation.PopModalAsync();
            //AddCategory();
        }
        private void CreateCategory()
        {
            if((NewCategory.Name!="")&&(NewCategory.Name!=null))
            {
                categories.Add(NewCategory);
                NewCategory = new Category() { Id = 0 };
            }            
        }
        //private async void EditActivity()
        //{
        //    //await db.Add();
        //}
        //private async void DeleteActivity()
        //{
        //    //await db.Add();
        //}
        //public void OnPropertyChanged(string propName)
        //{
        //    if (PropertyChanged != null)
        //        PropertyChanged(this, new PropertyChangedEventArgs(propName));
        //}
    }
}
