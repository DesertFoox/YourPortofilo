using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class RegisterUserDTO
    {
        [Required]
        [MaxLength(20)]
        [MinLength(3)]
        public string UserName { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required]
        [MaxLength(128)]
        [MinLength(5)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required]
        [MaxLength(128)]
        [MinLength(8)]
        
        public string Password { get; set; }
    }
}
