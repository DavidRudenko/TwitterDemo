using System;

namespace TwitterDemo.Code.Loggers
{
    public class DebugConsoleLogger:ILogger
    {
        public void LogInfo(string message)
        {
            Console.WriteLine(message);//Debug.WriteLine(message);
                
        }

        public void LogError(string message)
        {
            Console.WriteLine(message);//Debug.Fail(message);
        }

        public void LogError(Exception e)
        {
            Console.WriteLine($"CAUGHT EXCEPTION AT {DateTime.Now}: \n" +
                       $" MESSAGE: \n {e.Message} \n" +
                       $" STACKTRACE: \n {e.StackTrace} ");
        }
    }
}
