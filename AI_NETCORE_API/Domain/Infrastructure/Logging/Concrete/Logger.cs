using Domain.Infrastructure.Logging.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Infrastructure.Logging.Concrete
{
    public class Logger : ILogger
    {
        public void Log(string toLog)
        {
            System.Diagnostics.Debug.Write(toLog);
        }

        public void Log(Exception excetionToLog)
        {
            var log = string.Format("\n[EXCEPTION] {0}\n", excetionToLog);
            System.Diagnostics.Debug.WriteLine(log);
        }
    }
}
