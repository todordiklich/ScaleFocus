using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Text.Json;
using RateApp.DAL.Entities;
using System.Data.SqlClient;
using System.Data;

namespace RateApp.DAL.Data
{
    public class Database
    {
        private readonly string _sqlConnectionString;

        public Database(string connectionString)
        {
            _sqlConnectionString = connectionString;
        }

        public bool AddReview(Review review)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_sqlConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = CreateCommand(connection, "INSERT INTO Reviews ([ReviewText], [Rating], [RestaurantId], [ReviewerId]) VALUES " +
                                                                                              "(@ReviewText, @Rating, @RestaurantId, @ReviewerId)"))
                    {
                        AddParameter(command, "@ReviewText", SqlDbType.NVarChar, review.ReviewText);
                        AddParameter(command, "@Rating", SqlDbType.Int, review.Rating);
                        AddParameter(command, "@RestaurantId", SqlDbType.Int, review.Restaurant.Id);
                        AddParameter(command, "@ReviewerId", SqlDbType.Int, review.Reviewer.Id);
                        object result = command.ExecuteScalar();
                        return result != null;
                    }
                }
            }
            catch
            {

            }
            return false;
        }

        public bool AddRestaurantOwner(int restaurantId, int ownerId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_sqlConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = CreateCommand(connection, "INSERT INTO RestaurantOwners ([RestaurantId], [OwnerId]) VALUES " +
                                                                                              "(@RestaurantId, @OwnerId) SELECT SCOPE_IDENTITY()"))
                    {
                        AddParameter(command, "@RestaurantId", SqlDbType.Int, restaurantId);
                        AddParameter(command, "@OwnerId", SqlDbType.Int, ownerId);
                        object result = command.ExecuteScalar();
                        return result != null;
                    }
                }
            }
            catch
            {

            }
            return false;
        }

        public List<User> GetUsers()
        {
            List<User> users = new();
            try
            {
                using (SqlConnection connection = new SqlConnection(_sqlConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = CreateCommand(connection, "SELECT Id, [Name] FROM Users"))
                    {
                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    User user = new();
                                    user.Id = reader.GetInt32(0);
                                    user.Name = reader.GetString(1);
                                    users.Add(user);
                                }
                            }
                        }
                    }
                }
            }
            catch
            {

            }
            return users;
        }

        public User GetUserByName(string userName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_sqlConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = CreateCommand(connection, "SELECT Id, [Name] FROM Users WHERE [Name] = @Name"))
                    {
                        AddParameter(command, "@Name", SqlDbType.NVarChar, userName);

                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {

                            if (reader.HasRows)
                            {
                                reader.Read();
                                User user = new User();
                                user.Id = reader.GetInt32(0);
                                user.Name = reader.GetString(1);
                                return user;
                            }
                        }
                    }
                }
            }
            catch
            {

            }
            return null;
        }

        public User GetUserById(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_sqlConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = CreateCommand(connection, "SELECT Id, [Name] FROM Users WHERE [Id] = @Id"))
                    {
                        AddParameter(command, "@Id", SqlDbType.Int, id);

                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {

                            if (reader.HasRows)
                            {
                                reader.Read();
                                User user = new User();
                                user.Id = reader.GetInt32(0);
                                user.Name = reader.GetString(1);
                                return user;
                            }
                        }
                    }
                }
            }
            catch
            {

            }
            return null;
        }

        public List<User> GetRestaurantOwners(int restaurantId)
        {
            List<User> users = new List<User>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_sqlConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = CreateCommand(connection, "SELECT u.Id, u.Name,u.CreatedAt FROM Restaurants re " +
                                                                          "JOIN RestaurantOwners ro ON ro.RestaurantId = re.Id " +
                                                                          "JOIN Users u ON u.Id = ro.OwnerId WHERE re.Id = @RestaurantId"))
                    {
                        AddParameter(command, "@RestaurantId", SqlDbType.Int, restaurantId);

                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    User user = new User();
                                    user.Id = reader.GetInt32(0);
                                    user.Name = reader.GetString(1);
                                    user.CreatedAt = reader.GetDateTime(2);
                                    users.Add(user);
                                }
                            }
                        }
                    }
                }
            }
            catch
            {

            }
            return users;
        }

        public Restaurant GetRestaurantByName(string restaurantName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_sqlConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = CreateCommand(connection, "SELECT Id, [Name], Description FROM Restaurants WHERE Name= @Name"))
                    {
                        AddParameter(command, "@Name", SqlDbType.NVarChar, restaurantName);

                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (reader.HasRows)
                            {
                                Restaurant restaurant = new();
                                restaurant.Id = reader.GetInt32(0);
                                restaurant.Name = reader.GetString(1);
                                restaurant.Description = reader.GetString(2);
                                return restaurant;
                            }
                        }
                    }
                }
            }
            catch
            {

            }
            return null;
        }

        public Restaurant GetRestaurantById(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_sqlConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = CreateCommand(connection, "SELECT Id, [Name], Description FROM Restaurants WHERE Id = @Id"))
                    {
                        AddParameter(command, "@Id", SqlDbType.Int, id);

                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (reader.HasRows)
                            {

                                Restaurant restaurant = new();
                                reader.Read();
                                restaurant.Id = reader.GetInt32(0);
                                restaurant.Name = reader.GetString(1);
                                restaurant.Description = reader.GetString(2);
                                return restaurant;
                            }
                        }
                    }
                }
            }
            catch
            {

            }
            return null;
        }

        public bool CreateUser(User user)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_sqlConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = CreateCommand(connection, "INSERT INTO Users ([Name]) VALUES (@Name) SELECT SCOPE_IDENTITY()"))
                    {
                        AddParameter(command, "@Name", SqlDbType.NVarChar, user.Name);
                        object result = command.ExecuteScalar();
                        return result != null;
                    }
                }
            }
            catch
            {

            }
            return false;
        }

        public List<Restaurant> GetRestaurants()
        {
            List<Restaurant> restaurants = new();
            try
            {
                using (SqlConnection connection = new SqlConnection(_sqlConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = CreateCommand(connection, "SELECT Id, [Name], Description FROM Restaurants"))
                    {

                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Restaurant restaurant = new();
                                    restaurant.Id = reader.GetInt32(0);
                                    restaurant.Name = reader.GetString(1);
                                    restaurant.Description = reader.GetString(2);
                                    restaurants.Add(restaurant);
                                }
                            }
                        }
                    }
                }
            }
            catch
            {

            }
            return restaurants;
        }

        public int CreateRestaurant(Restaurant restaurant)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_sqlConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = CreateCommand(connection, "INSERT INTO Restaurants ([Name], Description) VALUES " +
                                                                                                  "(@Name, @Description)" +
                                                                                                  "SELECT SCOPE_IDENTITY()"))
                    {
                        {
                            AddParameter(command, "@Name", SqlDbType.NVarChar, restaurant.Name);
                            AddParameter(command, "@Description", SqlDbType.NVarChar, restaurant.Description);
                            object result = command.ExecuteScalar();
                            if (result != null)
                            {
                                return (int)(decimal)result;
                            }
                        }
                    }
                }
            }
            catch
            {

            }
            return 0;
        }

        public List<Review> GetReviewsByRestaurantId(int restaurantId)
        {
            List<Review> reviews = new();
            try
            {
                using (SqlConnection connection = new SqlConnection(_sqlConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = CreateCommand(connection, "SELECT " +
                        "[Id], " +
                        "[ReviewText], " +
                        "[Rating], " +
                        "[RestaurantId], " +
                        "[ReviewerId], " +
                        "[CreatedAt] " +
                        "FROM [dbo].[Reviews] WHERE [RestaurantId] = @RestaurantId"))
                    {

                        AddParameter(command, "@RestaurantId", SqlDbType.Int, restaurantId);

                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Review review = new();
                                    review.Id = reader.GetInt32(0);
                                    review.ReviewText = reader.GetString(1);
                                    review.Rating = reader.GetInt32(2);
                                    review.RestaurantId = reader.GetInt32(3);
                                    review.ReviewerId = reader.GetInt32(4);
                                    review.CreatedAt = reader.GetDateTime(5);
                                    reviews.Add(review);
                                }
                            }
                        }
                    }
                }
            }
            catch
            {

            }
            return reviews;
        }

        public List<Review> GetReviewsByReviewerId(int restaurantId)
        {
            List<Review> reviews = new();
            try
            {
                using (SqlConnection connection = new SqlConnection(_sqlConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = CreateCommand(connection, "SELECT " +
                        "[Id], " +
                        "[ReviewText], " +
                        "[Rating], " +
                        "[RestaurantId], " +
                        "[ReviewerId], " +
                        "[CreatedAt] " +
                        "FROM [dbo].[Reviews] WHERE [ReviewerId] = @ReviewerId"))
                    {

                        AddParameter(command, "@ReviewerId", SqlDbType.Int, restaurantId);

                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Review review = new();
                                    review.Id = reader.GetInt32(0);
                                    review.ReviewText = reader.GetString(1);
                                    review.Rating = reader.GetInt32(2);
                                    review.RestaurantId = reader.GetInt32(3);
                                    review.ReviewerId = reader.GetInt32(4);
                                    review.CreatedAt = reader.GetDateTime(5);
                                    reviews.Add(review);
                                }
                            }
                        }
                    }
                }
            }
            catch
            {

            }
            return reviews;
        }

        private SqlCommand CreateCommand(SqlConnection connection, string sql)
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandText = sql;
            return command;
        }

        private void AddParameter(SqlCommand command, string parametername, SqlDbType parameterType, object parameterValue)
        {
            command.Parameters.Add(parametername, parameterType).Value = parameterValue;
        }
    }
}
