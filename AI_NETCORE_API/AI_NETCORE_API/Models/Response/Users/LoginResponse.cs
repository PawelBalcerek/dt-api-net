using AI_NETCORE_API.Models.Response.ExecutingTimes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AI_NETCORE_API.Models.Response.Users
{
    public class LoginResponse
    {
        /// <summary>
        /// Token
        /// </summary>
        [JsonProperty("jwt")]
        public string Token { get; set; }
        public ExecutionDetails ExecDetails { get; set; }
    }
}
