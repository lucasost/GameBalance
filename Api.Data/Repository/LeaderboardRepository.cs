using Api.Data.Context;
using Api.Domain.Interfaces;
using Api.Domain.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Data.Repository
{
    public class LeaderboardRepository<T> : ILeaderboardRepository<LeaderboardViewModel>
    {
        private readonly GameContext _context;
        public LeaderboardRepository(GameContext context)
        {
            _context = context;
        }

        public async Task<List<LeaderboardViewModel>> GetTop100()
        {
            try
            {
                var top100 = _context.Games
                    .GroupBy(a => a.PlayerId)
                    .Take(100)
                    .Select(group => new LeaderboardViewModel
                    {
                        PlayerId = group.Key,
                        Balance = group.Sum(a => a.Win),
                        LastUpdateDate = DateTime.UtcNow
                    })
                    .OrderByDescending(b=>b.Balance)
                    .ToList();

                return top100;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
