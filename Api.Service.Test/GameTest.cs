using Api.Domain.Interfaces.Services.Game;

namespace Api.Service.Test
{
    public class GameTest : BaseTestService
    {
        public GameTest(IGameService service) : base(service)
        {
        }
    }
}
