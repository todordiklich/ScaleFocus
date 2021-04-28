using CommandLine;
using System.Collections.Generic;

namespace SimpleToDoApp.CLITool.Options.ToDoListOptions
{
    [Verb("sharetodo", HelpText = "Share ToDo Lists with other users.")]
    public class ShareToDoListOption
    {
        [Option('u', "username", Required = true, HelpText = "Enter User name.")]
        public string UserName { get; set; }

        [Option('p', "password", Required = true, HelpText = "Enter Password.")]
        public string Password { get; set; }

        [Option('i', "todoId", Required = true, HelpText = "Enter Id of the ToDo List you want to share.")]
        public int ToDoId { get; set; }

        [Option('s', "usersIds", Required = true, HelpText = "Enter Ids of the users you want to share ToDo List with.")]
        public IEnumerable<int> UsersIds { get; set; }
    }
}
