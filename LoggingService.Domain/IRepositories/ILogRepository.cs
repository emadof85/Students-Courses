using LoggingService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggingService.Domain.IRepositories
{
    public interface ILogRepository
    {
        Task AddAsync(Log log);
        Task<IEnumerable<Log>> GetAllAsync();
    }

}
