﻿using System;
using System.Collections.Generic;
using System.Text;
//using NpgsqlTypes;

namespace Sibur.Models
{
    public partial class Activity
    {
        public Activity()
        {
            ActAttendings = new HashSet<ActAttending>();
            //ActCategories = new HashSet<ActCategory>(); ;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? StartD { get; set; }
        public DateTime? EndD { get; set; }
        public string Description { get; set; }

        public virtual ICollection<ActAttending> ActAttendings { get; set; }
        public virtual ICollection<ActCategory> ActCategories { get; set; }
        public virtual ICollection<ActChat> ActChat { get; set; }
    }
}
