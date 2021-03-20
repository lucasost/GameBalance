using Api.Domain.Interfaces.Services.Game;
using System;
using Xunit;

namespace Api.Service.Test
{
    public abstract class BaseTestService
    {
        private readonly IGameService _service;

        protected BaseTestService(IGameService service)
        {
            _service = service;
        }
    }
}
