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
        public ICommand ShowRankCommand { get; set; }
        public ICommand ShowMonthRankCommand { get; set; }
        public ObservableCollection<UserRank> UsersMonthRank { get; set; }
        public ObservableCollection<UserRank> UsersRank { get; set; }
        public ObservableCollection<UserRank> CurrentRank { get; set; }
        public RaitingRequests db = new RaitingRequests();
        public INavigation Navigation { get; set; }
        public Activities ActivitiesPage;

        public RaitingViewModel()
        {
            CurrentRank = new ObservableCollection<UserRank>();
            UsersMonthRank = new ObservableCollection<UserRank>();
            UsersRank = new ObservableCollection<UserRank>();
            ShowRankCommand = new Command(ShowRank);
            ShowMonthRankCommand = new Command(ShowMonthRank);
        }
        //public bool ForAdmin
        //{
        //    get
        //    {
        //        if (Globals.CurrentUser.Role != null)
        //            return true;
        //        else return false;
        //    }
        //    set
        //    {
        //        //isBusy = value;
        //        //OnPropertyChanged("IsBusy");
        //        //OnPropertyChanged("IsLoaded");
        //    }
        //}
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
            ShowRank();
        }

        private void ShowRank()
        {
            //CurrentRank = UsersRank;
            while (CurrentRank.Any())
                CurrentRank.RemoveAt(CurrentRank.Count - 1);
            foreach (UserRank ur in UsersRank)
                CurrentRank.Add(ur);
        }
        private void ShowMonthRank()
        {
            //CurrentRank = UsersMonthRank;
            while (CurrentRank.Any())
                CurrentRank.RemoveAt(CurrentRank.Count - 1);
            foreach (UserRank ur in UsersMonthRank)
                CurrentRank.Add(ur);
        }
    }
}
