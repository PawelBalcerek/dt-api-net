using Domain.Updaters.Configurations.Abstract;
using Domain.Updaters.Configurations.Request.Abstract;
using Domain.Updaters.Configurations.Response.Abstract;
using Domain.Updaters.Configurations.Response.Concrete;
using Domain.Infrastructure.Logging.Abstract;
using Domain.Repositories.ConfigurationRepo.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Updaters.Configurations.Concrete
{
    public class ConfigurationUpdater : IConfigurationUpdater
    {
        private readonly ILogger _logger;
        private readonly IConfigurationRepository _configuationRepository;

        public ConfigurationUpdater(ILogger logger, IConfigurationRepository configurationRepository)
        {
            _logger = logger;
            _configuationRepository = configurationRepository;
        }

        public IUpdateConfigurationResponse UpdateConfiguration(IUpdateConfigurationRequest configurationUpdateRequest)
        {
            try
            {
                var result =_configuationRepository.UpdateConfiguration(configurationUpdateRequest.Name, configurationUpdateRequest.Value);
                return new UpdateConfigurationResponse(result.Object, result.DatabaseTime);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return new UpdateConfigurationResponse();
            }
        }
    }
}
