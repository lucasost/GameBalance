using Api.Domain.Interfaces.Services.Game;
using System;
using Xunit;

namespace Api.Service.Test
{
    public abstract class BaseTestService
    {
        private IGameService _service;

        public BaseTestService(IGameService service)
        {
            _service = service;
        }
    }
}
