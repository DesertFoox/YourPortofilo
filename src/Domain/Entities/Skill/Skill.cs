using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Skill
    {
        [Key]
        public Guid Id { get;  set; }
        [ForeignKey("User")]
        public Guid UserId { get;  set; }
        [Required]
        public int KnowLedge { get;  set; }
        [Required]
        [MaxLength(100)]
        [MinLength(3)]
        public string SkillName { get;  set; }

        public User User { get; set; }
    }
}
