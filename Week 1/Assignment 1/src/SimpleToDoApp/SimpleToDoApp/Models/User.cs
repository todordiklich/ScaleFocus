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

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine(new string('=', 20))
                .AppendLine($"Id: {this.Id}")
                .AppendLine($"Username: {this.Username}")
                .AppendLine($"Password: {this.Password}")
                .AppendLine($"First Name: {this.FirstName}")
                .AppendLine($"Last Name: {this.LastName}")
                .AppendLine($"Role: {(this.IsAdmin == true ? "Admin" : "RegularUser")}")
                .AppendLine($"Date of creation: {this.CreationDate}")
                .AppendLine($"Id of the creator: {this.CreatorId}")
                .AppendLine($"Date of last change: {this.LastChangeDate}")
                .AppendLine($"Id of the user that did the last change: {this.LastChangeUserId}")
                .AppendLine(new string('=', 20));

            return sb.ToString().TrimEnd();
        }
    }
}
