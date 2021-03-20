using Api.Domain.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Domain.Interfaces.Services.Game
{
    public interface ILeaderboardService
    {
        Task<List<LeaderboardViewModel>> GetTop100();
    }
}
