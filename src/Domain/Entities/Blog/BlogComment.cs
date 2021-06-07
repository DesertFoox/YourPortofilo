using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class BlogComment
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid UserCreatorId { get; set; }
        [ForeignKey("Blog")]
        public Guid BlogId { get; set; }
        public string Comment { get; set; }
        public bool IsVerified { get; set; }
        public Blog Blog { get; set; }
    }
}
