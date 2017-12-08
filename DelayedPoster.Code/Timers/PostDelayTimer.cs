using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DelayedPoster.Code.Annotations;
using Microsoft.Win32;

namespace DelayedPoster.Code.Timers
{
   public class PostDelayTimer : IPostDelayTimer
    {

        public event EventHandler TimerElapsed;
        private TimeSpan _delay;
        private readonly Timer _timer;
        public TimeSpan Delay
        {
            get { return _delay; }
            set { _delay = value; }
        }


        public PostDelayTimer(TimeSpan delay, Action callback)
        {
            if (callback == null) throw new ArgumentNullException(nameof(callback));
            _delay = delay;
            _timer = new Timer(state =>
            {
                callback();
                OnTimerElapsed();
            }, null, _delay, TimeSpan.Zero);
        }

        /// <summary>
        /// Starts timer
        /// </summary>
        public void StartTimer()
        {
            _timer.Change(TimeSpan.Zero,_delay);
        }

        /// <summary>
        /// Stops timer
        /// </summary>
        public void StopTimer()
        {
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        protected virtual void OnTimerElapsed()
        {
            var handler = TimerElapsed;
            handler?.Invoke(this, EventArgs.Empty);
        }

    }
}
