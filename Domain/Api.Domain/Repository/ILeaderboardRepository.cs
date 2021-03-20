using Api.Domain.Interfaces;
using Api.Domain.ViewModel;

namespace Api.Domain.Repository
{
    public interface ILeaderboardRepository : ILeaderboardRepository<LeaderboardViewModel>
    {
    }
}