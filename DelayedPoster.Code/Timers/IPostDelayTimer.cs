using System;

namespace DelayedPoster.Code.Timers
{
    public interface IPostDelayTimer
    {
        TimeSpan Delay { get; set; }

        event EventHandler TimerElapsed;

        void StartTimer();
        void StopTimer();
    }
}