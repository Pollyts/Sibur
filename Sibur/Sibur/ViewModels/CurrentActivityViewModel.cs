using System;
using System.Collections.Generic;
using System.Text;
using Sibur.Models;
using Sibur.Requests;

namespace Sibur.ViewModels
{
    public class CurrentActivityViewModel
    {
        public ActWithCatGet CurrentActivity;
        public CurrentActivityViewModel (ActWithCatGet curact)
        {
            CurrentActivity = curact;
        }

    }
}
