using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace SimpleToDoApp.Models
{
    public class ToDo : Entity
    {
        public ToDo(int id, DateTime creationDate, int creatorId, DateTime lastChangeDate, int lastChangeUserId, string title)
            :base(id, creationDate, creatorId, lastChangeDate, lastChangeUserId)
        {
            this.Tasks = new List<Task>();
            this.SharedWithUsersIds = new List<int>();

            this.Title = title;
        }

        public string Title { get; set; }
        public ICollection<Task> Tasks { get; set; }
        public ICollection<int> SharedWithUsersIds { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(new string('=', 20))
                 .AppendLine($"Id: {this.Id}")
                 .AppendLine($"Title: {this.Title}")
                 .AppendLine($"This ToDo list is shared with users with the following ids: {string.Join(", ", this.SharedWithUsersIds)}")
                 .AppendLine($"Date of creation: {this.CreationDate}")
                 .AppendLine($"Id of the creator: {this.CreatorId}")
                 .AppendLine($"Date of last change: {this.LastChangeDate}")
                 .AppendLine($"Id of the user that did the last change: {this.LastChangeUserId}")
                 .AppendLine($"Tasks titles: {string.Join(", ", this.Tasks.Select(t => t.Title))}")
                 .AppendLine(new string('=', 20));


            return sb.ToString().TrimEnd();
        }
    }
}
