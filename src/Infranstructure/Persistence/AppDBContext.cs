using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Application.Interfaces;
namespace Infranstructure.Persistence
{
    public class AppDBContext : DbContext,IAppDBContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogComment> BlogComments { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<PortoFilo> PortoFilos  { get; set; }
        public DbSet<UserProfile> UserProfile { get; set; }
    }

}
