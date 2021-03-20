using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Domain.Entities
{
    [Table("Games")]
    public class GameEntity : BaseEntity
    {
        public GameEntity()
        {
            Timestamp = DateTime.UtcNow;
        }

        [Display(Name = "Player")]
        public long PlayerId { get; set; }

        [Display(Name = "Game")]
        public long GameId { get; set; }

        public long Win { get; set; }

        public DateTime Timestamp
        {
            get; private set;
        }
    }
}
