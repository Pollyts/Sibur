﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Sibur.Models
{
    public class ChatPhoto
    {
        public int Id { get; set; }
        public int ActChatId { get; set; }
        public byte[] Photo { get; set; }

        public virtual ActChat ActChat { get; set; }
    }
}
