namespace RateApp.DAL.Entities
{
    public class Review : Entity
    {
        public int ReviewerId { get; set; }

        public User Reviewer { get; set; }

        public int RestaurantId { get; set; }

        public Restaurant Restaurant { get; set; }

        public string ReviewText { get; set; }

        public int Rating { get; set; }
    }
}