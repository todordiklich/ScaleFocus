using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsComplete { get; set; }
        public int ToDoId { get; set; }
    }
}
