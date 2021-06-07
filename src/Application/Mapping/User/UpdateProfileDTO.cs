using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
   public  class UpdateProfileDTO
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(11)]
        public string PhoneNumber { get; set; }
        [MaxLength(20)]
        [Required]
        public string City { get; set; }
        [MaxLength(256)]
        public string Instagram { get; set; }
        [MaxLength(256)]
        public string TwitterLink { get; set; }
        [MaxLength(256)]
        public string TelegramId { get; set; }
    }
}
