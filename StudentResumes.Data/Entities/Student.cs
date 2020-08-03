using System;
using System.Collections.Generic;
using System.Text;

namespace StudentResumes.Data.Entities
{
    public class Student
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Surename { get; set; }

        public byte CourseNumber { get; set; }

        public string ResumeLink { get; set; }

        public List<Skill> Skills { get; set; } = new List<Skill>();

        public string UniversityName { get; set; }
    }
}
