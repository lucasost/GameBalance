using Api.Domain.Interfaces.Services.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Service.Test
{
    public class GameTest : BaseTestService
    {
        public GameTest(IGameService service) : base(service)
        {
        }


    }
}
