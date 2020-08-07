using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentResumes.API.Models
{
    public class RefereeCreatingModel
    {
        public string Name { get; set; }

        public string WorkPosition { get; set; }

        public RefereeCreatingModel(string name, string workPosition)
        {
            Name = name;
            WorkPosition = workPosition;
        }
    }
}
