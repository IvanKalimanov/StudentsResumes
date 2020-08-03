using StudentResumes.Data.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentResumes.Data.Repositories
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAsync();

        Task<Student> GetByIdAsync(Guid id);

        Task<Student> CreateAsync(Student student);

        Task<bool> UpdateAsync(Student student);

        Task<bool> DeleteAsync(Guid id);
    }
}
