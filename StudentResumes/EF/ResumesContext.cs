using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentResumes.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentResumes.Core.EF
{
    public class ResumesContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DbSet<Student> Students { get; set; }

        public DbSet<Referee> Referees { get; set; }

        public DbSet<Skill> Skills { get; set; }

        public DbSet<StudentSkill> StudentSkills { get; set; }

        public ResumesContext(DbContextOptions<ResumesContext> opt) : base(opt)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole<Guid>>().HasData(
                new IdentityRole<Guid>[]
                {
                    new IdentityRole<Guid>
                    {
                        Id = Guid.NewGuid(),
                        Name = "admin",
                        NormalizedName = "ADMIN"
                    }
                });

            modelBuilder.Entity<Skill>().HasKey(key => new { key.Name });

            modelBuilder.Entity<StudentSkill>().HasKey(key => new { key.StudentId, key.SkillName });

            modelBuilder.Entity<Student>()
                .HasOne(x => x.Referee)
                .WithMany(x => x.Students)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<StudentSkill>()
                .HasOne(x => x.Student)
                .WithMany(x => x.StudentSkills)
                .HasForeignKey(x => x.StudentId);

            modelBuilder.Entity<StudentSkill>()
                .HasOne(x => x.Skill)
                .WithMany(x => x.StudentSkills)
                .HasForeignKey(x => x.SkillName);
            
            


            base.OnModelCreating(modelBuilder);
        }
    }
}
