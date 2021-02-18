using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Sibur.Models;
using Sibur.Requests;

namespace Sibur.ViewModels
{
    public class CurrentActivityViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ActWithCatGet CurrentActivity;
        public CurrentActivityViewModel (ActWithCatGet curact)
        {
            CurrentActivity = curact;
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
        //protected void OnPropertyChanged(string propName)
        //{
        //    if (PropertyChanged != null)
        //        PropertyChanged(this, new PropertyChangedEventArgs(propName));
        //}
    }
}
