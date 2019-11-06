using System;
using System.Collections.Generic;
using System.Text;
using Domain.Creators.Configurations.Request.Abstract;
using Domain.Creators.Configurations.Response.Abstract;

namespace Domain.Creators.Configurations.Abstract
{
    public interface IConfigurationCreator
    {
        IUpdateConfigurationResponse UpdateConfiguration(IUpdateConfigurationRequest configurationUpdateRequest);
    }
}
