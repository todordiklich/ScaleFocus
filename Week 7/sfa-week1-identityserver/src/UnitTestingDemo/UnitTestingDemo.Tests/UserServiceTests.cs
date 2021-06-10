using Moq;
using Xunit;

namespace UnitTestingDemo.Tests
{
    public class UserServiceTests
    {
        [Fact]
        public void RegisterUser_ValidUser_CallEmailService()
        {
            // arrange
            var emailServiceMock = new Mock<IEmailService>();
            var sut = new UserService(emailServiceMock.Object);
            var userInfo = new UserInfo { Email = "user@email.test" };

            // act
            sut.RegisterUser(userInfo);

            // assert
            emailServiceMock.Verify(x => x.Send("user@email.test", It.IsAny<string>()), Times.Once);
        }
    }
}
