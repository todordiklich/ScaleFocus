using System;
using CommandLine;
using System.Collections.Generic;

using SimpleToDoApp.Models;
using SimpleToDoApp.Services;
using SimpleToDoApp.CLITool.Options.TaskOptions;
using SimpleToDoApp.CLITool.Options.ToDoListOptions;
using SimpleToDoApp.CLITool.Options.UserOptions;

namespace SimpleToDoApp.CLITool
{
    public class CLI
    {
        private static UserService _userService = new UserService();
        private static ToDoService _toDoService = new ToDoService();
        private static TaskService _taskService = new TaskService();

        public CLI(string[] args)
        {
            ParseArguments(args);
        }

        public int ExitCode { get; set; }

        private void ParseArguments(string[] args)
        {
            ExitCode = Parser
                .Default
                .ParseArguments<CreateTaskOpiton, DeleteTaskOption, EditTaskOption, AssignTaskOption, CompleteTaskOption, CreateUserOption, DeleteUserOption, EditUserOption, ListToDoListsOption, CreateToDoListOption, DeleteToDoListOption, EditToDoListOption, ShareToDoListOption>(args)
                .MapResult(
                    (CreateTaskOpiton opts) => CreateTask(opts),
                    (DeleteTaskOption opts) => DeleteTask(opts),
                    (EditTaskOption opts) => EditTask(opts),
                    (AssignTaskOption opts) => AssignTask(opts),
                    (CompleteTaskOption opts) => CompleteTask(opts),
                    (CreateUserOption opts) => CreateUser(opts),
                    (DeleteUserOption opts) => DeleteUser(opts),
                    (EditUserOption opts) => EditUser(opts),
                    (ListToDoListsOption opts) => ListToDoLists(opts),
                    (CreateToDoListOption opts) => CreateToDoList(opts),
                    (DeleteToDoListOption opts) => DeleteToDoList(opts),
                    (EditToDoListOption opts) => EditToDoList(opts),
                    (ShareToDoListOption opts) => ShareToDoList(opts),
                    errs => 1);
        }

        private int ShareToDoList(ShareToDoListOption opts)
        {
            bool isLoggedIn = IsLoggedIn(opts.UserName, opts.Password);
            if (!isLoggedIn)
            {
                return -1;
            }

            ToDo toDo = _toDoService.GetToDoById(opts.ToDoId);
            if (toDo == null)
            {
                Console.WriteLine("There is no ToDo list with such Id.");
                return -1;
            }

            if (_userService.CurrentUser.Id != toDo.CreatorId)
            {
                Console.WriteLine("You are not allowed to share ToDo List, which is not created by you.");
                return -1;
            }

            foreach (var userId in opts.UsersIds)
            {
                User user = _userService.GetUserById(userId);
                if (user == null)
                {
                    Console.WriteLine("There is no User with such Id.");
                    continue;
                }

                _toDoService.ShareToDoList(toDo.Id, userId);
                _toDoService.SaveChanges();
                Console.WriteLine($"ToDo List with Id '{toDo.Id}' successfully shared with User with Id '{userId}'.");
            }

            return 1;
        }

        private int EditToDoList(EditToDoListOption opts)
        {
            bool isLoggedIn = IsLoggedIn(opts.UserName, opts.Password);
            if (!isLoggedIn)
            {
                return -1;
            }

            ToDo toDo = _toDoService.GetToDoById(opts.ToDoId);
            if (toDo == null)
            {
                Console.WriteLine("There is no ToDo list with such Id.");
                return -1;
            }

            bool hasChange = false;
            if (opts.NewToDoId != null && _toDoService.IsToDoWithIdContained(opts.NewToDoId.Value))
            {
                toDo.Id = opts.NewToDoId.Value;
                hasChange = true;
            }
            if (opts.NewToDoTitle != null)
            {
                toDo.Title = opts.NewToDoTitle;
                hasChange = true;
            }

            if (hasChange)
            {
                toDo.LastChangeDate = DateTime.Now;
                toDo.LastChangeUserId = _userService.CurrentUser.Id;
                _toDoService.SaveChanges();
                Console.WriteLine("Your change has been applied successfully.");
            }

            return 1;
        }

