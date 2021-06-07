using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserProfile
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get;  set; }
        [MaxLength(256)]
        public string Image { get;  set; }
        [MaxLength(128)]
        public string FirstName { get;  set; }
        [MaxLength(128)]
        public string LastName { get;  set; }
        [MaxLength(11)]
        public string PhoneNumber { get;  set; }
        [MaxLength(128)]
        public string City { get;  set; }
        [MaxLength(256)]
        public string Instagram { get; set; } 
        public string TwitterLink { get;  set; }
        [MaxLength(256)]
        public string TelegramId { get;  set; }
        public DateTime BirthDate { get;  set; }

        [ForeignKey("UserId")]
        public User User { get;  set; }

    }
}
