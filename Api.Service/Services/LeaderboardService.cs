using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services.Game;
using Api.Domain.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Service.Services
{
    public class LeaderboardService : ILeaderboardService<LeaderboardViewModel>
    {
        private ILeaderboardRepository<LeaderboardViewModel> _repository;
        public LeaderboardService(ILeaderboardRepository<LeaderboardViewModel> repository)
        {
            _repository = repository;
        }

        public async Task<List<LeaderboardViewModel>> GetTop100()
        {
            return await _repository.GetTop100();
        }
    }
}
