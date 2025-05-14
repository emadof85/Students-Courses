using AutoMapper;
using LoggingService.Application.DTOs;
using LoggingService.Application.IServices;
using LoggingService.Domain.Entities;
using LoggingService.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggingService.Application.Services
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepository;
        private readonly IMapper _mapper;

        public LogService(ILogRepository logRepository, IMapper mapper)
        {
            _logRepository = logRepository;
            _mapper = mapper;
        }

        public async Task CreateLogAsync(CreateLogDto dto)
        {
            var log = new Log
            {
                Message = dto.Message,
                CreatedBy = dto.CreatedBy
            };

            await _logRepository.AddAsync(log);
        }

        public async Task<IEnumerable<LogDto>> GetAllAsync()
        {
            var logs = await _logRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<LogDto>>(logs);
        }

    }

}
