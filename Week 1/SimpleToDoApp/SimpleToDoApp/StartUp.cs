using SimpleToDoApp.Models;
using SimpleToDoApp.Services;
using System;

namespace SimpleToDoApp
{
    public class StartUp
    {
        private static UserService _userService = new UserService();
        static void Main(string[] args)
        {
            bool shouldExit = false;
            while (!shouldExit)
            {
                shouldExit = MainMenu();
            }
        }

        private static bool MainMenu()
        {
            RenderUsersManagementViewMenu();

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
                    CreateUser();
                    return false;
                case "c":
                    Console.Clear();
                    return false;
                case "e":
                    return true;
                default:
                    Console.WriteLine("Unknown Command");
                    return false;
            }

        }

        private static void RenderUsersManagementViewMenu()
        {
            Console.WriteLine("--------Users Management View--------");
            if (_userService.CurrentUser == null)
            {
                Console.WriteLine("1. LogIn ");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"You are logged in as: {_userService.CurrentUser.Username}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("1. LogOut");
                Console.WriteLine("2. Create User");
            }

            Console.WriteLine(new string('=', 37));
            Console.WriteLine("Clear console (c)");
            Console.WriteLine("Exit the program (e)");
            Console.WriteLine();
        }

        private static void LogOut()
        {
            _userService.LogOut();
        }

        private static void LogIn()
        {
            Console.WriteLine("Enter your user name:");
            string username = Console.ReadLine();
            Console.WriteLine("Enter your password:");
            string password = Console.ReadLine();

            _userService.Login(username, password);

            if (_userService.CurrentUser == null)
            {
                Console.WriteLine("Login failed! Wrong username or password!");
            }
            else
            {
                Console.WriteLine("Login successful!");
            }
        }

        private static void CreateUser()
        {
            if (_userService.CurrentUser == null || !_userService.CurrentUser.IsAdmin)
            {
                Console.WriteLine("Only users who are admins can create new user!");
                return;
            }

            Console.WriteLine("Username:");
            string username = Console.ReadLine();
            Console.WriteLine("Password:");
            string password = Console.ReadLine();
            Console.WriteLine("User First Name:");
            string firstName = Console.ReadLine();
            Console.WriteLine("User Last Name:");
            string lastName = Console.ReadLine();
            Console.WriteLine("Give admin rights (true/false):");
            bool isAdmin = Console.ReadLine() == "true" ? true : false;

            bool isSuccess = _userService.CreateUser(username, password, firstName, lastName, isAdmin, _userService.CurrentUser);
            if (isSuccess)
            {
                Console.WriteLine($"User with name '{username}' added");
            }
            else
            {
                Console.WriteLine($"User with name '{username}' already exists");
                CreateUser();
            }
        }
    }
}
