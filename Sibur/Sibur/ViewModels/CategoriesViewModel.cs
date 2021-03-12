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
    public class CategoriesViewModel
    {
        public ICommand PerformSearchCommand { get; set; }
        public ICommand DeleteCategoriesCommand { get; set; }
        public ICommand CreateCategoryCommand { get; set; }
        public ICommand GoToActivitiesCommand { get; set; }
        public ICommand SaveCategoriesCommand { get; set; }
        public CategoryPage categoryPage { get; set; }
        public ObservableCollection<Category> categories { get; set; }
        public Collection<Category> allcategories { get; set; }
        public ObservableCollection<Category> Selectedcats { get; set; }
        public Category NewCategory { get; set; }
        ActivitiesRequests db = new ActivitiesRequests();
        public Activity currentact;
        AddEditActivitiesViewModel addeditvm;
        public INavigation Navigation;
        public CategoriesViewModel(ObservableCollection<Category>catsforedit, Activity act, AddEditActivitiesViewModel oldvm, INavigation nav)
        {
            PerformSearchCommand = new Command(PerformSearch);
            DeleteCategoriesCommand = new Command(DeleteCategories);
            CreateCategoryCommand = new Command(CreateCategory);
            GoToActivitiesCommand = new Command(GoToActivities);
            SaveCategoriesCommand = new Command(SaveCategories);
            categories = new ObservableCollection<Category>();
            NewCategory = new Category() { Id = 0 };
            Selectedcats = new ObservableCollection<Category>();
            currentact = act;
            NewCategory = new Category() { Id = 0 };
            addeditvm = oldvm;
            Navigation = nav;
        }        

        //Получить список категорий
        public async Task GetCategories()
        {
            IEnumerable<Category> cats = await db.GetCategories();
            categories.Clear();
            foreach (Category a in cats)
                categories.Add(a);
            allcategories = new ObservableCollection<Category>(categories);
        }

        //Получение текущих категорий при редактировании мероприятия        
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
                categories.Clear();
                foreach (Category a in allcategories)
                    categories.Add(a);
            }
            else
            {
                var temp = (from cat in allcategories where cat.Name.Contains(text) select cat);
                categories.Clear();
                foreach (Category a in temp)
                    categories.Add(a);
            }
        }
        //Возвращение на страницу с созданием мероприятия
        private async void GoToActivities()
        {
            await Navigation.PopModalAsync();
        }
        private async void SaveCategories()
        {
            addeditvm.SaveCategoriesList(Selectedcats);
            await Navigation.PopModalAsync();
        }
        private async void DeleteCategories()
        {
            foreach (Category category in Selectedcats.ToList())
            {
                if (category.Id == 0)
                {
                    categories.Remove(category);
                    Selectedcats.Remove(category);
                }
                else
                    await db.DeleteCategory(category.Id);
            }
            await GetCategories();
            categoryPage.UpdateCollection();
        }
        private void CreateCategory()
        {
            if ((NewCategory.Name != "") && (NewCategory.Name != null))
            {
                categories.Add(NewCategory);
                allcategories = new ObservableCollection<Category>(categories);
                NewCategory = new Category() { Id = 0 };
            }
        }
    }
}
