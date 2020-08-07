using Microsoft.AspNetCore.Http;
using StudentResumes.Data.Dto;
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
        Task<IEnumerable<StudentDto>> GetAsync();

        Task<StudentDto> GetByIdAsync(Guid id);

        Task<StudentDto> CreateAsync(StudentDto studentDto);

        Task<bool> UpdateAsync(StudentDto studentDto);

        Task<bool> DeleteAsync(Guid id);

        Task<bool> UploadResumeFileAsync(IFormFile file, Guid studentId);

        Task<ICollection<StudentDto>> SearchByNameAsync(string name);
    }
}
