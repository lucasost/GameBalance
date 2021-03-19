using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services.Game;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Service.Services
{
    public class GameService : IGameService
    {
        private IRepository<GameEntity> _repository;
        public GameService(IRepository<GameEntity> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<GameEntity> Get(Guid id)
        {
            return await _repository.SelectAsync(id);
        }

        public async Task<IEnumerable<GameEntity>> GetAll()
        {
            return await _repository.SelectAsync();
        }

        public async Task<GameEntity> Post(GameEntity user)
        {
            return await _repository.InsertAsync(user);
        }

        public async Task<GameEntity> Put(GameEntity user)
        {
            return await _repository.UpdateAsync(user);
        }
    }
}
