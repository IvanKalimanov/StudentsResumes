using StudentResumes.Data.Dto;
using StudentResumes.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentResumes.Data.Converters
{
    public class StudentConverter
    {
        public static Student Convert(StudentDto studentDto) =>
            new Student()
            {
                Id = studentDto.Id,
                ResumeLink = studentDto.ResumeLink,
                RefereeId = studentDto.RefereeId,
                Name = studentDto.Name,
                CourseNumber = studentDto.CourseNumber,
                UniversityName = studentDto.UniversityName
            };

        public static StudentDto Convert(Student student) =>
            new StudentDto()
            {
                Id = student.Id,
                ResumeLink = student.ResumeLink,
                RefereeId = student.RefereeId,
                Name = student.Name,
                CourseNumber = student.CourseNumber,
                UniversityName = student.UniversityName,
                Skills = student.StudentSkills?.Select(x => x?.SkillName).ToList(),
           
            };

        public static ICollection<StudentDto> Convert(ICollection<Student> students) =>
            students.Select(x => Convert(x)).ToList();
    }
}
