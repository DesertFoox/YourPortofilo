using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PortoFilo
    {
        [Key]
        public Guid Id { get;  set; }
        [ForeignKey("User")]
        public Guid UserId { get;  set; }
        [Required]
        [MaxLength(100)]
        [MinLength(5)]
        public string Title { get;  set; }
        [Required]
        [MaxLength(300)]
        [MinLength(10)]
        public string Description { get;  set; }
        [Required]
        public string Image { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; }
        public User User { get; set; }
    }
}
