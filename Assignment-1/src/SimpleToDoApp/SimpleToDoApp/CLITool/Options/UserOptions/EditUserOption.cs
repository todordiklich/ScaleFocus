using CommandLine;

namespace SimpleToDoApp.CLITool.Options.UserOptions
{
    [Verb("edituser", HelpText = "Edit user by Id.")]
    public class EditUserOption
    {
        [Option('u', "username", Required = true, HelpText = "Enter User name.")]
        public string UserName { get; set; }

        [Option('p', "password", Required = true, HelpText = "Enter Password.")]
        public string Password { get; set; }

        [Option('i', "Id", Required = true, HelpText = "Enter Id of the user you want to edit.")]
        public int UserToEditId { get; set; }

        [Option('n', "newUsername", HelpText = "Enter new user name.")]
        public string NewUserName { get; set; }

        [Option('w', "newPasswor", HelpText = "Enter new password.")]
        public string NewPassword { get; set; }

        [Option('f', "newFirstName", HelpText = "Enter new first name.")]
        public string NewFirstName { get; set; }

        [Option('l', "newLastName", HelpText = "Enter new last name.")]
        public string NewLastName { get; set; }
    }
}