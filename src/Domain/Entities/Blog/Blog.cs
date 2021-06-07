using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Blog
    {
        [Key]
        public Guid Id { get;  set; }
        [Required]
        [MaxLength(128)]
        [MinLength(3)]
        public string Title { get;  set; }
        [Required]
        [MinLength(7)]
        public string Description { get;  set; }
        public string Image { get;  set; }
        [ForeignKey("User")]
        [Required]
        public Guid UserId { get;  set; }
        public DateTime CreateDate { get;  set; }

        public User User { get;  set; }

        public List<BlogComment> Comments { get; set; }
    }
}
