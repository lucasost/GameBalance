using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Domain.Interfaces
{
    public interface ILeaderboardRepository<T>
    {
        Task<List<T>> GetTop100();
    }
}
