using StudentResumes.Data.Dto;
using StudentResumes.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentResumes.Data.Converters
{
    public class RefereeConverter
    {
        public static Referee Convert(RefereeDto refereeDto) =>
            new Referee()
            {
                Name = refereeDto.Name,
                WorkPosition = refereeDto.WorkPosition
            };
    }
}
