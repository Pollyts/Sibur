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

namespace Sibur.ViewModels
{
    class ActivitiesViewModel : INotifyPropertyChanged
    {
        //private bool isBusy;    // идет ли загрузка с сервера
        ActivitiesRequests db = new ActivitiesRequests();
        public event PropertyChangedEventHandler PropertyChanged;
        //public ICommand CreateActivityCommand { get; set; }
        public ObservableCollection<ActWithCatGet> activities { get; set; }
        public INavigation Navigation { get; set; }
        public Registration View { get; set; }

        public ActivitiesViewModel()
        {
            activities = new ObservableCollection<ActWithCatGet>();
        }

        //public Activity SelectedActivity
        //{
        //    //get { return selectedFriend; }
        //    //set
        //    //{
        //    //    if (selectedFriend != value)
        //    //    {
        //    //        Friend tempFriend = new Friend()
        //    //        {
        //    //            Id = value.Id,
        //    //            Name = value.Name,
        //    //            Email = value.Email,
        //    //            Phone = value.Phone
        //    //        };
        //    //        selectedFriend = null;
        //    //        OnPropertyChanged("SelectedFriend");
        //    //        Navigation.PushAsync(new FriendPage(tempFriend, this));
        //    //    }
        //    //}
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
