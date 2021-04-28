using CommandLine;

namespace SimpleToDoApp.CLITool.Options.TaskOptions
{
    [Verb("createtask", HelpText = "Create a new task.")]
    public class CreateTaskOpiton
    {
        [Option('u', "username", Required = true, HelpText = "Enter User name.")]
        public string UserName { get; set; }

        [Option('p', "password", Required = true, HelpText = "Enter Password.")]
        public string Password { get; set; }

        [Option('i', "ToDoId", Required = true, HelpText = "Enter Id of the ToDo List where you want to create a new task.")]
        public int ToDoId { get; set; }

        [Option('t', "title", Required = true, HelpText = "Enter title of the task.")]
        public string Title { get; set; }

        [Option('d', "description", Required = true, HelpText = "Enter description of the task.")]
        public string Description { get; set; }
    }
}
