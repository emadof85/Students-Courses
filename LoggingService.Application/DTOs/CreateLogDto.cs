using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggingService.Application.DTOs
{
    public class CreateLogDto
    {
        [Required]
        public string Message { get; set; }
        [Required]
        public string CreatedBy { get; set; }
    }

}
