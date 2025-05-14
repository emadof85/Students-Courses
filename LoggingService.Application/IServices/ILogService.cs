using LoggingService.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggingService.Application.IServices
{
    public interface ILogService
    {
        Task CreateLogAsync(CreateLogDto dto);
        public Task<IEnumerable<LogDto>> GetAllAsync();
    }

}
