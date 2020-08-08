using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StudentResumes.AUTH;
using StudentResumes.AUTH.Interfaces;
using StudentResumes.AUTH.Services;
using StudentResumes.Core.EF;
using StudentResumes.Core.ExceptionMiddleware;
using StudentResumes.Core.Repositories;
using StudentResumes.Core.Services;
using StudentResumes.Core.Services.Impl;
using StudentResumes.Data;
using StudentResumes.Data.Entities;
using StudentResumes.Data.Repositories;
using StudentResumes.Data.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace StudentResumes.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers()
                .AddNewtonsoftJson(
                x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            ConfigureIdentity(services);

            ConfigureAuthentication(services, Configuration);

            AddServices(services);

            AddRepositories(services);

            AddDbConnection(services);

            AddSwagger(services);

            AddCorsConfiguration(services);

            AddSettings(services);
       
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime applicationLifetime, ILoggerFactory loggerFactory)
        {
            RegisterLogger(env, loggerFactory, applicationLifetime);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            EnableSwagger(app);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.ConfigureExceptionMiddleware();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


        #region Private methods
        private static void AddRepositories(IServiceCollection services)
        {
            services
                .AddTransient<IRefereeRepository, RefereeRepository>()
                .AddTransient<IStudentRepository, StudentRepository>()
                .AddTransient<ISkillRepository, SkillRepository>()
                .AddTransient<IStudentSkillRepository, StudentSkillRepository>();
        }

        private static void AddServices(IServiceCollection services)
        {
            services
                .AddTransient<IStorageService, StorageService>()
                .AddTransient<IJwtGenerator, JwtGenerator>()
                .AddTransient<IAuthService, AuthService>();
        }

        private static void AddCorsConfiguration(IServiceCollection services) =>
           services.AddCors(options => {
               options.AddPolicy("AllowAll", builder =>
               builder.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader());
           });

        private void ConfigureAuthentication(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = true;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateAudience = true,
                            ValidAudience = configuration["Audience"],
                            ValidateIssuer = true,
                            ValidIssuer = configuration["Issuer"],
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Key"])),
                            ValidateLifetime = true
                        };
                    });
        }

        private void ConfigureIdentity(IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole<Guid>>(o =>
            {
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireLowercase = false;
            })
                .AddEntityFrameworkStores<ResumesContext>();

        }

        private void AddSettings(IServiceCollection services)
        {
            services.Configure<EnvironmentConfig>(Configuration);
        }


        private static void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "StudentResumes",
                    Version = "v1",
                    Description = "ASP.NET Core Web API"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        private static void RegisterLogger(IHostEnvironment env, ILoggerFactory loggerFactory, IHostApplicationLifetime applicationLifetime)
        {
            var log4NetProviderOptions = new Log4NetProviderOptions("log4net.config");

            loggerFactory.AddLog4Net(log4NetProviderOptions);
            Logger.RegisterLogger(loggerFactory.CreateLogger("LOGGER"));

            applicationLifetime.ApplicationStarted.Register(
                () =>
                {
                    Logger.Log.LogInformation("Service started");
                    Logger.Log.LogInformation($"Settings {env.EnvironmentName}");
                });
        }

        private void AddDbConnection(IServiceCollection services)
        {
            var connection = Configuration["DB_CONNECTION"];

            services.AddDbContext<ResumesContext>(options => options.UseSqlite(connection,
                   b => b.MigrationsAssembly("StudentsResumes.API")));

        }

        private static void EnableSwagger(IApplicationBuilder app)
        {
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Student Resumes V1");
                c.RoutePrefix = "swagger/resumes";
            });
        }
        #endregion
    }
}

