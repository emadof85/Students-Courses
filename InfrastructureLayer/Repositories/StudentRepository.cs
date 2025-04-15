using Domain.Entities;
using Domain.IRepositories;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class StudentRepository : Repository<Student>,   IStudentRepository
    {
        public StudentRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Student> GetByStudentNumberAsync(string studentNumber)
        {
            return await _context.Students.FirstOrDefaultAsync(s => s.StudentNumber == studentNumber);
        }
    }
}
