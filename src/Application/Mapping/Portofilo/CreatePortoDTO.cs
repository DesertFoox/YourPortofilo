using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping.Portofilo
{
    public class CreatePortoDTO
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        [MaxLength(100)]
        [MinLength(5)]
        public string Title { get; set; }
        [Required]
        [MaxLength(300)]
        [MinLength(10)]
        public string Description { get; set; }
        [Required]
        public string Image { get; set; }
    }
}
