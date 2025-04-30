using Application.DTOs;
using Application.IServices;
using Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [ApiController]
    [Route("api/[area]/[controller]")]
    [Authorize]
    public class StudentsController : ControllerBase    
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await _studentService.GetAllAsync();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var student = await _studentService.GetByIdAsync(id);
            if (student == null) return NotFound();
            return Ok(student);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStudentDto dto)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var role = HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;

            if (!ModelState.IsValid)
                return BadRequest(ModelState); // Return a JSON Error messages
            var createdStudent = await _studentService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdStudent.Id }, createdStudent);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateStudentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedStudent = await _studentService.UpdateAsync(dto);
            if (updatedStudent == null)
                return NotFound();

            return Ok(updatedStudent);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _studentService.DeleteAsync(id);
            return NoContent();
        }
    }
}
