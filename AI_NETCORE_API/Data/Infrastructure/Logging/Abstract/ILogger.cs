using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Infrastructure.Logging.Abstract
{
    public interface ILogger
    {
        void Log(string toLog);
        void Log(Exception excetionToLog);

    }
}
