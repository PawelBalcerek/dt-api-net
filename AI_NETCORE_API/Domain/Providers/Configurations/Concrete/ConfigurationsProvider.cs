using Domain.BusinessObject;
using Domain.Infrastructure.Logging.Abstract;
using Domain.Providers.Configurations.Abstract;
using Domain.Providers.Configurations.Request.Abstract;
using Domain.Providers.Configurations.Response.Abstract;
using Domain.Providers.Configurations.Response.Concrete;
using Domain.Repositories.BaseRepo.Response;
using Domain.Repositories.ConfigurationRepo.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.Configurations.Concrete
{
    public class ConfigurationsProvider : IConfigurationsProvider
    {
        private readonly ILogger _logger;
        private readonly IConfigurationRepository _configurations;

        public ConfigurationsProvider(ILogger logger, IConfigurationRepository configurations)
        {
            _logger = logger;
            _configurations = configurations;
        }
        public IGetConfigurationResponse GetConfiguration(IGetConfigurationRequest getConfigurationRequest)
        {
            try
            {
                RepositoryResponse<Configuration> response = _configurations.GetConfiguration(getConfigurationRequest.ConfigurationName);
                return new GetConfigurationResponse(response.Object, response.DatabaseTime);
            } catch(Exception ex)
            {
                _logger.Log(ex);
                return new GetConfigurationResponse();
            }
        }
    }
}
