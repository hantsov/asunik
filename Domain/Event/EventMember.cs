using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Identity;

namespace Domain.Event
{
    public class EventMember : BaseEntity
    {
        [Required]
        [Key]
        public int Id { get; set; }

        public Event Event { get; set; }

        public User Member { get; set; }
    }
}
