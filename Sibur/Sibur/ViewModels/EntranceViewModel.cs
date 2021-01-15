using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;
using Sibur.Models;
using Sibur.Views;

namespace Sibur.ViewModels
{
    public class EntranceViewModel 
    {
        DBRequests db = new DBRequests();
        public ICommand GoEntryCommand { get; set; }
        public INavigation Navigation { get; set; }
        public Entrance View { get; set; }
        public EntranceViewModel()
        {
            GoEntryCommand = new Command(GoEntry);
        }

        public string Mail { get; set; }
        public string Password { get; set; }


        private async void GoEntry(object vm)
        {
            {
                EntranceViewModel entranceViewModel = vm as EntranceViewModel;
                Globals.CurrentUser = await db.Entry(entranceViewModel.Mail, entranceViewModel.Password);
                if ((Globals.CurrentUser != null)&&(Globals.CurrentUser.Mail.Trim()==entranceViewModel.Mail) && (Globals.CurrentUser.Password.Trim() == entranceViewModel.Password))
                {
                    await Navigation.PushAsync(new Tabs());
                }
                else
                {
                    View.Fail();
                }
            }
        }
    }
}
