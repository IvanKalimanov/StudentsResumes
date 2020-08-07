using StudentResumes.Data.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentResumes.Data.Repositories
{
    public interface ISkillRepository
    {
        Task<IEnumerable<string>> GetAsync();

        Task<Skill> CreateAsync(string name);

        Task<bool> DeleteAsync(string name);
    }
}
