using System;
using System.Collections.Generic;
using System.Text;

namespace StudentResumes.Data.Entities
{
    public class StudentSkill
    {
        public Guid StudentId { get; set; }

        public string SkillName { get; set; }

        public Student Student { get; set; }

        public Skill Skill { get; set; }

        public StudentSkill(Guid studentId, string skillName)
        {
            StudentId = studentId;
            SkillName = skillName;
        }
    }
}
