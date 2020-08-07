using System;
using System.Collections.Generic;
using System.Text;

namespace StudentResumes.Data.Settings
{
    public  class EnvironmentConfig
    {
        public string DB_CONNECTION { get; set; }

        public string REDIS_CONNECTION { get; set; }

        public string STORAGE_PATH { get; set; }
    }
}
