using Api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Domain.Interfaces.Services.Game
{
    public interface IGameService
    {
        Task<GameEntity> Get(Guid id);
        Task<IEnumerable<GameEntity>> GetAll();
        Task<GameEntity> Post(GameEntity user);
        Task<GameEntity> Put(GameEntity user);
        Task<bool> Delete(Guid id);
    }
}
