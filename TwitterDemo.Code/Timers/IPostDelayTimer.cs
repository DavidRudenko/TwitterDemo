using System;

namespace TwitterDemo.Code.Timers
{
    public interface IPostDelayTimer
    {
        TimeSpan Delay { get; set; }

        event EventHandler TimerElapsed;
    }
}