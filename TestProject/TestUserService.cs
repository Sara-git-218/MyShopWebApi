using AutoMapper;
using DTO;
using Entities;
using Moq;
using Repositories;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Moq;
using Repositories;
using Services;
using AutoMapper;
using DTO;
using Xunit;
namespace TestProject
{
   
        public class TestUserService
        {
            [Fact]
            public void CheckPassword_WeakPassword_ReturnsLowScore()
            {
                // Arrange
                var userService = CreateService();
                var weakPassword = "123";

                // Act
                int score = userService.CheckPassword(weakPassword);

                // Assert
                Assert.InRange(score, 0, 1);
            }

            [Fact]
            public void CheckPassword_MediumPassword_ReturnsMidScoreOrHigher()
            {
                // Arrange
                var userService = CreateService();
                var mediumPassword = "abc12345";

                // Act
                int score = userService.CheckPassword(mediumPassword);

                // Assert
                Assert.InRange(score, 1, 4);
            }

            [Fact]
            public void CheckPassword_StrongPassword_ReturnsHighScore()
            {
                // Arrange
                var userService = CreateService();
                var strongPassword = "StrongPass!2024";

                // Act
                int score = userService.CheckPassword(strongPassword);

                // Assert
                Assert.InRange(score, 3, 4);
            }

            private UserService CreateService()
            {
                var mockRepo = new Mock<IUserRepository>();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<UserRegisterDTO, User>();
                    cfg.CreateMap<User, UserDTO>();
                });
                var mapper = config.CreateMapper();

                return new UserService(mockRepo.Object, mapper);
            }
        }
    }



