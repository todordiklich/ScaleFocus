using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleToDoApp.Models
{
    public class ToDo : Entity
    {
        public ToDo(int id, DateTime creationDate, int creatorId, DateTime lastChangeDate, int lastChangeUserId, string title)
            :base(id, creationDate, creatorId, lastChangeDate, lastChangeUserId)
        {
            this.Tasks = new List<Task>();

            this.Title = title;
        }

        public string Title { get; set; }
        public ICollection<Task> Tasks { get; set; }
    }
}
