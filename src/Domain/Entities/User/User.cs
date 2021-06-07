using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Domain.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get;  set; }
        [Required]
        [MaxLength(128)]
        [MinLength(1)]
        public string UserName { get;  set; }
        [DataType(DataType.EmailAddress)]
        [Required]
        [MaxLength(128)]
        [MinLength(1)]
        public string Email { get;  set; }
        [DataType(DataType.Password)]
        [Required]
        [MaxLength(128)]
        [MinLength(1)]
        public string Password { get;  set; }
        [DataType(DataType.Date)]
        [Required]
        public DateTime CreateDate { get;  set; }
        public string Role { get;  set; }
        public bool IsActive { get;  set; }
        public bool IsBanned { get;  set; }
        public bool IsDeleted { get; set; }
        public UserProfile UserProfile { get; set; }
        public List<Blog> UserBlogs { get; set; }
        public List<Skill> UserSkills { get; set; }
        public List<PortoFilo> PortoFiles { get; set; }
    }
}
