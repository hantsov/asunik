using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Identity;

namespace Domain.Course
{
    public class CourseMember : BaseEntity
    {
        [Required]
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

        public string MemberRole { get; set; }
    }
}
