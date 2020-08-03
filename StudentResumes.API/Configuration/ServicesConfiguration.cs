using Microsoft.Extensions.DependencyInjection;
using StudentResumes.Core.Repositories;
using StudentResumes.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentResumes.API.Configuration
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services
                .AddTransient<IRefereeRepository, RefereeRepository>();
               




            return services;
        }

        public static IServiceCollection AddCorsConfiguration(this IServiceCollection services) =>
           services.AddCors(options => {
               options.AddPolicy("AllowAll", builder =>
               builder.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader());
           });
    }
}
