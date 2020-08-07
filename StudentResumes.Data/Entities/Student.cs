using System;
using System.Collections.Generic;
using System.Text;

namespace StudentResumes.Data.Entities
{
    public class Student
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public byte? CourseNumber { get; set; }

        public string ResumeLink { get; set; }

        public ICollection<StudentSkill> StudentSkills { get; set; } 

        public string UniversityName { get; set; }

        public Guid? RefereeId { get; set; }

        public Referee Referee { get; set; }
    }
}
