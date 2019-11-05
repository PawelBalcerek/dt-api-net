using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AI_NETCORE_API.Models.Response.ExecutingTimes
{
    public class ExecutionDetails
    {
        public long DatabaseTime { get; set; }
        public long ExecutionTime { get; set; }
    }
}
