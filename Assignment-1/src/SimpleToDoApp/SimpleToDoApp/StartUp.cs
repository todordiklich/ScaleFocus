using System;
using System.Linq;
using System.Collections.Generic;

using SimpleToDoApp.Models;
using SimpleToDoApp.CLITool;
using SimpleToDoApp.Services;

namespace SimpleToDoApp
{
    public class StartUp
    {
        private static UserService _userService = new UserService();
        private static ToDoService _toDoService = new ToDoService();
        private static TaskService _taskService = new TaskService();
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                CLI CLI = new CLI(args);

                Environment.Exit(CLI.ExitCode);
            }

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
                case "-log":
                    if (_userService.CurrentUser == null)
                    {
                        LogIn();
                    }
                    else
                    {
                        LogOut();
                    }
                    return false;
                case "-etlmv":
                    bool shouldExitFromToDoMenu = false;
                    while (!shouldExitFromToDoMenu)
                    {
                        shouldExitFromToDoMenu = ToDoListManagementView();
                    }
                    return false;
                case "-etmv":
                    Console.WriteLine("Write the Id of ToDo List you want to enter:");
                    bool isParsed = int.TryParse(Console.ReadLine(), out int toDoId);
                    if (!isParsed || !_toDoService.IsToDoWithIdContained(toDoId))
                    {
                        Console.WriteLine("There is no ToDo List with such Id.");
                        return false;
                    }

