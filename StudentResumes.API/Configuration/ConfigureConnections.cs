using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudentResumes.Core.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentResumes.API.Configuration
{
    public static class ConfigureConnections
    {
        public static IServiceCollection AddConnectionProvider(this IServiceCollection services, IConfiguration conf)
        {
            services.AddDbContext<ResumesContext>(options => options.UseSqlite(conf.GetConnectionString("DataBase"),
                    b => b.MigrationsAssembly("StudentsResumes.API")));
            return services;
        }
    }
}
