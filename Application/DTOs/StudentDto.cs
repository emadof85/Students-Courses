using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class StudentDto
    {
        public string Id { get; set; }
        public string FullName { get; set; }  // Combined first and last names.
        public string Email { get; set; }
        public int Age { get; set; }
        public string StudentNumber { get; set; }
        public bool IsActive { get; set; }
    }
}
