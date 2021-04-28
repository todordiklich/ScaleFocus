using CommandLine;

namespace SimpleToDoApp.CLITool.Options.ToDoListOptions
{
    [Verb("listtodos", HelpText = "List all of my ToDo Lists.")]
    public class ListToDoListsOption
    {
        [Option('u', "username", Required = true, HelpText = "Enter User name.")]
        public string UserName { get; set; }

        [Option('p', "password", Required = true, HelpText = "Enter Password.")]
        public string Password { get; set; }
    }
}
