using Application.Interfaces;
using Application.Interfaces.Portofilo;
using Infranstructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Infranstructure
{
    public static class DependencyInjectsion
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDBContext>(options =>
                          options.UseSqlServer(
                              configuration.GetConnectionString("PortoFiloDb"),
                              b => b.MigrationsAssembly(typeof(AppDBContext).Assembly.FullName)));


            services.AddScoped<IAppDBContext>(provider => provider.GetService<AppDBContext>());
            services.AddTransient<IUserRepo, UserSql>();
            services.AddTransient<ISkillRepo, SkillSql>();
            services.AddTransient<IBlogRepo,BlogSql>();
            services.AddTransient<IPortofiloRepo, PortofiloSql>();
            services.AddTransient<IBlogCommentRepo,BlogCommentSql>();
            return services;
        }
     
    }
}
