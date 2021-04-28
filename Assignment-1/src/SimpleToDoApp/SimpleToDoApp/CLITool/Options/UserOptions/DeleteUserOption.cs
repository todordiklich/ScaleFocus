using CommandLine;

namespace SimpleToDoApp.CLITool.Options.UserOptions
{
    [Verb("deleteuser", HelpText = "Delete user by Id.")]
    public class DeleteUserOption
    {
        [Option('u', "username", Required = true, HelpText = "Enter User name.")]
        public string UserName { get; set; }

        [Option('p', "password", Required = true, HelpText = "Enter Password.")]
        public string Password { get; set; }

        [Option('i', "Id", Required = true, HelpText = "Enter Id of the user you want to delete.")]
        public int UserToDeleteId { get; set; }
    }
}
