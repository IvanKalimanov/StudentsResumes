﻿using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentResumes.Data.Entities
{
    public class User : IdentityUser<Guid>
    {
    }
}
