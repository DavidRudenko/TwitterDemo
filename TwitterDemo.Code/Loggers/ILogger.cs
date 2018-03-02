using System;

namespace TwitterDemo.Code.Loggers
{
    public interface ILogger
    {
        void LogInfo(string message);
        void LogError(string message);
        void LogError(Exception e);
    }
}
