using Xunit;

namespace UnitTestingDemo.Tests
{
    public class UserServiceTests
    {
        [Fact]
        public void RegisterUser_ValidUser_CallEmailService()
        {
            // arrange
            var emailService = new EmailServiceFake();
            var sut = new UserService(emailService);
            var userInfo = new UserInfo { Email = "user@email.test" };

            // act
            sut.RegisterUser(userInfo);

            // assert
            Assert.Equal(1, emailService.SendCallTimes);
            Assert.Equal("user@email.test", emailService.SendToValue);
        }


        public class EmailServiceFake : IEmailService
        {
            public int SendCallTimes { get; set; }
            public string SendToValue { get; set; }

            public void Send(string to, string body)
            {
                SendCallTimes++;
                SendToValue = to;
            }
        }
    }
}
