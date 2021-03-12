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
    public class AddEditActivitiesViewModel : INotifyPropertyChanged
    {        
        public ICommand CreateActivityCommand { get; set; }   
        public ICommand GoBackCommand { get; set; }
        public ICommand EditActivityCommand { get; set; }
        public ICommand AddCategoryCommand { get; set; }
        public Activity currentact { get; set; }
        public ObservableCollection<Category> Selectedcats { get; set; }

        private bool isBusy;    // идет ли загрузка с сервера
        ActivitiesRequests db = new ActivitiesRequests();       
        public ActivityCreation activityCreationpage;        
        public bool isedit=false;        
        public event PropertyChangedEventHandler PropertyChanged;
        public INavigation Navigation;
                
        public AddEditActivitiesViewModel(ActWithCatGet oldact)
        {
            if (oldact == null)
            {
                currentact = new Activity(); //Создание
            }
            else
            {
                currentact = oldact; //Редактирование
            }
            Selectedcats = new ObservableCollection<Category>();
            CreateActivityCommand = new Command(CreateActivity);
            GoBackCommand = new Command(GoBack);
            AddCategoryCommand = new Command(AddCategory);
        }
        //public AddDeleteEditActivitiesViewModel(ActWithCatGet oldact)
        //{
            
        //    categories = new ObservableCollection<Category>();
        //    CreateActivityCommand = new Command(CreateActivity);
        //    GoBackCommand = new Command(GoBack);
        //    AddCategoryCommand = new Command(AddCategory);
        //}        

        
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

        public async Task GetCategoriesForEditing()
        {
            IEnumerable<Category> cats = await db.GetCategoriesForEditing(currentact.Id);
            while (Selectedcats.Any())
                Selectedcats.RemoveAt(Selectedcats.Count - 1);
            if (cats != null)
                foreach (Category a in cats)
                    Selectedcats.Add(a);
        }

        //Переход по кнопке "Добавить категории" на страницу CategoriyPage
        private async void AddCategory()
        {
            //PerformSearchCommand = new Command(PerformSearch);
            //DeleteCategoriesCommand = new Command(DeleteCategories);
            //CreateCategoryCommand = new Command(CreateCategory);
            //GoToActivitiesCommand = new Command(GoToActivities);
            //SaveCategoriesCommand = new Command(SaveCategories);
            //NewCategory = new Category() { Id=0};
            Selectedcats = new ObservableCollection<Category>();
            //if((selectedcats!=null)&&(selectedcats.Count>0))
            //        {

            //}
            //Selectedcats= new ObservableCollection<Category>()
            //{
            //    categories[1], categories[3]
            //};
            //Перемещение на страницу со списком категорий
            var CategoriesViewModel = new CategoriesViewModel(Selectedcats, currentact, this, Navigation);
            await Navigation.PushModalAsync(new CategoryPage(CategoriesViewModel));

        }
        public void SaveCategoriesList(ObservableCollection<Category> savecats)
        {
            Selectedcats = savecats;
            activityCreationpage.setCategoriesList();
        }
        private async void GoBack()
        {
            await Navigation.PopModalAsync();
        }
    }
}
