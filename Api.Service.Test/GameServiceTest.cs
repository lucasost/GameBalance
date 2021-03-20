using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.Game;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Api.Service.Test
{
    public class GameServiceTest
    {
        private IGameService _service;

        private Mock<IGameService> _serviceMoq;

        [Fact(DisplayName = "É possível executar método GET")]
        [Trait("Service", "Get")]
        public async Task E_Possivel_Executar_Metodo_GET()
        {
            // Arrange
            var id = Guid.NewGuid();
            _serviceMoq = new Mock<IGameService>();

            _serviceMoq.Setup(a => a.Get(id)).ReturnsAsync(new GameEntity()
            {
                Id = id,
                GameId = 1,
                PlayerId = 2,
                Win = 10
            });
            _service = _serviceMoq.Object;

            // Act
            var result = await _service.Get(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.GameId);
            Assert.Equal(2, result.PlayerId);
            Assert.Equal(id, result.Id);
            Assert.Equal(10, result.Win);
        }

        [Fact(DisplayName = "Ao executar método GET e não encontrar registro deve retornar null")]
        [Trait("Service", "Get Null")]
        public async Task Executar_Metodo_GET_Returno_Null()
        {
            // Arrange
            var id = Guid.NewGuid();
            _serviceMoq = new Mock<IGameService>();

            _serviceMoq.Setup(a => a.Get(id)).ReturnsAsync(new GameEntity()
            {
                Id = id,
                GameId = 1,
                PlayerId = 2,
                Win = 10
            });
            _service = _serviceMoq.Object;

            // Act
            var result = await _service.Get(Guid.NewGuid());

            // Assert
            Assert.Null(result);
        }

        [Fact(DisplayName = "É possível executar método GET All")]
        [Trait("Service", "GetAll")]
        public async Task E_Possivel_Executar_Metodo_GetAll()
        {
            // Arrange
            var id = Guid.NewGuid();
            _serviceMoq = new Mock<IGameService>();

            _serviceMoq.Setup(a => a.GetAll()).ReturnsAsync(new List<GameEntity>()
            {
                new GameEntity(){
                    Id = id,
                    GameId = 2,
                    PlayerId = 3,
                    Win = 30
                },
                 new GameEntity(){
                    Id = id,
                    GameId = 3,
                    PlayerId = 4,
                    Win = 40
                },
                new GameEntity(){
                    Id = id,
                    GameId = 4,
                    PlayerId = 5,
                    Win = 50
                },
            });

            _service = _serviceMoq.Object;

            // Act
            var result = await _service.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Collection(result,
               item =>
               {
                   Assert.Equal(2, item.GameId);
                   Assert.Equal(3, item.PlayerId);
               },
               item =>
               {
                   Assert.Equal(3, item.GameId);
                   Assert.Equal(4, item.PlayerId);
               },
               item =>
               {
                   Assert.Equal(4, item.GameId);
                   Assert.Equal(5, item.PlayerId);
               });
        }

        [Fact(DisplayName = "Deve executar método POST")]
        [Trait("Service", "POST")]
        public async Task Executar_Metodo_Post()
        {
            // Arrange
            var id = Guid.NewGuid();
            _serviceMoq = new Mock<IGameService>();

            var createGame = new GameEntity()
            {
                GameId = 1,
                PlayerId = 2,
                Win = 10
            };

            _serviceMoq.Setup(a => a.Post(createGame)).ReturnsAsync(new GameEntity()
            {
                Id = id,
                GameId = 1,
                PlayerId = 2,
                Win = 10,
                Timestamp = DateTime.UtcNow
            });
            _service = _serviceMoq.Object;

            // Act
            var result = await _service.Post(createGame);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.GameId);
            Assert.Equal(2, result.PlayerId);
            Assert.Equal(id, result.Id);
            Assert.Equal(10, result.Win);
        }

        [Fact(DisplayName = "Deve executar método Update")]
        [Trait("Service", "Update")]
        public async Task Executar_Metodo_Update()
        {
            // Arrange
            var id = Guid.NewGuid();
            _serviceMoq = new Mock<IGameService>();
            var createGame = new GameEntity()
            {
                GameId = 1,
                PlayerId = 2,
                Win = 10
            };

            _serviceMoq.Setup(a => a.Post(createGame)).ReturnsAsync(new GameEntity()
            {
                Id = id,
                GameId = 1,
                PlayerId = 2,
                Win = 10,
                Timestamp = DateTime.UtcNow
            });

            _serviceMoq = new Mock<IGameService>();

            var updateGame = new GameEntity()
            {
                Id = id,
                GameId = 1,
                PlayerId = 2,
                Win = 10,
                Timestamp = DateTime.UtcNow
            };

            _serviceMoq.Setup(a => a.Put(updateGame)).ReturnsAsync(new GameEntity()
            {
                Id = id,
                GameId = 3,
                PlayerId = 4,
                Win = 20,
                Timestamp = DateTime.UtcNow
            });
            _service = _serviceMoq.Object;

            // Act
            var result = await _service.Put(updateGame);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.GameId);
            Assert.Equal(4, result.PlayerId);
            Assert.Equal(id, result.Id);
            Assert.Equal(20, result.Win);
        }

        [Fact(DisplayName = "Deve executar método DELETE")]
        [Trait("Service", "Delete")]
        public async Task Executar_Metodo_Delete()
        {
            // Arrange
            var id = Guid.NewGuid();
            _serviceMoq = new Mock<IGameService>();

            _serviceMoq.Setup(a => a.Delete(It.IsAny<Guid>())).ReturnsAsync(true);
            _service = _serviceMoq.Object;

            // Act
            var result = await _service.Delete(id);

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Deve executar método DELETE deve retornar FALSE")]
        [Trait("Service", "Delete False")]
        public async Task Executar_Metodo_Delete_Retorno_False()
        {
            // Arrange
            var id = Guid.NewGuid();
            _serviceMoq = new Mock<IGameService>();

            _serviceMoq.Setup(a => a.Delete(It.IsAny<Guid>())).ReturnsAsync(false);
            _service = _serviceMoq.Object;

            // Act
            var result = await _service.Delete(id);

            // Assert
            Assert.False(result);
        }
    }
}
