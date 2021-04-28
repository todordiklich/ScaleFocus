using CommandLine;

namespace SimpleToDoApp.CLITool.Options.ToDoListOptions
{
    [Verb("edittodo", HelpText = "Edit ToDo Lists.")]
    public class EditToDoListOption
    {
        [Option('u', "username", Required = true, HelpText = "Enter User name.")]
        public string UserName { get; set; }

        [Option('p', "password", Required = true, HelpText = "Enter Password.")]
        public string Password { get; set; }

        [Option('i', "todoId", Required = true, HelpText = "Enter Id of the ToDo List you want to edit.")]
        public int ToDoId { get; set; }

        [Option('n', "todoTitile", HelpText = "Enter new ToDo List title.")]
        public string NewToDoTitle { get; set; }

        [Option('d', "todoNewId", HelpText = "Enter new ToDo List Id.")]
        public int? NewToDoId { get; set; }
    }
}
