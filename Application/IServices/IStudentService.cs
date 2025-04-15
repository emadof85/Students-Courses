using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDto>> GetAllAsync();
        Task<StudentDto> GetByIdAsync(string id);
        Task CreateAsync(CreateStudentDto studentDto);
        Task UpdateAsync(UpdateStudentDto dto);
        Task DeleteAsync(string id);
        Task EnrollInCourse(string studentId, int courseId);
    }
}
