using StudentResumes.Data.Dto;
using StudentResumes.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentResumes.Data.Repositories
{
    public interface IStudentSkillRepository
    {
        Task<ICollection<StudentSkill>> GetAsync();

        Task<ICollection<StudentSkill>> GetBySkillNameAsync(string name);

        Task<ICollection<StudentSkill>> GetByStudentIdAsync(Guid id);

        Task<ICollection<StudentSkill>> SetSkillsToStudentAsync(Guid studentId, ICollection<string> skillNames);

        Task<ICollection<StudentDto>> SearchBySkillsAsync(ICollection<string> skills);
    }
}
