using System;
using System.Collections.Generic;
using System.Text;

namespace Sibur.Models
{
    public partial class ActChat
    {
        public int Id { get; set; }
        public int Thanks { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
        public int ActivityId { get; set; }

        public virtual Activity Activity { get; set; }
        public virtual User User { get; set; }
    }
}
