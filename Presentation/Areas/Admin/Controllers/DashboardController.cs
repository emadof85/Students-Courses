using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    [Route("api/[area]/[controller]")]
    public class DashboardController : ControllerBase
    {
        [HttpGet("overview")]
        public IActionResult Index()
        {
            var data = new
            {
                TotalStudents = 100,
                TotalLessons = 25,
                ActiveStudents = 90
            };

            return Ok(data);
        }
    }
}
