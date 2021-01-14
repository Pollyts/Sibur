﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Sibur.Models
{
    public partial class UserQuest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int QuestId { get; set; }
        public bool Status { get; set; }
        public DateTime? ComplitionDate { get; set; }

        public virtual Quest Quest { get; set; }
        public virtual User User { get; set; }
    }
}
