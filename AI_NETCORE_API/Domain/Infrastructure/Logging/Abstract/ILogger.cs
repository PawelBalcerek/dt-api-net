using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Infrastructure.Logging.Abstract
{
    public interface ILogger
    {
        void Log(string toLog);
        void Log(Exception excetionToLog);

    }
}
