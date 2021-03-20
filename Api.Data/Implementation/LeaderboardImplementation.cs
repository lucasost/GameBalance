using Api.Data.Context;
using Api.Domain.Interfaces;
using Api.Domain.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Data.Implementation
{
    public class LeaderboardImplementation : ILeaderboardRepository<LeaderboardViewModel>
    {
        private readonly GameContext _context;

        public LeaderboardImplementation(GameContext context)
        {
            _context = context;
        }

        public async Task<List<LeaderboardViewModel>> GetTop100()
        {
            var top100 = await _context.Games
                 .GroupBy(a => a.PlayerId)
                 .Take(100)
                 .Select(group => new LeaderboardViewModel
                 {
                     PlayerId = group.Key,
                     Balance = group.Sum(a => a.Win),
                     LastUpdateDate = DateTime.UtcNow
                 })
                 .OrderByDescending(b => b.Balance)
                 .ToListAsync();

            return top100;
        }
    }
}