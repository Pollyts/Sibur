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
    public class RaitingViewModel
    {
        public ICommand PerformSearchCommand { get; set; }
        public ICommand ShowRankCommand { get; set; }
        public ICommand MakeAdminCommand { get; set; }
        public ICommand DeleteUserCommand { get; set; }
        public ICommand ShowMonthRankCommand { get; set; }
        public ObservableCollection<UserRank> UsersMonthRank { get; set; }
        public ObservableCollection<UserRank> UsersRank { get; set; }
        public ObservableCollection<UserRank> CurrentRank { get; set; }
        public ObservableCollection<UserRank> AllCurrentRanks { get; set; }
        public RaitingRequests db = new RaitingRequests();
        public UserRequests dbuser = new UserRequests();
        public string whattype;
        public RaitingPage raitingPage;
        public INavigation Navigation { get; set; }

        public RaitingViewModel()
        {
            whattype = "all";
            CurrentRank = new ObservableCollection<UserRank>();
            UsersMonthRank = new ObservableCollection<UserRank>();
            UsersRank = new ObservableCollection<UserRank>();
            ShowRankCommand = new Command(ShowRank);
            ShowMonthRankCommand = new Command(ShowMonthRank);
            PerformSearchCommand = new Command(PerformSearch);
            DeleteUserCommand = new Command(DeleteUser);
            MakeAdminCommand = new Command(MakeAdmin);
        }
        private async void DeleteUser(object currus)
        {
            UserRank ur = currus as UserRank;
            bool ifcan = await dbuser.Delete(ur.UserId);
            if (ifcan)
            {
                raitingPage.Sucess();                
                await GetRanks();
            }
            else
            {
                raitingPage.Fail();
            }
        }
        private async void MakeAdmin(object currus)
        {
            UserRank ur = currus as UserRank;
            bool ifcan = await dbuser.MakeAdmin(ur.UserId);
            if (ifcan)
            {
                raitingPage.MakeFalseVisibility();
                raitingPage.Sucess();
                await GetRanks();
            }
            else
            {
                raitingPage.Fail();
            }
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
        private void PerformSearch(object search)
        {
            string text = search.ToString();
            if ((text == null) || (text == ""))
            {
                //activities = new ObservableCollection<ActWithCatGet>(allactivities);
                while (CurrentRank.Any())
                    CurrentRank.RemoveAt(CurrentRank.Count - 1);

                // добавляем загруженные данные
                foreach (UserRank a in AllCurrentRanks)
                    CurrentRank.Add(a);
            }
            else
            {
                var temp = (from rank in AllCurrentRanks where rank.name.Contains(text) select rank);
                //activities = new ObservableCollection<ActWithCatGet>(temp);      
                while (CurrentRank.Any())
                    CurrentRank.RemoveAt(CurrentRank.Count - 1);

                // добавляем загруженные данные
                foreach (UserRank a in temp)
                    CurrentRank.Add(a);
            }
        }
        public bool ForAdmin
        {
            get
            {
                if (Globals.CurrentUser.Role != null)
                    return true;
                else return false;
            }
            set
            {
                //isBusy = value;
                //OnPropertyChanged("IsBusy");
                //OnPropertyChanged("IsLoaded");
            }
        }
        public async Task GetRanks()
        {
            IEnumerable<UserRank> urank = await db.GetRank();
            while (UsersRank.Any())
                UsersRank.RemoveAt(UsersRank.Count - 1);
            foreach (UserRank ur in urank)
                UsersRank.Add(ur);
            urank = await db.GetMonthRank();
            while (UsersMonthRank.Any())
                UsersMonthRank.RemoveAt(UsersMonthRank.Count - 1);
            foreach (UserRank ur in urank)
                UsersMonthRank.Add(ur);
            if (whattype == "all")
                ShowRank();
            else
                ShowMonthRank();
            raitingPage.MakeFalseVisibility();
        }

        private void ShowRank()
        {
            //CurrentRank = UsersRank;
            while (CurrentRank.Any())
                CurrentRank.RemoveAt(CurrentRank.Count - 1);
            foreach (UserRank ur in UsersRank)
                CurrentRank.Add(ur);
            AllCurrentRanks = new ObservableCollection<UserRank>(CurrentRank);
            raitingPage.MakeFalseVisibility();
            whattype = "all";
        }
        private void ShowMonthRank()
        {
            //CurrentRank = UsersMonthRank;
            while (CurrentRank.Any())
                CurrentRank.RemoveAt(CurrentRank.Count - 1);
            foreach (UserRank ur in UsersMonthRank)
                CurrentRank.Add(ur);
            AllCurrentRanks = new ObservableCollection<UserRank>(CurrentRank);
            raitingPage.MakeFalseVisibility();
            whattype = "month";
        }
    }
}
