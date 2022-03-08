using System;
using System.Collections.Generic;
using System.Text;

namespace Helper.Logging
{
    public interface ILoggerManager
    {
        //log levels
        void LogInfo(string message);
        void LogWarn(string message);
        void LogDebug(string message);
        void LogError(string message);
    }
}
