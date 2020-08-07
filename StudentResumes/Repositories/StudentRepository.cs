using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using StudentResumes.Core.EF;
using StudentResumes.Core.Services;
using StudentResumes.Data.Converters;
using StudentResumes.Data.Dto;
using StudentResumes.Data.Entities;
using StudentResumes.Data.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentResumes.Core.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ResumesContext _context;
        private readonly IRefereeRepository _refereeRepository;
        private readonly IStorageService _storageService;

        public StudentRepository(ResumesContext context, IRefereeRepository refereeRepository, IStorageService storageService)
        {
            _context = context;
            _refereeRepository = refereeRepository;
            _storageService = storageService;
        }

        public async Task<StudentDto> CreateAsync(StudentDto studentDto)
        {
            var student = StudentConverter.Convert(studentDto);

            var result = await _context.Students.AddAsync(student);
            
            foreach(var skillName in studentDto.Skills)
            {
                var sk = await _context.Skills.FindAsync(skillName);
                if (sk != null)
                    await _context.StudentSkills.AddAsync(new StudentSkill(student.Id, sk.Name));
            }
            if (student.RefereeId != null)
            {
                var referee = await _refereeRepository.GetByIdAsync((Guid)student.RefereeId);
                if (referee != null)
                    student.Referee = referee;
            }
            await _context.SaveChangesAsync();
            return StudentConverter.Convert(result.Entity);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
                return false;
 
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<StudentDto>> GetAsync()
        {
            return StudentConverter.Convert(await _context.Students.Include(x => x.StudentSkills).ToListAsync());
        }

        public async Task<StudentDto> GetByIdAsync(Guid id)
        {
            return StudentConverter.Convert(await _context.Students.FirstOrDefaultAsync(x => x.Id == id));
        }

        public async Task<bool> UpdateAsync(StudentDto studentDto)
        {
            var student = await _context.Students.FindAsync(studentDto.Id);

            if (student == null)
                return false;

            student.Name = studentDto.Name;
            student.RefereeId = studentDto.RefereeId;
            student.ResumeLink = studentDto.ResumeLink;
            student.UniversityName = studentDto.UniversityName;
            student.CourseNumber = studentDto.CourseNumber;

            foreach (var skillName in studentDto.Skills)
            {
                if (await _context.Skills.FindAsync(skillName) != null)
                {
                    if (await _context.StudentSkills.FindAsync(student.Id, skillName) == null)
                        await _context.StudentSkills.AddAsync(new StudentSkill(student.Id, skillName));
                }
                        
            }

            if (student.RefereeId != null)
            {
                var referee = await _refereeRepository.GetByIdAsync((Guid)student.RefereeId);
                if (referee != null)
                    student.Referee = referee;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UploadResumeFileAsync(IFormFile file, Guid studentId)
        {
            var student = await _context.Students.FindAsync(studentId);

            if (student == null)
                return false;

            if (student.ResumeLink != null)
            {
                _storageService.RemoveFileByFullPath(student.ResumeLink);
                student.ResumeLink = null;
            }
            var filename = student.Name + "Resume" + Path.GetExtension(file.FileName);

            var link = await _storageService.UploadAsync(file, filename, "/resumes/");

            student.ResumeLink = link;

            return true;
        }

        public async Task<ICollection<StudentDto>> SearchByNameAsync(string name)
        {
            name = name.ToLower();

            var result = await _context.Students.Where(x => x.Name.ToLower().Contains(name)).ToListAsync();

            return StudentConverter.Convert(result);
        }
    }
}
