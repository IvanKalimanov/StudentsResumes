using System;
using System.Collections.Generic;
using System.Text;

namespace StudentResumes.Data.Dto
{
    public class StudentDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public byte? CourseNumber { get; set; }

        public string ResumeLink { get; set; }

        public ICollection<string> Skills { get; set; }

        public string UniversityName { get; set; }

        public Guid? RefereeId { get; set; }

    }
}
