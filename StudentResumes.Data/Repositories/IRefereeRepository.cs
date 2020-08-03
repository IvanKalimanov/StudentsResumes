using StudentResumes.Data.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentResumes.Data.Repositories
{
    public interface IRefereeRepository
    {
        Task<IEnumerable<Referee>> GetAsync();

        Task<Referee> GetByIdAsync(Guid id);

        Task<Referee> CreateAsync(Referee referee);

        Task<bool> UpdateAsync(Referee referee);

        Task<bool> DeleteAsync(Guid id);
    }
}
