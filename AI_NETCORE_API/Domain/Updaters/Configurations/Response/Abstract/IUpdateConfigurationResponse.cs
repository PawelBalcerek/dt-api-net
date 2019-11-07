using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Updaters.Configurations.Response.Abstract
{
    public interface IUpdateConfigurationResponse
    {
        bool Success { get; }
        long DbTime { get; }
    }
}
