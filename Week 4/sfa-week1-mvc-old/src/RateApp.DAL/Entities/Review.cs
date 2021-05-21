namespace RateApp.DAL.Entities
{
    public class Review : Entity
    {
        public virtual User Reviewer { get; set; }

        public virtual Restaurant Restaurant { get; set; }

        public string ReviewText { get; set; }

        public int Rating { get; set; }
    }
}