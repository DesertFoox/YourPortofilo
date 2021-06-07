using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping.Portofilo
{
    public class UpdatePortofiloDTO
    {
        [Required]
        [MaxLength(100)]
        [MinLength(5)]
        public int Title { get; set; }
        [Required]
        [MaxLength(300)]
        [MinLength(10)]
        public string Description { get; set; }
    }
}
