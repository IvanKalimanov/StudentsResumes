using Microsoft.EntityFrameworkCore;
using StudentResumes.Core.EF;
using StudentResumes.Data.Converters;
using StudentResumes.Data.Dto;
using StudentResumes.Data.Entities;
using StudentResumes.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentResumes.Core.Repositories
{
    public class StudentSkillRepository : IStudentSkillRepository
    {
        private readonly ResumesContext _context;

        public StudentSkillRepository(ResumesContext context)
        {
            _context = context;
        }

        public async Task<ICollection<StudentSkill>> GetAsync()
        {
            return await _context.StudentSkills.Include(x => x.Student).ToListAsync();
        }

        public async Task<ICollection<StudentSkill>> GetBySkillNameAsync(string name)
        {
            return await _context.StudentSkills.Where(x => x.SkillName == name).Include(x => x.Student).ToListAsync();
        }

        public async Task<ICollection<StudentSkill>> GetByStudentIdAsync(Guid id)
        {
            return await _context.StudentSkills.Where(x => x.StudentId == id).ToListAsync();
        }

        public async Task<ICollection<StudentSkill>> SetSkillsToStudentAsync(Guid studentId, ICollection<string> skillNames)
        {
            ICollection<StudentSkill> result = new List<StudentSkill>();
            foreach (var name in skillNames)
            {
                var studentSkill = await CreateStudentSkill(studentId, name);
                result.Add(studentSkill);
            }
            await _context.StudentSkills.AddRangeAsync(result);
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<ICollection<StudentDto>> SearchBySkillsAsync(ICollection<string> skills)
        {
            List<SearchingEntity> searchingEntities = new List<SearchingEntity>();

            var all = await _context.StudentSkills
                .Include(x => x.Student)
                .Where(ss => skills.Contains(ss.SkillName))
                .ToListAsync();

            foreach (var x in all)
            {
                var s = searchingEntities.FirstOrDefault(y => y.Student == x.Student);

                if (s == null)
                    searchingEntities.Add(new SearchingEntity(x.Student));
                else s.SkillCounter += 1;
            }

            return StudentConverter.Convert(searchingEntities
                                                                .OrderByDescending(x => x.SkillCounter)
                                                                .Select(x => x.Student)
                                                                .ToList());
        }

        private async Task<StudentSkill> CreateStudentSkill(Guid studentId, string skillName)
        {
            var result = await _context.StudentSkills.AddAsync(
                new StudentSkill(studentId, skillName));
            return result.Entity;
        }

        private class SearchingEntity
        {
            public Student Student { get; set; }

            public int SkillCounter { get; set; }

            public SearchingEntity(Student student)
            {
                Student = student;
                SkillCounter = 0;
            }
        }

    }

}
