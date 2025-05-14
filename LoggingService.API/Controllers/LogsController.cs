using LoggingService.Application.DTOs;
using LoggingService.Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace LoggingService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogsController : ControllerBase
    {
        private readonly ILogService _logService;

        public LogsController(ILogService logService)
        {
            _logService = logService;
        }

        [HttpPost]
        public async Task<IActionResult> Log(CreateLogDto dto)
        {
            await _logService.CreateLogAsync(dto);
            return Ok("Logged");
        }

        [HttpGet]
        public async Task<IActionResult> GetLogs()
        {
            var logs = await _logService.GetAllAsync(); // يجب أن ترجع LogDto فقط
            return Ok(logs);
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            var testResult = new LogDto
            {
                Id = 1,
                Message = "Test message",
                CreatedBy = "Admin",
                CreatedAt = DateTime.UtcNow
            };

            return Ok(testResult);
        }
    }

}
