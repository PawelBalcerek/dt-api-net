using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AI_NETCORE_API.Models.Response.ExecutingTimes
{
    public class ExecutionDetails
    {
        public long? DbTime { get; set; }
        public long? ExecTime { get; set; }
    }
}