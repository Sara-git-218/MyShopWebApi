using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using MyShopWebApi.Controllers;
using DTO;
using Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TestProject
{
public class TestUsersController
    {
        //[Fact]
        //public async Task Login_ShouldLogInformation_WhenLoginSucceeds()
        //{
        //    // Arrange
        //    var mockService = new Mock<IUserService>();
        //    var mockLogger = new Mock<ILogger<UsersController>>();

        //    var testUser = new UserDTO { UserName = "testuser" };
        //    var loginInput = new UserLoginDTO { UserName = "testuser", Password = "1234" };

        //    mockService.Setup(x => x.Login(loginInput)).ReturnsAsync(testUser);

        //    var controller = new UsersController(mockService.Object, mockLogger.Object);

        //    // Act
        //    var result = await controller.Login(loginInput);

        //    // Assert - לבדוק שהפעולה הצליחה
        //    Assert.IsType<OkObjectResult>(result.Result);

        //    // Assert - לוודא שהלוג נכתב פעם אחת עם המידע
        //    mockLogger.Verify(
        //        x => x.Log(
        //            LogLevel.Information,
        //            It.IsAny<EventId>(),
        //            It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("logged in successfully")),
        //            null,
        //            It.IsAny<Func<It.IsAnyType, Exception, string>>()),
        //        Times.Once);
        //}

        //[Fact]
        //public async Task Login_ShouldNotLog_WhenLoginFails()
        //{
        //    // Arrange
        //    var mockService = new Mock<IUserService>();
        //    var mockLogger = new Mock<ILogger<UsersController>>();

        //    var loginInput = new UserLoginDTO { UserName = "wronguser", Password = "wrongpass" };

        //    mockService.Setup(x => x.Login(loginInput)).ReturnsAsync((UserDTO)null);

        //    var controller = new UsersController(mockService.Object, mockLogger.Object);

        //    // Act
        //    var result = await controller.Login(loginInput);

        //    // Assert
        //    Assert.IsType<ObjectResult>(result.Result); // StatusCode 400

        //    // לוודא שלא נכתב שום לוג
        //    mockLogger.Verify(
        //        x => x.Log(
        //            It.IsAny<LogLevel>(),
        //            It.IsAny<EventId>(),
        //            It.IsAny<It.IsAnyType>(),
        //            It.IsAny<Exception>(),
        //            It.IsAny<Func<It.IsAnyType, Exception, string>>()),
        //        Times.Never);
        //}
        [Fact]
        public async Task Login_ShouldLogInformation_WhenLoginSucceeds()
        {
            // Arrange
            var mockService = new Mock<IUserService>();
            var mockLogger = new Mock<ILogger<UsersController>>();

            var testUser = new UserDTO(1, "testuser", "Test", "User");
            var loginInput = new UserLoginDTO("testuser", "1234");

            mockService.Setup(x => x.Login(loginInput)).ReturnsAsync(testUser);

            var controller = new UsersController(mockService.Object, mockLogger.Object);

            // Act
            var result = await controller.Login(loginInput);

            // Assert - וידוא שהפעולה הצליחה
            Assert.IsType<OkObjectResult>(result.Result);

            // Assert - וידוא שהלוג נכתב
            mockLogger.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("logged in successfully")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

        [Fact]
        public async Task Login_ShouldNotLog_WhenLoginFails()
        {
            // Arrange
            var mockService = new Mock<IUserService>();
            var mockLogger = new Mock<ILogger<UsersController>>();

            var loginInput = new UserLoginDTO("wronguser", "wrongpass");

            mockService.Setup(x => x.Login(loginInput)).ReturnsAsync((UserDTO)null);

            var controller = new UsersController(mockService.Object, mockLogger.Object);

            // Act
            var result = await controller.Login(loginInput);

            // Assert - קוד שגיאה
            var objectResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(400, objectResult.StatusCode);

            // Assert - וידוא שלא נכתב לוג
            mockLogger.Verify(
                x => x.Log(
                    It.IsAny<LogLevel>(),
                    It.IsAny<EventId>(),
                    It.IsAny<It.IsAnyType>(),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Never);
        }
    }

}

