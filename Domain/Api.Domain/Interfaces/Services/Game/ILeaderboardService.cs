using Api.Domain.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Domain.Interfaces.Services.Game
{
    public interface ILeaderboardService<T>
    {
        Task<List<LeaderboardViewModel>> GetTop100();
    }
}
