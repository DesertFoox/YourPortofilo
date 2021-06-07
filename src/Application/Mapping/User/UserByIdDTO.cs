using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class UserByIdDTO
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
        public bool IsBanned { get; set; }
        public bool IsDeleted { get; set; }
        public UserProfile UserProfile { get; set; }
        public List<Blog> UserBlogs { get; set; }
        public List<Skill> UserSkills { get; set; }
        public List<PortoFilo> PortoFiles { get; set; }
    }
}