                    bool shouldExitFromTaskMenu = false;
                    while (!shouldExitFromTaskMenu)
                    {
                        shouldExitFromTaskMenu = TaskManagementView(toDoId);
                    }
                    return false;
                case "-cu":
                    CreateUser();
                    return false;
                case "-lau":
                    ListAllUsers();
                    return false;
                case "-eu":
                    EditUserById();
                    return false;
                case "-du":
                    Console.WriteLine("Write the Id of the user you want to delete:");
                    bool isParsedUserId = int.TryParse(Console.ReadLine(), out int userToDeleteId);
                    if (isParsedUserId)
                    {
                        DeleteUserById(userToDeleteId);
                    }
                    Console.WriteLine("Invalid Id.");
                    return false;
                case "-c":
                    Console.Clear();
                    return false;
                case "-e":
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
                Console.WriteLine("LogIn (-log)");
            }
            else
            {
                RenderLoggedUserUsername();
                Console.WriteLine("LogOut (-log)");
                Console.WriteLine("Enter ToDo List Management View (-etlmv)");
                Console.WriteLine("Enter Task Management View (-etmv)");

                if (_userService.CurrentUser.IsAdmin)
                {
                    Console.WriteLine("Create user (-cu)");
                    Console.WriteLine("List all users (-lau)");
                    Console.WriteLine("Edit user (-eu)");
                    Console.WriteLine("Delete user (-du)");
                }
            }

            Console.WriteLine("Clear console (-c)");
            Console.WriteLine("Exit the program (-e)");
        }

        private static bool ToDoListManagementView()
        {
            RenderToDoListManagementView();

            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "-ctl":
                    CreateToDoList();
                    return false;
                case "-etl":
                    EditToDoListById();
                    return false;
                case "-latl":
                    ListAllToDoLists();
                    return false;
                case "-dtl":
                    Console.WriteLine("Write the Id of the ToDo list you want to delete:");
                    bool isParsedToDoId = int.TryParse(Console.ReadLine(), out int toDoListToDeleteId);
                    if (isParsedToDoId)
                    {
                        DeleteToDoListById(toDoListToDeleteId);
                    }
                    Console.WriteLine("Invalid Id.");
                    return false;
                case "-stl":
                    Console.WriteLine("Write the Id of the ToDo list you want to share:");
                    int toDoListToShareId = int.Parse(Console.ReadLine());
                    ShareToDoList(toDoListToShareId);
                    return false;
                case "-c":
                    Console.Clear();
                    return false;
                case "-e":
                    return true;
                default:
                    Console.WriteLine("Unknown Command");
                    return false;
            }
        }

        private static void RenderToDoListManagementView()
        {
            Console.WriteLine("--------ToDo List Management View--------");
            RenderLoggedUserUsername();
            Console.WriteLine("Create ToDo list (-ctl)");
            Console.WriteLine("Edit ToDo list (-etl)");
            Console.WriteLine("Delete ToDo list (-dtl)");
            Console.WriteLine("List all ToDo lists (-latl)");
            Console.WriteLine("Share ToDo list: (-stl)");
            Console.WriteLine("Clear console (-c)");
            Console.WriteLine("Exit ToDo List Management View (-e)");
        }

        private static bool TaskManagementView(int toDoId)
        {
            ToDo toDo = _toDoService.GetToDoById(toDoId);

            RenderTaskManagementView(toDo);

            string userInput = Console.ReadLine();
            switch (userInput)
            {
                case "-ct":
                    CreateTask(toDo);
                    return false;
                case "-et":
                    EditTaskById(toDo);
                    return false;
                case "-lat":
                    ListAllTasks(toDo);
                    return false;
                case "-dt":
                        DeleteTaskById(toDo);
                    return false;
                case "-aut":
                    AssignUsersToTask(toDo);
                    return false;
                case "-cmt":
                    CompleteTaksById(toDo);
                    return false;
                case "-c":
                    Console.Clear();
                    return false;
                case "-e":
                    return true;
                default:
                    Console.WriteLine("Unknown Command");
                    return false;
            }
        }

        private static void RenderTaskManagementView(ToDo toDo)
        {
            Console.WriteLine("--------Task Management View--------");
            Console.WriteLine($"ToDo List Id: {toDo.Id} | Title: {toDo.Title}");
            RenderLoggedUserUsername();
            Console.WriteLine("Create Task (-ct)");
            Console.WriteLine("Edit Task (-et)");
            Console.WriteLine("Delete Task (-dt)");
            Console.WriteLine("List all Tasks (-lat)");
            Console.WriteLine("Assign users to Task (-aut)");
            Console.WriteLine("Complete Task (-cmt)");
            Console.WriteLine("Clear console (-c)");
            Console.WriteLine("Exit Task Management View (-e)");
        }

        private static void CompleteTaksById(ToDo toDo)
        {
            Task task = GetTaskById(toDo);
            if (task != null && toDo.SharedWithUsersIds.Contains(_userService.CurrentUser.Id))
            {
                task.IsComplete = true;
                task.LastChangeDate = DateTime.Now;
                task.LastChangeUserId = _userService.CurrentUser.Id;
                _toDoService.SaveChanges();
                Console.WriteLine("You have successfully completed this task.");
            }
        }

        private static List<int> EnterUsersIds()
        {
            Console.WriteLine("Enter users Ids (example: 1 2 3 4):");
            string[] usersIdString = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).Distinct().ToArray();

            List<int> usersId = new List<int>();
            foreach (var userIdString in usersIdString)
            {
                bool isUserIdParsed = int.TryParse(userIdString, out int userId);
                if (isUserIdParsed)
                {
                    usersId.Add(userId);
                }
                else
                {
                    Console.WriteLine($"The format of this Id: '{userIdString}' is invalid.");
                }
            }

            return usersId;
        }
        private static void AssignUsersToTask(ToDo toDo)
        {
            Task task = GetTaskById(toDo);
            if (task == null)
            {
                return;
            }

            List<int> usersId = EnterUsersIds();

            if (usersId.Any())
            {
                HashSet<int> allUsersId = _userService.GetAllUsersId();
                int counter = 0;

                foreach (var userId in usersId)
                {
                    if (allUsersId.Contains(userId) && toDo.SharedWithUsersIds.Contains(userId))
                    {
                        task.UsersAssignedToTask.Add(userId);
                        Console.WriteLine($"User with Id: {userId} added to task.");
                        counter++;
                    }
                    else
                    {
                        Console.WriteLine($"There is no user with Id: '{userId}' or the ToDo list is not shared with this user.");
                    }
                }

                Console.WriteLine($"You have assigned {counter} users to this task.");
                _toDoService.SaveChanges();
                return;
            }

            Console.WriteLine("No users assigned to this task.");
        }

        private static void ListAllTasks(ToDo toDo)
        {
            foreach (var task in toDo.Tasks)
            {
                Console.WriteLine(task);
            }
        }

        private static void DeleteTaskById(ToDo toDo)
        {
            Task taskToDelete = GetTaskById(toDo);

            if (taskToDelete == null)
            {
                return;
            }

            if (toDo.CreatorId == _userService.CurrentUser.Id || toDo.SharedWithUsersIds.Contains(_userService.CurrentUser.Id))
            {
                toDo.Tasks.Remove(taskToDelete);
                _toDoService.SaveChanges();

                Console.WriteLine($"You successfully deleted Task with id: {taskToDelete.Id}.");
                return;
            }

            Console.WriteLine("You can't delete this Task.");
        }

        private static void EditTaskById(ToDo toDo)
        {
            Task taskToEdit = GetTaskById(toDo);

            if (taskToEdit == null)
            {
                return;
            }

            Console.WriteLine("Change Id (-i)");
            Console.WriteLine("Change Title (-t)");
            Console.WriteLine("Change Description (-d)");
            Console.WriteLine("Complete Task (-c)");
            string userCoise = Console.ReadLine();
            bool hasChange = false;

            switch (userCoise)
            {
                case "-i":
                    Console.WriteLine("Set new Id:");
                    bool isParsed = int.TryParse(Console.ReadLine(), out int newId);

                    if (!isParsed || toDo.Tasks.Any(t => t.Id == newId))
                    {
                        Console.WriteLine("Invalid Id.");
                    }
                    else
                    {
                        taskToEdit.Id = newId;
                        hasChange = true;
                    }
                    break;
                case "-t":
                    Console.WriteLine("Set new Title:");
                    taskToEdit.Title = Console.ReadLine();
                    hasChange = true;
                    break;
                case "-d":
                    Console.WriteLine("Set new Description:");
                    taskToEdit.Description = Console.ReadLine();
                    hasChange = true;
                    break;
                case "-c":
                    Console.WriteLine("You have successfully completed this task.");
                    taskToEdit.IsComplete = true;
                    hasChange = true;
                    break;
                default:
                    Console.WriteLine("Unknown Command");
                    break;
            }

            if (hasChange)
            {
                taskToEdit.LastChangeDate = DateTime.Now;
                taskToEdit.LastChangeUserId = _userService.CurrentUser.Id;
                _toDoService.SaveChanges();
                Console.WriteLine("Your change has been applied successfully.");
            }
        }

        private static Task GetTaskById(ToDo toDo)
        {
            Console.WriteLine("Enter Task Id:");
            bool isTaskIdParsed = int.TryParse(Console.ReadLine(), out int taskId);
            if (!isTaskIdParsed)
            {
                Console.WriteLine("Invalid Id format.");
                return null;
            }
            Task taskToEdit = toDo.Tasks.FirstOrDefault(t => t.Id == taskId);
            if (taskToEdit == null)
            {
                Console.WriteLine("There is no Task with such Id.");
            }

            return taskToEdit;
        }

        private static void CreateTask(ToDo toDo)
        {
            Console.WriteLine("Title:");
            string title = Console.ReadLine();

            Console.WriteLine("Description:");
            string description = Console.ReadLine();

            Task task = _taskService.CreateTask(title, description, toDo, _userService.CurrentUser);
            toDo.Tasks.Add(task);
            _toDoService.SaveChanges();

            Console.WriteLine($"Task with title '{title}' added.");
        }

        private static void RenderLoggedUserUsername()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"You are logged in as: {_userService.CurrentUser.UserName}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void ShareToDoList(int toDoId)
        {
            ToDo toDo = _toDoService.GetToDoById(toDoId);
            if (toDo == null)
            {
                Console.WriteLine("There is no ToDo List with such Id.");
                return;
            }

            if (_userService.CurrentUser.Id != toDo.CreatorId)
            {
                Console.WriteLine("You are not allowed to share ToDo List, which is not created by you.");
                return;
            }

            List<int> usersIds = EnterUsersIds();

            foreach (var userId in usersIds)
            {
                User user = _userService.GetUserById(userId);
                if (user == null)
                {
                    Console.WriteLine("There is no User with such Id.");
                    continue;
                }

                _toDoService.ShareToDoList(toDoId, userId);
                _toDoService.SaveChanges();
                Console.WriteLine($"ToDo List with Id '{toDoId}' successfully shared with User with Id '{userId}'.");
            }
        }

        private static void ListAllToDoLists()
        {
            var toDoLists = _toDoService.ListAllUserToDoLists(_userService.CurrentUser.Id);

            foreach (var toDoList in toDoLists)
            {
                Console.WriteLine(toDoList);
            }
        }

        private static void DeleteToDoListById(int toDoId)
        {
            ToDo toDoListToDelete = _toDoService.GetToDoById(toDoId);

            if (toDoListToDelete == null)
            {
                Console.WriteLine("There is no ToDo list with such Id.");
                return;
            }
            if (toDoListToDelete.CreatorId == _userService.CurrentUser.Id)
            {
                _toDoService.DeleteToDoById(toDoId);

                Console.WriteLine($"You successfully deleted ToDo List with id: {toDoId}.");
                return;
            }
            if (toDoListToDelete.SharedWithUsersIds.Contains(_userService.CurrentUser.Id))
            {
                toDoListToDelete.SharedWithUsersIds.Remove(_userService.CurrentUser.Id);
                _toDoService.SaveChanges();

                Console.WriteLine($"ToDo List with id: {toDoId} is no longer shared with you.");
                return;
            }

            Console.WriteLine("You can't take deletion action with ToDo list which is not created or shared with you.");
        }

        private static void EditToDoListById()
        {
            Console.WriteLine("Write the Id of the ToDo list you want to edit:");
            int toDoListToEditId = int.Parse(Console.ReadLine());

            ToDo toDoListToEdit = _toDoService.GetToDoById(toDoListToEditId);

            if (toDoListToEdit == null)
            {
                Console.WriteLine("There is no ToDo list with such Id.");
                return;
            }
            if (toDoListToEdit.CreatorId != _userService.CurrentUser.Id)
            {
                Console.WriteLine("You can't edit ToDo list which is not created by you.");
                return;
            }

            Console.WriteLine("Change Id (-i)");
            Console.WriteLine("Change Title (-t)");
            string userCoise = Console.ReadLine();
            bool hasChange = false;

            switch (userCoise)
            {
                case "-t":
                    Console.WriteLine("Set new Title:");
                    toDoListToEdit.Title = Console.ReadLine();
                    hasChange = true;
                    break;
                case "-i":
                    Console.WriteLine("Set new Id:");
                    bool isParsed = int.TryParse(Console.ReadLine(), out int newId);

                    if (!isParsed || _toDoService.IsToDoWithIdContained(newId))
                    {
                        Console.WriteLine("Invalid Id.");
                    }
                    else
                    {
                        toDoListToEdit.Id = newId;
                        hasChange = true;
                    }
                    break;
                default:
                    Console.WriteLine("Unknown Command");
                    break;
            }

            if (hasChange)
            {
                toDoListToEdit.LastChangeDate = DateTime.Now;
                toDoListToEdit.LastChangeUserId = _userService.CurrentUser.Id;
                _toDoService.SaveChanges();
                Console.WriteLine("Your change has been applied successfully.");
            }
        }

        private static void CreateToDoList()
        {
            Console.WriteLine("Title:");
            string title = Console.ReadLine();
            bool isSuccessful = _toDoService.CreateToDoList(title, _userService.CurrentUser);

            if (isSuccessful)
            {
                Console.WriteLine($"ToDo List with title '{title}' added.");
            }
            else
            {
                Console.WriteLine($"ToDo List with title '{title}' already exists.");
                CreateToDoList();
            }

        }

        private static void DeleteUserById(int userId)
        {
            bool isDeleted = _userService.DeleteUserById(userId);

            if (isDeleted)
            {
                Console.WriteLine($"You successfully deleted user with id: {userId}.");
            }
            else
            {
                Console.WriteLine("There is no user with such Id.");
            }
        }

        private static void EditUserById()
        {
            Console.WriteLine("Write the Id of the user you want to edit:");
            int userToEditId = int.Parse(Console.ReadLine());

            User userToEdit = _userService.GetUserById(userToEditId);

            if (userToEdit == null)
            {
                Console.WriteLine("There is no user with such Id.");
                return;
            }

            Console.WriteLine("Change User name (-u)");
            Console.WriteLine("Change Password (-p)");
            Console.WriteLine("Change First Name (-fn)");
            Console.WriteLine("Change Last Name (-ln)");
            string userCoise = Console.ReadLine();

            bool hasChange = false;
            switch (userCoise)
            {
                case "-u":
                    Console.WriteLine("Set new User name:");
                    userToEdit.UserName = Console.ReadLine();
                    hasChange = true;
                    break;
                case "-p":
                    Console.WriteLine("Set new Password:");
                    userToEdit.Password = Console.ReadLine();
                    hasChange = true;
                    break;
                case "-fn":
                    Console.WriteLine("Set new First Name:");
                    userToEdit.FirstName = Console.ReadLine();
                    hasChange = true;
                    break;
                case "-ln":
                    Console.WriteLine("Set new Last Name:");
                    userToEdit.LastName = Console.ReadLine();
                    hasChange = true;
                    break;
                default:
                    Console.WriteLine("Unknown Command");
                    break;
            }

            if (hasChange)
            {
                userToEdit.LastChangeDate = DateTime.Now;
                userToEdit.LastChangeUserId = _userService.CurrentUser.Id;
                _userService.SaveChanges();
                Console.WriteLine("Your change has been applied successfully.");
            }
        }

        private static void ListAllUsers()
        {
            foreach (var user in _userService.ListAllUsers())
            {
                Console.WriteLine(user);
            }
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
                Console.WriteLine("Login failed! Wrong user name or password!");
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

            Console.WriteLine("User name:");
            string userName = Console.ReadLine();
            Console.WriteLine("Password:");
            string password = Console.ReadLine();
            Console.WriteLine("User First Name:");
            string firstName = Console.ReadLine();
            Console.WriteLine("User Last Name:");
            string lastName = Console.ReadLine();
            Console.WriteLine("Give admin rights (true/false):");
            bool isAdmin = Console.ReadLine() == "true" ? true : false;

            bool isSuccessful = _userService.CreateUser(userName, password, firstName, lastName, isAdmin, _userService.CurrentUser);
            if (isSuccessful)
            {
                Console.WriteLine($"User with name '{userName}' added.");
            }
            else
            {
                Console.WriteLine($"User with name '{userName}' already exists.");
                CreateUser();
            }
        }
    }
}
