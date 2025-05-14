using Application.DTOs.Log;
using Application.External;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ExternalClients
{
    public class LoggingClient : ILoggingClient
    {
        private readonly HttpClient _httpClient;

        public LoggingClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task SendLogAsync(CreateLogRequest log)
        {
            var response = await _httpClient.PostAsJsonAsync("api/logs", log);
            response.EnsureSuccessStatusCode();
        }
    }

}
