using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Domain.Entities
{
    public class Game : BaseEntity
    {
        public long PlayerId { get; set; }
        public long GameId { get; set; }
        public long Win { get; set; }
    }
}
