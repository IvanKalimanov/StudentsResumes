using Microsoft.EntityFrameworkCore;
using StudentResumes.Core.EF;
using StudentResumes.Data.Entities;
using StudentResumes.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentResumes.Core.Repositories
{
    public class RefereeRepository : IRefereeRepository
    {
        private readonly ResumesContext _context;
        private readonly IRefereeRepository _refereeRepository;

        public RefereeRepository(ResumesContext context)
        {
            _context = context;
        }

        public Task<Referee> CreateAsync(Referee referee)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Referee>> GetAsync()
        {
            return await _context.Referees.ToListAsync();
        }

        public Task<Referee> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Referee referee)
        {
            throw new NotImplementedException();
        }
    }
}
