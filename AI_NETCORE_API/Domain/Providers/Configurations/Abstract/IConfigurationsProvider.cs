using Domain.Providers.Configurations.Request.Abstract;
using Domain.Providers.Configurations.Response.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.Configurations.Abstract
{
    public interface IConfigurationsProvider
    {
        IGetConfigurationResponse GetConfiguration(IGetConfigurationRequest getConfigurationRequest);
    }
}
