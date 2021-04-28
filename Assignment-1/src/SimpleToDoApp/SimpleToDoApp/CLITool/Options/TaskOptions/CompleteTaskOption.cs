using CommandLine;

namespace SimpleToDoApp.CLITool.Options.TaskOptions
{
    [Verb("completetask", HelpText = "Complete task.")]
    public class CompleteTaskOption
    {
        [Option('u', "username", Required = true, HelpText = "Enter User name.")]
        public string UserName { get; set; }

        [Option('p', "password", Required = true, HelpText = "Enter Password.")]
        public string Password { get; set; }

        [Option('i', "ToDoId", Required = true, HelpText = "Enter Id of the ToDo List where you want to delete task from.")]
        public int ToDoId { get; set; }

        [Option('t', "taskId", Required = true, HelpText = "Enter Id of the task you want to delete.")]
        public int TaskId { get; set; }

        [Option('c', "completeTask", Required = true, HelpText = "Complete the task.")]
        public bool IsCompleted { get; set; }
    }
}
