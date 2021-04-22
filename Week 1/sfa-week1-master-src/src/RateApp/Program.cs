using RateApp.Entities;
using RateApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RateApp
{
    class Program
    {
        private static UserService _userService = new UserService();
        private static RestaurantService _restaurantService = new RestaurantService();

        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                _userService.Login(args[0]);
            }
            bool shouldExit = false;
            while (!shouldExit)
            {
                shouldExit = MainMenu();
            }
        }

        private static bool MainMenu()
        {
            RenderMenu();

            string userChoice = Console.ReadLine();

            switch (userChoice)
            {
                case "1":
                    if (_userService.CurrentUser == null)
                    {
                        LogIn();
                    }
                    else
                    {
                        LogOut();
                    }
                    return false;
                case "2":
                    CreateRestaurant();
                    return false;
                case "3":
                    AddUser();
                    return false;
                case "4":
                case "l":
                    ListRestaurants();
                    return false;
                case "5":
                case "r":
                    WriteReview();
                    return false;
                case "6":
                case "m":
                    MyReviews();
                    return false;
                case "7":
                    ListRestaurantReviews();
                    return false;
                case "c":
                    Console.Clear();
                    return false;
                case "exit":
                    return true;
                default:
                    Console.WriteLine("Unknown Command");
                    return false;
            }

        }

        private static void RenderMenu()
        {
            Console.WriteLine("--------Main Menu--------");
            if (_userService.CurrentUser == null)
            {
                Console.WriteLine("1. LogIn ");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"You are logged in as: {_userService.CurrentUser.Name}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("1. LogOut");
            }
            Console.WriteLine("2. Create Restaurant");
            Console.WriteLine("3. Add User");
            Console.WriteLine("4. List Restaurants (l)");
            Console.WriteLine("5. Write review (r)");
            Console.WriteLine("6. My reviews (m)");
            Console.WriteLine("7. Reviews for restaurant");
        }

        private static void MyReviews()
        {
            List<Review> reviews = _restaurantService.GetReviewsByUser(_userService.CurrentUser.Id);
            DisplayReviews(reviews);
        }

        private static void DisplayReviews(List<Review> reviews)
        {
            if (reviews.Any())
            {
                foreach (Review review in reviews)
                {
                    Console.WriteLine("==========================================");
                    Console.WriteLine($"Restaurant: {review.Restaurant.Name} ");
                    Console.WriteLine($"Reviewer: {review.Reviewer.Name} ");
                    Console.WriteLine($"On: {review.CreatedAt} ");
                    Console.WriteLine($"{review.ReviewText} ");
                    Console.WriteLine("==========================================");
                }
            }
            else
            {
                Console.WriteLine("==========================================");
                Console.WriteLine("No Reviews");
                Console.WriteLine("==========================================");
            }
        }

        private static void WriteReview()
        {
            if (_userService.CurrentUser == null)
            {
                Console.WriteLine("Can't Write anonymous reviews.");
                return;
            }

            Console.WriteLine("Id of the restaurant to add review:");

            string stringId = Console.ReadLine();
            int id;

            if (int.TryParse(stringId, out id))
            {
                Restaurant restaurant = _restaurantService.GetRestaurant(id);
                if (restaurant != null)
                {
                    Console.WriteLine("Review:");
                    string text = Console.ReadLine();
                    Console.WriteLine("Rating:");
                    string stringRating = Console.ReadLine();
                    int rating = int.Parse(stringRating);
                    _restaurantService.Review(restaurant, _userService.CurrentUser, text, rating);

                    return;
                }
            }

            Console.WriteLine("Restaurant does not exist");
        }

        private static void ListRestaurants()
        {
            List<Restaurant> restaurants = _restaurantService.GetAllRestaurants();
            if (restaurants.Any())
            {
                foreach (Restaurant restaurant in restaurants)
                {
                    Console.WriteLine($"========================={restaurant.Id}===========================");
                    Console.WriteLine($"{restaurant.Name}");
                    Console.WriteLine($"{restaurant.Description}");
                    Console.WriteLine($"{restaurant.Reviews.Count} Reviews");
                    Console.WriteLine("=======================================================");
                }
            }
            else
            {
                Console.WriteLine("=======================================================");
                Console.WriteLine("No restaurants found");
                Console.WriteLine("=======================================================");
            }
        }

        private static void CreateRestaurant()
        {
            Console.WriteLine("Restaurant Name:");
            string name = Console.ReadLine();
            Console.WriteLine("Restaurant Description:");
            string description = Console.ReadLine();

            bool isSuccess = _restaurantService.CreateRestaurant(name, description, _userService.CurrentUser);
            if (isSuccess)
            {
                Console.WriteLine($"You have created a restaurant called '{name}'");
            }
            else
            {
                Console.WriteLine($"Creating restaurant '{name}' failed");
            }
        }

        private static void ListRestaurantReviews()
        {
            Console.WriteLine("Restaurant id:");
            string stringId = Console.ReadLine();
            int id = int.Parse(stringId);
            List<Review> reviews = _restaurantService.GetReviewsForRestaurant(id);
            DisplayReviews(reviews);
        }

        private static void LogOut()
        {
            _userService.LogOut();
        }

        private static void LogIn()
        {
            Console.WriteLine("Enter your user name:");
            string userName = Console.ReadLine();
            _userService.Login(userName);
            if (_userService.CurrentUser == null)
            {
                Console.WriteLine("Login failed.");
            }
            else
            {
                Console.WriteLine("Login successful.");
            }
        }

        private static void AddUser()
        {
            if (_userService.CurrentUser == null || _userService.CurrentUser.Name != "yavor")
            {
                Console.WriteLine("Only yavor can create users");
                return;
            }

            Console.WriteLine("User Name:");
            string name = Console.ReadLine();

            bool isSuccess = _userService.CreateUser(name);
            if (isSuccess)
            {
                Console.WriteLine($"User with name '{name}' added");
            }
            else
            {
                Console.WriteLine($"User with name '{name}' already exists");
                AddUser();
            }
        }

    }
}
