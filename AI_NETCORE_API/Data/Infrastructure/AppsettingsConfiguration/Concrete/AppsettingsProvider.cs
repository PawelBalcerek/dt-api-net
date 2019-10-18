using Data.Infrastructure.AppsettingsConfiguration.Abstract;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Infrastructure.AppsettingsConfiguration.Concrete
{
    public class AppsettingsProvider : IAppsettingsProvider
    {
        private readonly IConfiguration _configuration;

        public AppsettingsProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetPasswordRegex()
        {
            return _configuration.GetSection("PasswordRegex").Value;
        }
    }
}
