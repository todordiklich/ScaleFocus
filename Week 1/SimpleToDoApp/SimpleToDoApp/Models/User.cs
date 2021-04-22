using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleToDoApp.Models
{
    public class User : Entity
    {
        private User(int id, DateTime creationDate, int creatorId, DateTime lastChangeDate, int lastChangeUserId)
            :base(id, creationDate, creatorId, lastChangeDate, lastChangeUserId)
        {
            this.ToDoCollection = new List<ToDo>();
        }
        public User(int id, DateTime creationDate, int creatorId, DateTime lastChangeDate, int lastChangeUserId, string username, string password, string firstName, string lastName, bool isAdmin)
            : this(id, creationDate, creatorId, lastChangeDate, lastChangeUserId)
        {
            this.Username = username;
            this.Password = password;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.IsAdmin = isAdmin;
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsAdmin { get; set; }
        public ICollection<ToDo> ToDoCollection { get; set; }
    }
}
