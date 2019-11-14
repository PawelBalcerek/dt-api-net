using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.Configurations.Request.Abstract
{
    public interface IGetConfigurationRequest
    {
        string ConfigurationName { get; }
    }
}
