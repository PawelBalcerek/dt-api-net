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
            //throw new NotImplementedException();
        }

        public void Log(Exception excetionToLog)
        {
            //throw new NotImplementedException();
        }
    }
}
