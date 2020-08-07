using System;
using System.Collections.Generic;
using System.Text;

namespace StudentResumes.Data.Entities
{
    public class Referee
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string WorkPosition { get; set; }

        public ICollection<Student> Students {get; set;}
    }
}
