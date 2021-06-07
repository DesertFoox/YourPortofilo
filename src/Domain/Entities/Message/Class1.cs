using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Message
{
    public class Class1
    {
        [Key]
        public int Id { get; private set; }
        [ForeignKey("User")]
        [Required]
        public Guid UserId { get; private set; }
        [Required]
        [MaxLength(64)]
        [MinLength(5)]
        public string Title { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [MaxLength(128)]
        [MinLength(5)]
        public string Email { get; set; }
        [Required]
        [MaxLength(128)]
        [MinLength(5)]
        public string Message { get; set; }
    }
}
