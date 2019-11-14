using Domain.BusinessObject;
using Domain.Providers.Common.Enum;
using Domain.Providers.Configurations.Response.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.Configurations.Response.Concrete
{
    public class GetConfigurationResponse : IGetConfigurationResponse
    {

        public GetConfigurationResponse()
        {
            ProvideResult = ProvideEnumResult.Exception;
        }

        public GetConfigurationResponse(Configuration configuration, long databaseExecutionTime)
        {
            DatabaseExecutionTime = databaseExecutionTime;
            if(configuration == null)
            {
                ProvideResult = ProvideEnumResult.NotFound;
            }
            else
            {
                Configuration = configuration;
                ProvideResult = ProvideEnumResult.Success;
            }
        }

        public Configuration Configuration { get; }

        public ProvideEnumResult ProvideResult { get; }

        public long DatabaseExecutionTime { get; }
    }
}
