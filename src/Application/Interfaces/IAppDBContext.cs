using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
   public interface IAppDBContext
    {
         DbSet<User> Users { get; set; }
         DbSet<Blog> Blogs { get; set; }
         DbSet<BlogComment> BlogComments { get; set; }
         DbSet<Skill> Skills { get; set; }
         DbSet<PortoFilo> PortoFilos { get; set; }
         DbSet<UserProfile> UserProfile { get; set; }

        int SaveChanges();
    }
}
