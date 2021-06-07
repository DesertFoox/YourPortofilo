using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class UpdateSkillDTO
    {
        [Required]
        public int KnowLedge { get; set; }
        [Required]
        [MaxLength(100)]
        public string SkillName { get; set; }
    }
}
