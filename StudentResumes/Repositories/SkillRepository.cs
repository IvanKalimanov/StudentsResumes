using Microsoft.EntityFrameworkCore;
using StudentResumes.Core.EF;
using StudentResumes.Core.Exceptions;
using StudentResumes.Data.Entities;
using StudentResumes.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentResumes.Core.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        private readonly ResumesContext _context;

        public SkillRepository(ResumesContext context)
        {
            _context = context;
        }

        public async Task<Skill> CreateAsync(string name)
        {
            var result = await _context.Skills.AddAsync(new Skill(name));
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> DeleteAsync(string name)
        {
            var skill = await _context.Skills.FirstOrDefaultAsync(x => x.Name == name);
            if (skill == null)
                throw new EntityNotFoundException();

            _context.Skills.Remove(skill);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<string>> GetAsync()
        {
            return await _context.Skills.Select(x => x.Name).ToListAsync();
        }
    }
}
