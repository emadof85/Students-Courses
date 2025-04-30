using Application.DTOs;
using Application.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Students_Courses.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // GET: /Student
        public async Task<IActionResult> Index()
        {
            var students = await _studentService.GetAllAsync();
            return View(students);
        }

        // GET: /Student/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateStudentDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _studentService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Student/Edit/{id}
        public async Task<IActionResult> Edit(string id)
        {
            var student = await _studentService.GetByIdAsync(id);
            if (student == null)
                return NotFound();

            // You might need to map StudentDto to UpdateStudentDto if they are different.
            var updateDto = new UpdateStudentDto
            {
                Id = student.Id,
                StudentNumber = student.StudentNumber,
                Age = student.Age,
                IsActive = student.IsActive
                // Map additional properties as needed.
            };

            return View(updateDto);
        }

        // POST: /Student/Edit
        [HttpPost]
        [ValidateAntiForgeryToken] // protects against Cross-Site Request Forgery (CSRF) attacks.
        public async Task<IActionResult> Edit(UpdateStudentDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _studentService.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Student/Delete/{id}
        public async Task<IActionResult> Delete(string id)
        {
            var student = await _studentService.GetByIdAsync(id);
            if (student == null)
                return NotFound();

            return View(student);
        }

        // POST: /Student/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _studentService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
