using CommandLine;

namespace SimpleToDoApp.CLITool.Options.TaskOptions
{
    [Verb("edittask", HelpText = "Edit task.")]
    public class EditTaskOption
    {
        [Option('u', "username", Required = true, HelpText = "Enter User name.")]
        public string UserName { get; set; }

        [Option('p', "password", Required = true, HelpText = "Enter Password.")]
        public string Password { get; set; }

        [Option('i', "ToDoId", Required = true, HelpText = "Enter Id of the ToDo List where you want to edit task from.")]
        public int ToDoId { get; set; }

        [Option('t', "taskId", Required = true, HelpText = "Enter Id of the task you want to edit.")]
        public int TaskId { get; set; }

        [Option('n', "taskTitile", HelpText = "Enter new task title.")]
        public string TaskTitile { get; set; }

        [Option('d', "taskDescription", HelpText = "Enter new task description.")]
        public string TaskDescription { get; set; }

        [Option('c', "completeTask", HelpText = "Complete the task.")]
        public bool? IsComplete { get; set; }
    }
}
