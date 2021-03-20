using Api.Data.Context;
using Api.Data.Implementation;
using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Api.Data.Test
{
    public class GameInsertTest : BaseTest, IClassFixture<DbTest>
    {
        private readonly GameContext _context;

        public GameInsertTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<GameContext>();
            optionsBuilder.UseInMemoryDatabase("GameContext");
            var options = optionsBuilder.Options;
            _context = new GameContext(options);
            _context.Database.EnsureDeleted();
        }

        [Fact(DisplayName = "Deve inserir GameRepository")]
        [Trait("Data", "Create")]
        public async Task GameRepository_Create()
        {
            // Arrange
            GameImplementation _repositorio = new GameImplementation(_context);
            var game = new GameEntity()
            {
                GameId = 1,
                PlayerId = 30,
                Win = 5
            };

            // Act
            var result = await _repositorio.InsertAsync(game);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(game.GameId, result.GameId);
            Assert.Equal(game.PlayerId, result.PlayerId);
            Assert.False(game.Id == Guid.Empty);

            var registroCriado = await _context.Games.FirstOrDefaultAsync();
            Assert.NotNull(registroCriado);
            Assert.Equal(registroCriado.GameId, registroCriado.GameId);
            Assert.Equal(game.PlayerId, registroCriado.PlayerId);
            Assert.False(game.Id == Guid.Empty);
        }

        [Fact(DisplayName = "Deve atualizar GameRepository")]
        [Trait("Data", "Update")]
        public async Task GameRepository_Update()
        {
            // Arrange
            GameImplementation _repositorio = new GameImplementation(_context);

            var id = Guid.NewGuid();

            _context.Games.Add(new GameEntity()
            {
                GameId = 1,
                PlayerId = 30,
                Win = 5,
                Id = id,
            });

            _context.SaveChanges();

            var game = new GameEntity()
            {
                GameId = 2,
                PlayerId = 30,
                Win = 5,
                Id = id,
            };

            // Act
            var result = await _repositorio.UpdateAsync(game);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(game.GameId, result.GameId);
            Assert.Equal(game.PlayerId, result.PlayerId);
            Assert.False(game.Id == Guid.Empty);

            var registroCriado = await _context.Games.FirstOrDefaultAsync();
            Assert.NotNull(registroCriado);
            Assert.Equal(registroCriado.GameId, registroCriado.GameId);
            Assert.Equal(game.PlayerId, registroCriado.PlayerId);
            Assert.False(game.Id == Guid.Empty);
        }

        [Fact(DisplayName = "Deve retornar null quando registro não atualizado")]
        [Trait("Data", "Update")]
        public async Task GameRepository_Update_NotFound()
        {
            // Arrange
            GameImplementation _repositorio = new GameImplementation(_context);

            var id = Guid.NewGuid();

            _context.Games.Add(new GameEntity()
            {
                GameId = 1,
                PlayerId = 30,
                Win = 5,
                Id = id,
            });

            _context.SaveChanges();

            var game = new GameEntity()
            {
                GameId = 2,
                PlayerId = 30,
                Win = 5,
                Id = Guid.NewGuid(),
            };

            // Act
            var result = await _repositorio.UpdateAsync(game);

            //Assert
            Assert.Null(result);
        }

        [Fact(DisplayName = "Deve retornar todos os registros")]
        [Trait("Data", "SelectAsync")]
        public async Task GameRepository_SelectAsync()
        {
            // Arrange
            GameImplementation _repositorio = new GameImplementation(_context);

            _context.Games.Add(new GameEntity()
            {
                GameId = 1,
                PlayerId = 30,
                Win = 5,
                Id = Guid.NewGuid(),
            });

            _context.Games.Add(new GameEntity()
            {
                GameId = 2,
                PlayerId = 20,
                Win = 20,
                Id = Guid.NewGuid(),
            });

            _context.Games.Add(new GameEntity()
            {
                GameId = 3,
                PlayerId = 30,
                Win = 3,
                Id = Guid.NewGuid(),
            });

            _context.Games.Add(new GameEntity()
            {
                GameId = 4,
                PlayerId = 40,
                Win = 4,
                Id = Guid.NewGuid(),
            });

            _context.SaveChanges();

            // Act
            var result = await _repositorio.SelectAsync();

            //Assert
            Assert.NotNull(result);
            Assert.Collection(result,
                item =>
                {
                    Assert.Equal(1, item.GameId);
                },
                item =>
                {
                    Assert.Equal(2, item.GameId);
                },
                item =>
                {
                    Assert.Equal(3, item.GameId);
                },
                item =>
                {
                    Assert.Equal(4, item.GameId);
                });
        }

        [Fact(DisplayName = "Deve retornar um registro específico")]
        [Trait("Data", "SelectAsync {id}")]
        public async Task GameRepository_SelectAsyncComParametro()
        {
            // Arrange
            GameImplementation _repositorio = new GameImplementation(_context);

            var id = Guid.NewGuid();
            _context.Games.Add(new GameEntity()
            {
                GameId = 1,
                PlayerId = 30,
                Win = 5,
                Id = id,
            });

            _context.Games.Add(new GameEntity()
            {
                GameId = 2,
                PlayerId = 20,
                Win = 20,
                Id = Guid.NewGuid(),
            });

            _context.Games.Add(new GameEntity()
            {
                GameId = 3,
                PlayerId = 30,
                Win = 3,
                Id = Guid.NewGuid(),
            });

            _context.Games.Add(new GameEntity()
            {
                GameId = 4,
                PlayerId = 40,
                Win = 4,
                Id = Guid.NewGuid(),
            });

            _context.SaveChanges();

            // Act
            var result = await _repositorio.SelectAsync(id);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.GameId);
            Assert.Equal(30, result.PlayerId);
            Assert.Equal(id, result.Id);
        }
    }

}
