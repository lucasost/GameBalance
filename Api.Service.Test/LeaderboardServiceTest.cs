using Api.Domain.Interfaces.Services.Game;
using Api.Domain.ViewModel;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Api.Service.Test
{
    public class LeaderboardServiceTest
    {
        private ILeaderboardService _service;

        private Mock<ILeaderboardService> _serviceMoq;

        [Fact(DisplayName = "É possível executar método GetTop100")]
        [Trait("Service", "GetTop100")]
        public async Task E_Possivel_Executar_Metodo_GET()
        {
            // Arrange
            var id = Guid.NewGuid();
            _serviceMoq = new Mock<ILeaderboardService>();

            _serviceMoq.Setup(a => a.GetTop100()).ReturnsAsync(new List<LeaderboardViewModel>()
            {
                new  LeaderboardViewModel(){
                     PlayerId = 2,
                     Balance = 10
                },
                 new  LeaderboardViewModel(){
                     PlayerId = 3,
                     Balance = 8
                },

            });
            _service = _serviceMoq.Object;

            // Act
            var result = await _service.GetTop100();

            // Assert
            Assert.NotNull(result);
            Assert.Collection(result,
               item =>
               {
                   Assert.Equal(10, item.Balance);
                   Assert.Equal(2, item.PlayerId);
               },
               item =>
               {
                   Assert.Equal(8, item.Balance);
                   Assert.Equal(3, item.PlayerId);
               });
        }
    }
}