        private int DeleteToDoList(DeleteToDoListOption opts)
        {
            bool isLoggedIn = IsLoggedIn(opts.UserName, opts.Password);
            if (!isLoggedIn)
            {
                return -1;
            }
            ToDo toDo = _toDoService.GetToDoById(opts.ToDoId);

            if (toDo == null)
            {
                Console.WriteLine("There is no ToDo list with such Id.");
                return -1;
            }
            if (toDo.CreatorId == _userService.CurrentUser.Id)
            {
                _toDoService.DeleteToDoById(opts.ToDoId);

                Console.WriteLine($"You successfully deleted ToDo List with id: {opts.ToDoId}.");
                return 1;
            }
            if (toDo.SharedWithUsersIds.Contains(_userService.CurrentUser.Id))
            {
                toDo.SharedWithUsersIds.Remove(_userService.CurrentUser.Id);
                _toDoService.SaveChanges();

                Console.WriteLine($"ToDo List with id: {opts.ToDoId} is no longer shared with you.");
                return 1;
            }

            Console.WriteLine("You can't take deletion action with ToDo list which is not created or shared with you.");

            return 1;
        }

        private int CreateToDoList(CreateToDoListOption opts)
        {
            bool isLoggedIn = IsLoggedIn(opts.UserName, opts.Password);
            if (!isLoggedIn)
            {
                return -1;
            }

            bool isSuccessful = _toDoService.CreateToDoList(opts.Title, _userService.CurrentUser);
            if (!isSuccessful)
            {
                Console.WriteLine($"ToDo List with title '{opts.Title}' already exists.");
                return -1;
            }
                Console.WriteLine($"ToDo List with title '{opts.Title}' added.");

            return 1;
        }

        private int ListToDoLists(ListToDoListsOption opts)
        {
            bool isLoggedIn = IsLoggedIn(opts.UserName, opts.Password);
            if (!isLoggedIn)
            {
                return -1;
            }

            var toDoLists = _toDoService.ListAllUserToDoLists(_userService.CurrentUser.Id);

            foreach (var toDoList in toDoLists)
            {
                Console.WriteLine(toDoList);
            }

            return 1;
        }

        private int EditUser(EditUserOption opts)
        {
            bool isAdmin = IsLoggedAsAdmin(opts.UserName, opts.Password);
            if (!isAdmin)
            {
                return -1;
            }

            User user = _userService.GetUserById(opts.UserToEditId);
            if (user == null)
            {
                Console.WriteLine("There is no user with such Id.");
                return -1;
            }

            bool hasChange = false;
            if (opts.NewUserName != null)
            {
                user.UserName = opts.NewUserName;
                hasChange = true;
            }
            if (opts.NewPassword != null)
            {
                user.Password = opts.NewPassword;
                hasChange = true;
            }
            if (opts.NewFirstName != null)
            {
                user.FirstName = opts.NewFirstName;
                hasChange = true;
            }
            if (opts.NewLastName != null)
            {
                user.LastName = opts.NewLastName;
                hasChange = true;
            }

            if (hasChange)
            {
                user.LastChangeDate = DateTime.Now;
                user.LastChangeUserId = _userService.CurrentUser.Id;
                _userService.SaveChanges();
                Console.WriteLine("Your change has been applied successfully.");
            }

            return 1;
        }

        private int DeleteUser(DeleteUserOption opts)
        {
            bool isAdmin = IsLoggedAsAdmin(opts.UserName, opts.Password);
            if (!isAdmin)
            {
                return -1;
            }

            bool isDeleted = _userService.DeleteUserById(opts.UserToDeleteId);

            if (isDeleted)
            {
                Console.WriteLine($"You successfully deleted user with id: {opts.UserToDeleteId}.");
            }
            else
            {
                Console.WriteLine("There is no user with such Id.");
            }

            return 1;
        }

        private int CreateUser(CreateUserOption opts)
        {
            bool isLoggedAsAdmin = IsLoggedAsAdmin(opts.UserName, opts.Password);
            if (!isLoggedAsAdmin)
            {
                return -1;
            }

            bool isSuccessful = _userService.CreateUser(opts.UserNameToCreate, opts.PasswordToCreate, opts.FirstName, opts.LastName, opts.IsAdmin, _userService.CurrentUser);

            if (isSuccessful)
            {
                Console.WriteLine($"User with name '{opts.UserNameToCreate}' added.");
            }
            else
            {
                Console.WriteLine($"User with name '{opts.UserNameToCreate}' already exists.");
            }

            return 1;
        }

        private int CompleteTask(CompleteTaskOption opts)
        {
            bool isValid = ValidateInputTaskArguments(opts.UserName, opts.Password, opts.ToDoId, opts.TaskId);

            if (!isValid)
            {
                return -1;
            }

            ToDo toDo = GetToDoListById(opts.ToDoId);
            Task task = _taskService.GetTaskById(toDo, opts.TaskId);
            if (task.IsComplete)
            {
                Console.WriteLine("This task is already completed.");
                return 1;
            }

            task.IsComplete = true;
            task.LastChangeDate = DateTime.Now;
            task.LastChangeUserId = _userService.CurrentUser.Id;
            _toDoService.SaveChanges();
            Console.WriteLine("You have successfully completed this task.");

            return 1;
        }

