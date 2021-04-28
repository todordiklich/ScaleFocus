using CommandLine;

namespace SimpleToDoApp.CLITool.Options.ToDoListOptions
{
    [Verb("deletetodo", HelpText = "Delete ToDo Lists.")]
    public class DeleteToDoListOption
    {
        [Option('u', "username", Required = true, HelpText = "Enter User name.")]
        public string UserName { get; set; }

        [Option('p', "password", Required = true, HelpText = "Enter Password.")]
        public string Password { get; set; }

        [Option('i', "todoId", Required = true, HelpText = "Enter Id of the ToDo List you want to delete.")]
        public int ToDoId { get; set; }
    }
}
