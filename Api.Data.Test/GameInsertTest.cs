using Api.Data.Context;
using Api.Data.Implementation;
using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static Api.Data.Test.BaseTest;

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
        }


        [Fact(DisplayName = "Deve inserir GameRepository")]
        [Trait("API", "GameResult")]
        public async Task GameRepository()
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
    }
}
