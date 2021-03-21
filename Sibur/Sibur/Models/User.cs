using System;
using System.Collections.Generic;
using System.Text;

namespace Sibur.Models
{
    public partial class User : ICloneable
    {
        public User()
        {

        }
        public User(bool a)
        {
            ActAttendings = new HashSet<ActAttending>();
            ActChats = new HashSet<ActChat>();
            QuestTaskUsers = new HashSet<QuestTaskUser>();
            UserImgs = new HashSet<UserImg>();
            UserQuests = new HashSet<UserQuest>();
            UsersKpihistories = new HashSet<UsersKpihistory>();
            MailConfirm = true;
        }
        public User(User user)
        {
            Id = user.Id;
            Mail = user.Mail;
            Password = user.Password;
            Name = user.Name;
        }
        public object Clone()
        {
            return new User {
                Name = this.Name,
                Id = this.Id,
                Mail = this.Mail,
                Password = this.Password,
                MailConfirm = this.MailConfirm,
                Currency=this.Currency,
                Thanks=this.Thanks,
                Role=this.Role,
                Display=this.Display,
                EngPoints=this.EngPoints,
                LastEntry=this.LastEntry,
                Bonus=this.Bonus,
                ActAttendings=this.ActAttendings,
                ActChats=this.ActChats,
                QuestTaskUsers=this.QuestTaskUsers,
                UserImgs=this.UserImgs,
                UserQuests=this.UserQuests,
                UsersKpihistories=this.UsersKpihistories  
            };
        }

        public int Id { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public bool MailConfirm { get; set; }
        public string Name { get; set; }
        public int Currency { get; set; }
        public int Thanks { get; set; }
        public char? Role { get; set; }
        public bool Display { get; set; }
        public float EngPoints { get; set; }
        public DateTime LastEntry { get; set; }
        public float Bonus { get; set; }

        public virtual ICollection<ActAttending> ActAttendings { get; set; }
        public virtual ICollection<ActChat> ActChats { get; set; }
        public virtual ICollection<QuestTaskUser> QuestTaskUsers { get; set; }
        public virtual ICollection<UserImg> UserImgs { get; set; }
        public virtual ICollection<UserQuest> UserQuests { get; set; }
        public virtual ICollection<UsersKpihistory> UsersKpihistories { get; set; }        
    }
}
