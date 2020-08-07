using System;
using System.Collections.Generic;
using System.Text;

namespace StudentResumes.Data.Entities
{
    public class Skill
    {
        public string Name { get; set; }

        public ICollection<StudentSkill> StudentSkills { get; set; }

        public Skill(string name)
        {
            Name = name;
        }
    }
}
