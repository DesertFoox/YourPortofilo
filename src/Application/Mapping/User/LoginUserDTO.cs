using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
   public  class LoginUserDTO
    {
        [MinLength(3)]
        [Required]
        public string Email { get; set; }
        [Required]
        [MinLength(2)]
        public string Password { get; set; }
    }
}
