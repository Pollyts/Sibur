using System;
using System.Collections.Generic;
using System.Text;

namespace Sibur.Models
{
    public partial class Assignment
    {
        public Assignment()
        {
            QuestTasks = new HashSet<QuestTask>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<QuestTask> QuestTasks { get; set; }
    }
}
