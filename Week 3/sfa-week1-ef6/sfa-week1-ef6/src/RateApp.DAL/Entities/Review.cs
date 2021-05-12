namespace RateApp.DAL.Entities
{
    public class Review : Entity
    {

        public Review() : base()
        {
        }

        public User Reviewer { get; set; }

        public Restaurant Restaurant { get; set; }

        public string ReviewText { get; set; }

        public int Rating { get; set; }
    }
}