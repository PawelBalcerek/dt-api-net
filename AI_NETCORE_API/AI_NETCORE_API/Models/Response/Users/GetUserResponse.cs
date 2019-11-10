using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AI_NETCORE_API.Models.Objects;
using AI_NETCORE_API.Models.Response.ExecutingTimes;

namespace AI_NETCORE_API.Models.Response.Users
{
    public class GetUserResponse
    {
        public UserModel user { get; set; }
        public ExecutionDetails execDetails { get; set; }
    }
}
