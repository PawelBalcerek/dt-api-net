using Domain.Creators.Configurations.Abstract;
using Domain.Creators.Configurations.Request.Abstract;
using Domain.Creators.Configurations.Response.Abstract;
using Domain.Creators.Configurations.Response.Concrete;
using Domain.Infrastructure.Logging.Abstract;
using Domain.Repositories.ConfigurationRepo.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Creators.Configurations.Concrete
{
    public class ConfigurationCreator : IConfigurationCreator
    {
        private readonly ILogger _logger;
        private readonly IConfigurationRepository _configuationRepository;

        public ConfigurationCreator(ILogger logger, IConfigurationRepository configurationRepository)
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
