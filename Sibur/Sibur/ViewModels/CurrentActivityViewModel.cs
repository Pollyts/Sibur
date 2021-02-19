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
    public class CurrentActivityViewModel : INotifyPropertyChanged
    {        
        ActivitiesRequests db = new ActivitiesRequests();
        public ICommand SignUpCommand { get; set; }
        public ActAttending currentattending;
        public ICommand AddCommentCommand { get; set; }
        public ICommand GoBackCommand { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public ActWithCatGet CurrentActivity;
        public CurrentActivity CurrentActivityPage;
        public ObservableCollection<ActChat> comments { get; set; }
        public INavigation Navigation { get; set; }
        public string TextComment { get; set; }
        public CurrentActivityViewModel (ActWithCatGet curact)
        {
            CurrentActivity = curact;            
            comments = new ObservableCollection<ActChat>();            
            SignUpCommand = new Command(SignUp);
            AddCommentCommand = new Command(AddComment);
            GoBackCommand = new Command(GoBack);
        }

        //Подгрузка комментариев
        public async Task GetComments()
        {
            IEnumerable<ActChat> com = await db.GetComments(CurrentActivity.id);
            // очищаем список
            //activities.Clear();
            while (comments.Any())
                comments.RemoveAt(comments.Count - 1);

            // добавляем загруженные данные
            foreach (ActChat c in com)
                comments.Add(c);
        }

        //Переименовывает кнопку
        public void GetButtonName()
        {
            currentattending = Globals.CurrentUser.ActAttendings.ToList().Find(r => r.ActivityId == CurrentActivity.id);
            if (currentattending == null)
                CurrentActivityPage.ButtonText(true);
            else
                CurrentActivityPage.ButtonText(false);
        }
        //Записаться на мероприятие или отписаться
        private async void SignUp()
        {
            if (currentattending != null)
            {
                bool ifcan = await db.SignOut(CurrentActivity.id, Globals.CurrentUser.Id);
                if (ifcan)
                {
                    UserRequests us = new UserRequests();
                    CurrentActivityPage.Sucess();
                    Globals.CurrentUser = await us.Entry(Globals.CurrentUser.Mail, Globals.CurrentUser.Password);
                    currentattending = null;
                    GetButtonName();
                }
                else
                    CurrentActivityPage.Fail();
            }
            else
            {
                bool ifcan = await db.SignUp(CurrentActivity.id, Globals.CurrentUser.Id);
                if (ifcan)
                {
                    UserRequests us = new UserRequests();
                    CurrentActivityPage.Sucess();
                    Globals.CurrentUser = await us.Entry(Globals.CurrentUser.Mail, Globals.CurrentUser.Password);
                    currentattending = Globals.CurrentUser.ActAttendings.ToList().Find(r => r.ActivityId == CurrentActivity.id);
                    GetButtonName();
                }
                else
                    CurrentActivityPage.Fail();
            }
        }
        
        //Вернуться к списку мероприятий
        private async void GoBack()
        {
            await Navigation.PopModalAsync();
        }

        //Добавление комментария
        private async void AddComment()
        {
            if((TextComment=="")||(TextComment == null))
                    {
                CurrentActivityPage.Fail();
                return;
            }
            bool ifcan = await db.AddComment(CurrentActivity.id, Globals.CurrentUser.Id, TextComment);
            if (ifcan)
            {
                CurrentActivityPage.Sucess();
                await GetComments();
            }
            else
                CurrentActivityPage.Fail();
        }
        public string Name
        {
            get { return CurrentActivity.name; }
            set
            {
                if (CurrentActivity.name != value)
                {
                    CurrentActivity.name = value;
                    //OnPropertyChanged("Name");
                }
            }
        }
        public string Description
        {
            get { return CurrentActivity.desc; }
            set
            {
                if (CurrentActivity.desc != value)
                {
                    CurrentActivity.desc = value;
                    //OnPropertyChanged("Description");
                }
            }
        }
        public string Categories
        {
            get {
                if (CurrentActivity.categories.Length == 0)
                    return "Нет";
                string output = "";
                foreach(string cat in CurrentActivity.categories)
                {
                    output += cat + "; ";
                }
                return output; 
            }
            set
            {
                if (CurrentActivity.desc != value)
                {
                    CurrentActivity.desc = value;
                    //OnPropertyChanged("Description");
                }
            }
        }
        public string StartDate
        {
            get { return CurrentActivity.startD.ToString(); }
            set
            {
                if (CurrentActivity.desc != value)
                {
                    CurrentActivity.desc = value;
                    //OnPropertyChanged("Description");
                }
            }
        }
        public string EndDate
        {
            get { return CurrentActivity.endD.ToString(); }
            set
            {
                if (CurrentActivity.desc != value)
                {
                    CurrentActivity.desc = value;
                    //OnPropertyChanged("Description");
                }
            }
        }
    }
}
