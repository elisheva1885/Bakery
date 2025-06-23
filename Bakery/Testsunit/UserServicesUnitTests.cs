using AutoMapper;
using Entities;
using Microsoft.Extensions.Logging;
using Moq;
using Repositories;
using Services;
using System.Threading.Tasks;
using Xunit;
using DTOs;
namespace TestsProject
{
    public class UserServicesUnitTests
    {
        private readonly Mock<IUsersData> mockUsersData;
        private readonly Mock<IMapper> mockMapper;
        private readonly Mock<ILogger<UserServices>> mockLogger;
        private readonly UserServices userServices;

        public UserServicesUnitTests()
        {
            mockUsersData = new Mock<IUsersData>();
            mockMapper = new Mock<IMapper>();
            mockLogger = new Mock<ILogger<UserServices>>();
            userServices = new UserServices(mockUsersData.Object, mockMapper.Object, mockLogger.Object);

        }
        [Theory]
        [InlineData("123", 0)]
        [InlineData("password", 1)]
        [InlineData("Pass123!", 3)]
        public void ValidatePasswordStrong_ShouldReturnCorrectScore(string password, int expectedMinScore)
        {
            int score = userServices.validatepasswordStrong(password);
            Assert.True(score >= 0 && score <= 4);
        }

        [Fact]
        public async Task Login_ShouldLogInformation_WhenLoginSuccessful()
        {
            // Arrange
            var loginDto = new LoginUserDto
            (
                "test",
                 "1234"
            );
            var user = new User { Username = "test", Password = "1234" };
            var userDto = new UserDto (1,"test","test","test" );
            mockMapper.Setup(m => m.Map<User>(loginDto)).Returns(user);
            mockUsersData.Setup(m => m.Login(user)).ReturnsAsync(user);
            mockMapper.Setup(m => m.Map<UserDto>(user)).Returns(userDto);

            // Act
            var result = await userServices.login(loginDto);

            // Assert
            Assert.Equal("test", result.Username);

            mockLogger.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("User logged in")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }
    }
}

