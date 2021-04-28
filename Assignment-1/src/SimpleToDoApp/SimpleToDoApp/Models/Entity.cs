using System;

namespace SimpleToDoApp.Models
{
    public abstract class Entity
    {
        public Entity(int id, DateTime creationDate, int creatorId, DateTime lastChangeDate, int lastChangeUserId)
        {
            this.Id = id;
            this.CreationDate = creationDate;
            this.CreatorId = creatorId;
            this.LastChangeDate = lastChangeDate;
            this.LastChangeUserId = lastChangeUserId;
        }
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public int CreatorId { get; set; }
        public DateTime LastChangeDate { get; set; }
        public int LastChangeUserId { get; set; }
    }
}
