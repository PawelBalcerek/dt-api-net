using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AI_NETCORE_API.Models.Objects;
using AI_NETCORE_API.Models.Response.ExecutingTimes;

namespace AI_NETCORE_API.Models.Response.Resources
{
    public class GetUserResourcesResponseModel
    {
        public IList<ResourceModel> Resources { get; set; }
        public ExecutionDetails ExecutionDetails { get; set; }
    }
}
