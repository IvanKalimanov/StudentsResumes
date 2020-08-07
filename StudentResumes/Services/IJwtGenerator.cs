using StudentResumes.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentResumes.AUTH
{
    public interface IJwtGenerator
    {
        Task<object> GenerateJwt(User user);
    }
}
