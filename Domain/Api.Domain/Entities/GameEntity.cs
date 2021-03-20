using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Api.Domain.Entities
{
    [Table("Games")]
    public class GameEntity : BaseEntity
    {
        [Display(Name = "Player")]
        public long PlayerId { get; set; }

        [Display(Name = "Game")]
        public long GameId { get; set; }

        public long Win { get; set; }

        private DateTime _timestamp;

        public DateTime Timestamp
        {
            get { return _timestamp; }
            set { _timestamp = DateTime.UtcNow; }
        }
    }
}
