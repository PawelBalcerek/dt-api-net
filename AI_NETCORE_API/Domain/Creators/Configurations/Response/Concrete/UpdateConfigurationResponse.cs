using Domain.Creators.Configurations.Response.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Creators.Configurations.Response.Concrete
{
    public class UpdateConfigurationResponse : IUpdateConfigurationResponse
    {
        public bool Success { get; }
        public long DbTime { get; }

        public UpdateConfigurationResponse(bool success, long dbTime)
        {
            Success = success;
            DbTime = dbTime;
        }

        public UpdateConfigurationResponse()
        {
            Success = false;
        }
    }
}
