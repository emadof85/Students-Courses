using Application.DTOs.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.External
{
    public interface ILoggingClient
    {
        Task SendLogAsync(CreateLogRequest log);
    }
}
