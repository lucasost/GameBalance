using Api.Application.Controllers;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.Game;
using Api.Domain.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.Application.Test.LeaderboardTest
{
    public class Leaderboard
    {
        private LeaderboardController _controller;

        private Mock<ILeaderboardService> serviceMoq;

        [Fact(DisplayName = "Quando Requisitar GetTop100 - BadRequest")]
        [Trait("Application", "GetTop100 - BadRequest")]
        public async Task Quando_Requisitar_GetAll_BadRequest()
        {
            // Arrange
            var serviceMock = new Mock<ILeaderboardService>();
            var id = Guid.NewGuid();
            serviceMock.Setup(a => a.GetTop100()).ReturnsAsync(new List<LeaderboardViewModel>
            {
                new LeaderboardViewModel{
                    PlayerId = 4,
                    Balance = 2,
                    LastUpdateDate = DateTime.UtcNow
                },
                 new LeaderboardViewModel{
                    PlayerId = 2,
                    Balance = 3,
                    LastUpdateDate = DateTime.UtcNow
                }
            });

            _controller = new LeaderboardController(serviceMock.Object);
            _controller.ModelState.AddModelError("Id", "Formato Invalido");

            // Act
            var result = await _controller.GetTop100();

            // Assert
            Assert.True(result is BadRequestObjectResult);
            Assert.False(_controller.ModelState.IsValid);
        }

        [Fact(DisplayName = "Quando Requisitar GetTop100")]
        [Trait("Application", "GetTop100")]
        public async Task Quando_Requisitar_GetAll()
        {
            // Arrange
            var serviceMock = new Mock<ILeaderboardService>();
            var id = Guid.NewGuid();
            serviceMock.Setup(a => a.GetTop100()).ReturnsAsync(new List<LeaderboardViewModel>
            {
                new LeaderboardViewModel{
                    PlayerId = 4,
                    Balance = 2,
                    LastUpdateDate = DateTime.UtcNow
                },
                 new LeaderboardViewModel{
                    PlayerId = 2,
                    Balance = 3,
                    LastUpdateDate = DateTime.UtcNow
                }
            });

            _controller = new LeaderboardController(serviceMock.Object);

            // Act
            var result = await _controller.GetTop100();

            // Assert
            Assert.True(result is OkObjectResult);
            var resultValue = ((OkObjectResult)result).Value as IList<LeaderboardViewModel>;
            Assert.True(resultValue.Count() == 2);
        }
    }
}
