using AI_NETCORE_API.Models.Response.ExecutingTimes;
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
        public string Token { get; set; }
        public ExecutionDetails ExecDetails { get; set; }
    }
}
