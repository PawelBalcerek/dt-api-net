using System;
using System.Collections.Generic;
using System.Text;
using Domain.Updaters.Configurations.Request.Abstract;
using Domain.Updaters.Configurations.Response.Abstract;

namespace Domain.Updaters.Configurations.Abstract
{
    public interface IConfigurationUpdater
    {
        IUpdateConfigurationResponse UpdateConfiguration(IUpdateConfigurationRequest configurationUpdateRequest);
    }
}