        private int AssignTask(AssignTaskOption opts)
        {
            bool isValid = ValidateInputTaskArguments(opts.UserName, opts.Password, opts.ToDoId, opts.TaskId);

            if (!isValid)
            {
                return -1;
            }

            ToDo toDo = GetToDoListById(opts.ToDoId);
            Task task = _taskService.GetTaskById(toDo, opts.TaskId);

            HashSet<int> allUsersId = _userService.GetAllUsersId();
            int counter = 0;

            foreach (var userId in opts.UsersIds)
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

            return 1;
        }

        private int EditTask(EditTaskOption opts)
        {
            bool isValid = ValidateInputTaskArguments(opts.UserName, opts.Password, opts.ToDoId, opts.TaskId);

            if (!isValid)
            {
                return -1;
            }

            ToDo toDo = GetToDoListById(opts.ToDoId);
            Task task = _taskService.GetTaskById(toDo, opts.TaskId);

            bool hasChange = false;

            if (opts.TaskTitile != null)
            {
                task.Title = opts.TaskTitile;
                hasChange = true;
            }
            if (opts.TaskDescription != null)
            {
                task.Description = opts.TaskDescription;
                hasChange = true;
            }
            if (opts.IsComplete != null)
            {
                task.IsComplete = opts.IsComplete.Value;
                hasChange = true;
            }

            if (hasChange)
            {
                task.LastChangeDate = DateTime.Now;
                task.LastChangeUserId = _userService.CurrentUser.Id;
                _toDoService.SaveChanges();
                Console.WriteLine("Your change has been applied successfully.");
            }

            return 1;
        }

        private int DeleteTask(DeleteTaskOption opts)
        {
            bool isValid = ValidateInputTaskArguments(opts.UserName, opts.Password, opts.ToDoId, opts.TaskId);

            if (!isValid)
            {
                return -1;
            }

            ToDo toDo = GetToDoListById(opts.ToDoId);
            Task task = _taskService.GetTaskById(toDo, opts.TaskId);

            toDo.Tasks.Remove(task);
            _toDoService.SaveChanges();

            return 1;
        }

        private int CreateTask(CreateTaskOpiton opts)
        {
            bool isLoggedIn = IsLoggedIn(opts.UserName, opts.Password);
            if (!isLoggedIn)
            {
                return -1;
            }

            ToDo toDo = GetToDoListById(opts.ToDoId);
            if (toDo == null)
            {
                return -1;
            }

            Task task = _taskService.CreateTask(opts.Title, opts.Description, toDo, _userService.CurrentUser);

            if (task == null)
            {
                Console.WriteLine("Creation failed.");
                return -1;
            }

            toDo.Tasks.Add(task);
            _toDoService.SaveChanges();

            Console.WriteLine($"Task with title '{opts.Title}' added.");

            return 1;
        }

        private ToDo GetToDoListById(int toDoId)
        {
            ToDo toDo = _toDoService.GetToDoById(toDoId);
            if (toDo == null)
            {
                Console.WriteLine("Invalid ToDo List Id.");
                return null;
            }
            if (!toDo.SharedWithUsersIds.Contains(_userService.CurrentUser.Id))
            {
                Console.WriteLine("You are not allowed to access this ToDo List.");
                return null;
            }

            return toDo;
        }

        private bool IsLoggedIn(string username, string password)
        {
            _userService.Login(username, password);

            if (_userService.CurrentUser == null)
            {
                Console.WriteLine("Login failed! Wrong user name or password!");
                return false;
            }

            return true;
        }

        private bool IsLoggedAsAdmin(string username, string password)
        {
            bool isLoggedIn = IsLoggedIn(username, password);
            if (!isLoggedIn)
            {
                return false;
            }

            if (!_userService.CurrentUser.IsAdmin)
            {
                Console.WriteLine("Only users who are admins can create/edit/delete other users!");
                return false;
            }

            return true;
        }

        private bool ValidateInputTaskArguments(string username, string password, int toDoId, int taskId)
        {
            bool isLoggedIn = IsLoggedIn(username, password);
            if (!isLoggedIn)
            {
                return false;
            }

            ToDo toDo = GetToDoListById(toDoId);
            if (toDo == null)
            {
                return false;
            }

            Task task = _taskService.GetTaskById(toDo, taskId);

            if (task == null)
            {
                Console.WriteLine("Invalid task Id.");
                return false;
            }

            return true;
        }
    }
}
