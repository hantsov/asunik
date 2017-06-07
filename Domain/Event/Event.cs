using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Identity;

namespace Domain.Event
{
    public class Event : BaseEntity
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [MaxLength(256)]
        public string Heading { get; set; }

        [MaxLength(4000)]
        public string Content { get; set; }

        public string Type { get; set; }

        public virtual User Author { get; set; }

        public virtual List<EventMember> EventMembers { get; set; }
    }
}
