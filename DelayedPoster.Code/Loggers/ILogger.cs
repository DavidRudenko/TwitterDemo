using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelayedPoster.Code.Loggers
{
    public interface ILogger
    {
        void LogInfo(string message);
        void LogError(string message);
        void LogError(Exception e);
    }
}
