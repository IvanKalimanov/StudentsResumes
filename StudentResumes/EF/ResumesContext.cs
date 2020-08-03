using Microsoft.EntityFrameworkCore;
using StudentResumes.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentResumes.Core.EF
{
    public class ResumesContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public DbSet<Referee> Referees { get; set; }

        public DbSet<Skill> Skills { get; set; }

        public ResumesContext(DbContextOptions<ResumesContext> opt) : base(opt)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skill>().HasKey(key => new { key.Name });

            base.OnModelCreating(modelBuilder);
        }
    }
}
