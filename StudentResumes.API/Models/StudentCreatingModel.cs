using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentResumes.API.Models
{
    public class StudentCreatingModel
    {
        public string Name { get; set; }

        public byte? CourseNumber { get; set; }

        public string ResumeLink { get; set; }

        public ICollection<string> Skills { get; set; }

        public string UniversityName { get; set; }

        public Guid? RefereeId { get; set; }
    }
}
