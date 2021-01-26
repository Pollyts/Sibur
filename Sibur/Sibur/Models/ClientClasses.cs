using System;
using System.Collections.Generic;
using System.Text;

namespace Sibur.Models
{  
    #region Activities
    public class ActWithCat
    {
        public string name { get; set; }
        public DateTime? startD { get; set; }
        public DateTime? endD { get; set; }
        public string desc { get; set; }
        public ActWithCat()
        {

        }
        public ActWithCat(Activity activity)
        {
            name = activity.Name;
            startD = activity.StartD;
            endD = activity.EndD;
            desc = activity.Description;
        }
        public static implicit operator Activity(ActWithCat b) => new Activity() { Name = b.name, StartD = b.startD, EndD = b.endD, Description = b.desc };
    }
    public class ActWithCatGet : ActWithCat
    {
        public string[] categories { get; set; }
        public ActWithCatGet(Activity activity) : base(activity)
        {

        }
        public ActWithCatGet()
        {

        }

        public ActWithCatGet(Activity activity, string[] cats) : base(activity)
        {
            categories = cats;
        }

    }

    public class ActWithCatPost : ActWithCat
    {
        public ActWithCatPost(string name)
        {
            base.name = name;
        }
        public ActWithCatPost(Activity activity) : base(activity)
        {

        }
        public Category[] categories;
        public ActWithCatPost(Activity activity, Category[] cats) : base(activity)
        {
            categories = cats;
        }

        public static implicit operator Activity(ActWithCatPost b) => new Activity() { Name = b.name, StartD = b.startD, EndD = b.endD, Description = b.desc };

    }

    #endregion
}
