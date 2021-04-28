using System;
using System.Text;
using System.Collections.Generic;

namespace SimpleToDoApp.Models
{
    public class Task : Entity
    {
        public Task(int id, DateTime creationDate, int creatorId, DateTime lastChangeDate, int lastChangeUserId, string title, string description, bool isComplete, int toDoId)
            : base(id, creationDate, creatorId, lastChangeDate, lastChangeUserId)
        {
            this.Title = title;
            this.Description = description;
            this.IsComplete = isComplete;
            this.ToDoId = toDoId;
            this.UsersAssignedToTask = new List<int>();
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsComplete { get; set; }
        public int ToDoId { get; set; }
        public ICollection<int> UsersAssignedToTask { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(new string('=', 20))
                 .AppendLine($"Id: {this.Id}")
                 .AppendLine($"Title: {this.Title}")
                 .AppendLine($"Description: {this.Description}")
                 .AppendLine($"IsCompleted: {this.IsComplete}")
                 .AppendLine($"ToDo List Id: {this.ToDoId}")
                 .AppendLine($"Date of creation: {this.CreationDate}")
                 .AppendLine($"Id of the creator: {this.CreatorId}")
                 .AppendLine($"Date of last change: {this.LastChangeDate}")
                 .AppendLine($"Id of the user that did the last change: {this.LastChangeUserId}")
                 .AppendLine($"Users Ids assigned to Task: {String.Join(", ", this.UsersAssignedToTask)}")
                 .AppendLine(new string('=', 20));


            return sb.ToString().TrimEnd();
        }
    }
}
