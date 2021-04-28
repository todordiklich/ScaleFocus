using CommandLine;

namespace SimpleToDoApp.CLITool.Options.ToDoListOptions
{
    [Verb("createtodo", HelpText = "Create new ToDo Lists.")]
    public class CreateToDoListOption
    {
        [Option('u', "username", Required = true, HelpText = "Enter User name.")]
        public string UserName { get; set; }

        [Option('p', "password", Required = true, HelpText = "Enter Password.")]
        public string Password { get; set; }

        [Option('t', "title", Required = true, HelpText = "Enter Title.")]
        public string Title { get; set; }
    }
}
