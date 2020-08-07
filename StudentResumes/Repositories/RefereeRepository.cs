using Microsoft.EntityFrameworkCore;
using StudentResumes.Core.EF;
using StudentResumes.Core.Exceptions;
using StudentResumes.Data.Converters;
using StudentResumes.Data.Dto;
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

        public RefereeRepository(ResumesContext context)
        {
            _context = context;
        }

        public async Task<Referee> CreateAsync(RefereeDto refereeDto)
        {
            var referee = RefereeConverter.Convert(refereeDto);
            var result = await _context.Referees.AddAsync(referee);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            Referee referee = await _context.Referees.FindAsync(id);

            if (referee == null)
                throw new EntityNotFoundException();

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Referee>> GetAsync()
        {
            return await _context.Referees.Include(x => x.Students).ToListAsync();
        }

        public async Task<Referee> GetByIdAsync(Guid id)
        {
            var referee = await _context.Referees.Include(x => x.Students).FirstOrDefaultAsync(x => x.Id == id);

            if (referee == null)
                throw new EntityNotFoundException();

            return referee;
        }

        public async Task<bool> UpdateAsync(RefereeDto referee)
        {
            var oldReferee = await _context.Referees.FindAsync(referee.Id);

            if (oldReferee == null)
                throw new EntityNotFoundException();

            oldReferee.Name = referee.Name;
            oldReferee.WorkPosition = referee.WorkPosition;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
