using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class CreateCommentDTO
    {
        [Required]
        public Guid UserCreatorId { get; set; }
        [Required]
        public Guid BlogId { get; set; }
        [Required]
        public string Comment { get; set; }
    }
}
