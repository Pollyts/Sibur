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
    public class EntranceViewModel : INotifyPropertyChanged
    {
        private bool isBusy;    // идет ли загрузка с сервера
        public event PropertyChangedEventHandler PropertyChanged;
        UserRequests db = new UserRequests();
        public ICommand GoEntryCommand { get; set; }
        public INavigation Navigation { get; set; }
        public Entrance View { get; set; }
        public EntranceViewModel()
        {
            IsBusy = false;
            GoEntryCommand = new Command(GoEntry);
        }

        public string Mail { get; set; }
        public string Password { get; set; }

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                OnPropertyChanged("IsBusy");
                OnPropertyChanged("IsLoaded");
            }
        }
        public bool IsLoaded
        {
            get { return !isBusy; }
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        private async void GoEntry(object vm)
        {
            {
                IsBusy = true;
                EntranceViewModel entranceViewModel = vm as EntranceViewModel;
                if(entranceViewModel.Mail!=null && entranceViewModel.Password!=null)
                Globals.CurrentUser = await db.Entry(entranceViewModel.Mail, entranceViewModel.Password);
                if ((Globals.CurrentUser != null)&&(Globals.CurrentUser.Mail.Trim()==entranceViewModel.Mail) && (Globals.CurrentUser.Password.Trim() == entranceViewModel.Password))
                {
                    await Navigation.PushAsync(new Tabs());
                }
                else
                {
                    View.Fail();
                }
                IsBusy = false;
            }
        }
    }
}
