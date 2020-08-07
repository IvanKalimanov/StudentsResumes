using System;
using System.Collections.Generic;
using System.Text;

namespace StudentResumes.Data.Dto
{
    public class RefereeDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string WorkPosition { get; set; }

        public RefereeDto(string name, string workPosition)
        {
            Name = name;
            WorkPosition = workPosition;
        }
    }
}

