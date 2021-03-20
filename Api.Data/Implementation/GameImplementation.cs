using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Api.Data.Implementation
{
    public class GameImplementation : BaseRepository<GameEntity>
    {
        private DbSet<GameEntity> _dataset;
        public GameImplementation(GameContext context) : base(context)
        {
            _dataset = context.Set<GameEntity>();
        }
    }
}