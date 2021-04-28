using CommandLine;
using System.Collections.Generic;

namespace SimpleToDoApp.CLITool.Options.TaskOptions
{
    [Verb("assigntask", HelpText = "Assign task to user/users.")]
    public class AssignTaskOption
    {
        [Option('u', "username", Required = true, HelpText = "Enter User name.")]
        public string UserName { get; set; }

        [Option('p', "password", Required = true, HelpText = "Enter Password.")]
        public string Password { get; set; }

        [Option('i', "ToDoId", Required = true, HelpText = "Enter Id of the ToDo List where you want to delete task from.")]
        public int ToDoId { get; set; }

        [Option('t', "taskId", Required = true, HelpText = "Enter Id of the task you want to delete.")]
        public int TaskId { get; set; }

        [Option('a', "usersIds", Required = true, HelpText = "Enter Ids of the users you want to assign to the task.")]
        public IEnumerable<int> UsersIds { get; set; }
    }
}
