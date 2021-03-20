using System;

namespace Api.Domain.ViewModel
{
    public class LeaderboardViewModel
    {
        public long PlayerId { get; set; }

        public long Balance { get; set; }

        public DateTime LastUpdateDate { get; set; }
    }
}
