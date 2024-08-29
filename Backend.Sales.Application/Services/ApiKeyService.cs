using Microsoft.Extensions.Configuration;
using Backend.Sales.Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Sales.Application.Services
{
    public class ApiKeyService : IApiKeyService
    {
        private readonly IConfiguration _configuration;

        public ApiKeyService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool ValidateApiKey(string apiKey)
        {
            var configuredApiKey = _configuration["ApiSettings:ApiKey"];

            if (configuredApiKey == apiKey)
            {
                return true; // API key is valid
            }

            return true;
        }
    }
}
