using Api.Application.Controllers;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.Game;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Api.Application.Test.GamesTest
{
    public class Games
    {
        private GamesController _controller;

        private Mock<IGameService> serviceMoq;

        [Fact(DisplayName = "Quando Requisitar Post")]
        [Trait("Application", "POST")]
        public async Task Quando_Requisitar_Post()
        {
            // Arrange
            serviceMoq = new Mock<IGameService>();

            var gameCreate = new GameEntity()
            {
                GameId = 5,
                PlayerId = 4,
                Win = 2
            };

            serviceMoq.Setup(a => a.Post(gameCreate)).ReturnsAsync(new GameEntity()
            {
                GameId = 5,
                PlayerId = 4,
                Win = 2,
                Id = Guid.NewGuid(),
            });

            _controller = new GamesController(serviceMoq.Object);
            Mock<IUrlHelper> url = new Mock<IUrlHelper>();
            url.Setup(a => a.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost:5000");
            _controller.Url = url.Object;

            // Act
            var result = await _controller.Post(gameCreate);

            // Assert
            Assert.NotNull(result);
            var resultType = Assert.IsType<CreatedResult>(result);
            Assert.Equal(201, resultType.StatusCode);
            var entityResult = Assert.IsType<GameEntity>(resultType.Value);
            Assert.True(Guid.Empty != entityResult.Id);
            Assert.Equal(4, entityResult.PlayerId);
            Assert.Equal(5, entityResult.GameId);
            Assert.Equal(2, entityResult.Win);
        }

        [Fact(DisplayName = "Quando Requisitar Update")]
        [Trait("Application", "Update")]
        public async Task Quando_Requisitar_Update()
        {
            // Arrange
            var serviceMock = new Mock<IGameService>();

            serviceMock.Setup(m => m.Put(It.IsAny<GameEntity>())).ReturnsAsync(
                new GameEntity
                {
                    GameId = 5,
                    PlayerId = 4,
                    Win = 2,
                    Id = Guid.NewGuid(),
                }
            );

            _controller = new GamesController(serviceMock.Object);

            var gameUpdate = new GameEntity
            {
                Id = Guid.NewGuid(),
                PlayerId = 4,
                Win = 3,
                GameId = 3,
            };

            // Act
            var result = await _controller.Put(gameUpdate);

            // Assert
            Assert.NotNull(result);
            Assert.True(result is OkObjectResult);
            var resultType = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, resultType.StatusCode);
            var entityResult = Assert.IsType<GameEntity>(resultType.Value);
            Assert.True(Guid.Empty != entityResult.Id);
        }


        [Fact(DisplayName = "Quando Requisitar Update - BadRequest")]
        [Trait("Application", "Update - BadRequest")]
        public async Task E_Possivel_Invocar_a_Controller_Update()
        {
            var serviceMock = new Mock<IGameService>();

            serviceMock.Setup(m => m.Put(It.IsAny<GameEntity>())).ReturnsAsync(
                new GameEntity
                {
                    GameId = 5,
                    PlayerId = 4,
                    Win = 2,
                    Id = Guid.NewGuid(),
                }
            );

            _controller = new GamesController(serviceMock.Object);
            _controller.ModelState.AddModelError("GameId", "É um campo obrigatorio");

            var userDtoUpdate = new GameEntity
            {
                Id = Guid.NewGuid(),
                PlayerId = 4,
                Win = 3,
                GameId = 3,
            };

            var result = await _controller.Put(userDtoUpdate);
            Assert.True(result is BadRequestObjectResult);
            Assert.False(_controller.ModelState.IsValid);
        }

        [Fact(DisplayName = "Quando Requisitar o Delete")]
        [Trait("Application", "Delete")]
        public async Task Quando_Requisitar_Delete()
        {
            // Arrange
            var serviceMock = new Mock<IGameService>();
            var id = Guid.NewGuid();
            serviceMock.Setup(m => m.Delete(It.IsAny<Guid>())).ReturnsAsync(true);

            _controller = new GamesController(serviceMock.Object);

            // Act
            var result = await _controller.Delete(id);

            // Assert
            Assert.NotNull(result);
            Assert.True(result is OkObjectResult);
            var resultType = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, resultType.StatusCode);
        }

        [Fact(DisplayName = "Quando Requisitar o Delete - BadRequest")]
        [Trait("Application", "Delete - BadRequest")]
        public async Task Quando_Requisitar_Delete_BadRequest()
        {
            // Arrange
            var serviceMock = new Mock<IGameService>();
            var id = Guid.NewGuid();
            serviceMock.Setup(m => m.Delete(It.IsAny<Guid>())).ReturnsAsync(false);

            _controller = new GamesController(serviceMock.Object);
            _controller.ModelState.AddModelError("Id", "Id não informado.");

            // Act
            var result = await _controller.Delete(id);

            // Assert
            Assert.True(result is BadRequestObjectResult);
            Assert.False(_controller.ModelState.IsValid);
        }

        [Fact(DisplayName = "Quando Requisitar GetAll - BadRequest")]
        [Trait("Application", "GetAll - BadRequest")]
        public async Task Quando_Requisitar_GetAll_BadRequest()
        {
            // Arrange
            var serviceMock = new Mock<IGameService>();
            serviceMock.Setup(a => a.GetAll()).ReturnsAsync(new List<GameEntity>
            {
                new GameEntity{
                    GameId = 5,
                    PlayerId = 4,
                    Win = 2,
                    Id = Guid.NewGuid(),
                },
                 new GameEntity{
                    GameId = 1,
                    PlayerId = 2,
                    Win = 3,
                    Id = Guid.NewGuid(),
                }
            });

            _controller = new GamesController(serviceMock.Object);
            _controller.ModelState.AddModelError("Id", "Formato Invalido");

            // Act
            var result = await _controller.GetAll();

            // Assert
            Assert.True(result is BadRequestObjectResult);
            Assert.False(_controller.ModelState.IsValid);
        }

        [Fact(DisplayName = "Quando Requisitar GetAll")]
        [Trait("Application", "GetAll")]
        public async Task Quando_Requisitar_GetAll()
        {
            // Arrange
            var serviceMock = new Mock<IGameService>();
            serviceMock.Setup(a => a.GetAll()).ReturnsAsync(new List<GameEntity>
            {
                new GameEntity{
                    GameId = 5,
                    PlayerId = 4,
                    Win = 2,
                    Id = Guid.NewGuid(),
                },
                 new GameEntity{
                    GameId = 1,
                    PlayerId = 2,
                    Win = 3,
                    Id = Guid.NewGuid(),
                }
            });

            _controller = new GamesController(serviceMock.Object);

            // Act
            var result = await _controller.GetAll();

            // Assert
            Assert.True(result is OkObjectResult);
            var resultValue = ((OkObjectResult)result).Value as IEnumerable<GameEntity>;
            Assert.True(resultValue.Count() == 2);
        }

        [Fact(DisplayName = "Quando Requisitar Get - BadRequest")]
        [Trait("Application", "GetAll - BadRequest")]
        public async Task Quando_Requisitar_Get_BadRequest()
        {
            // Arrange
            var serviceMock = new Mock<IGameService>();
            serviceMock.Setup(a => a.Get(It.IsAny<Guid>())).ReturnsAsync(new GameEntity()
            {
                GameId = 5,
                PlayerId = 4,
                Win = 2,
                Id = Guid.NewGuid(),
            });

            _controller = new GamesController(serviceMock.Object);
            _controller.ModelState.AddModelError("Id", "Formato Invalido");

            // Act
            var result = await _controller.Get(Guid.NewGuid());

            // Assert
            Assert.True(result is BadRequestObjectResult);
            Assert.False(_controller.ModelState.IsValid);
        }

        [Fact(DisplayName = "Quando Requisitar Get")]
        [Trait("Application", "GetAll")]
        public async Task Quando_Requisitar_Get()
        {
            // Arrange
            var serviceMock = new Mock<IGameService>();
            var id = Guid.NewGuid();
            serviceMock.Setup(a => a.Get(It.IsAny<Guid>())).ReturnsAsync(new GameEntity
            {
                GameId = 5,
                PlayerId = 4,
                Win = 2,
                Id = id,
            });

            _controller = new GamesController(serviceMock.Object);

            // Act
            var result = await _controller.Get(id);

            // Assert
            Assert.True(result is OkObjectResult);
            var resultValue = ((OkObjectResult)result).Value as GameEntity;
            Assert.NotNull(resultValue);
            Assert.Equal(4, resultValue.PlayerId);
            Assert.Equal(5, resultValue.GameId);
        }
    }
}
