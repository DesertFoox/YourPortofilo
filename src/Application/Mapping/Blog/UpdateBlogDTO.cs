using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class UpdateBlogDTO
    {
        [Required]
        [MaxLength(128)]
        [MinLength(3)]
        public string Title { get; set; }
        [Required]
        [MinLength(7)]
        public string Description { get; set; }
     }
}
