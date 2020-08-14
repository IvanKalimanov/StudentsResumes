using System;
using System.Collections.Generic;
using System.Text;

namespace StudentResumes.Data.Settings
{
    public class AppSettings
    {
        public List<string> AcceptedStudentPhotoExtensions { get; set; }

        public List<string> AcceptedResumeExtensions { get; set; }
    }
}
