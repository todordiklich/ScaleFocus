using CommandLine;

namespace SimpleToDoApp.CLITool.Options.UserOptions
{
    [Verb("createuser", HelpText = "Create a new user.")]
    public class CreateUserOption
    {
        [Option('u', "username", Required = true, HelpText = "Enter User name.")]
        public string UserName { get; set; }

        [Option('p', "password", Required = true, HelpText = "Enter Password.")]
        public string Password { get; set; }

        [Option('n', "usernameToCreate", Required = true, HelpText = "Enter user name to the user you want to create.")]
        public string UserNameToCreate { get; set; }

        [Option('w', "passwordToCreate", Required = true, HelpText = "Enter password to the user you want to create.")]
        public string PasswordToCreate { get; set; }

        [Option('f', "firstName", Required = true, HelpText = "Enter user first name to the user you want to create.")]
        public string FirstName { get; set; }

        [Option('l', "lastName", Required = true, HelpText = "Enter user last name to the user you want to create.")]
        public string LastName { get; set; }

        [Option('a', "isAdmin", Default = false, HelpText = "Enter admin rights to the user you want to create.")]
        public bool IsAdmin { get; set; }

    }
}