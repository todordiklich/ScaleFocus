namespace RateApp.Entities
{
    public class Review : Entity
    {
        public User Reviewer { get; set; }

        public Restaurant Restaurant { get; set; }

        public string ReviewText { get; set; }

        public int Rating { get; set; }
    }
}