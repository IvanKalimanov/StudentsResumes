using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentResumes.API.Models
{
    public class StudentSkillModel
    {
        public Guid StudentId { get; set; }

        public ICollection<string> SkillNames { get; set; }

        public StudentSkillModel(Guid studentId, ICollection<string> skillName)
        {
            StudentId = studentId;
            SkillNames = skillName;
        }
    }
}
