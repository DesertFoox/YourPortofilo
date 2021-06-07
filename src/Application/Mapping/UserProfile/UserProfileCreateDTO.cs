using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class UserProfileCreateDTO
    {
        public Guid UserId { get; set; }
        [MaxLength(128)]
        [Required]
        public string FirstName { get; set; }
        [MaxLength(128)]
        [Required]
        public string LastName { get; set; }
        [Required]
        [MaxLength(12)]
        public string PhoneNumber { get; set; }
        [MaxLength(128)]
        public string City { get; set; }
        [MaxLength(256)]
        public string Instagram { get; set; }
        public string TwitterLink { get; set; }
        [MaxLength(256)]
        public string TelegramId { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }

    }
}
