using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestingDemo
{
    public class UserService
    {
        private readonly IEmailService _emailService;

        public UserService(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public void RegisterUser(UserInfo user)
        {
            // Some validation logic
            // Some insertion logic

            _emailService.Send(user.Email, "Welcome");
        }

    }

    public interface IEmailService
    {
        void Send(string to, string body);
    }
}
