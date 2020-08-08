using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentResumes.Data
{
    public class Logger
    {
        public static ILogger Log { get; private set; }

        public static void RegisterLogger(ILogger logger)
        {
            Log = logger;
        }
    }
}
