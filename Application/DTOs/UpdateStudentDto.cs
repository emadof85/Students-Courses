using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class UpdateStudentDto
    {
        public string Id { get; set; }
        public string PhoneNumber { get; set; }
        public string StudentNumber { get; set; }
        public int Age { get; set; }
        public bool IsActive { get; set; }
    }
}
