using System;
using System.Timers;
using System.Windows;

namespace TwitterDemo.Code.Timers
{
   public class PostDelayTimer : IPostDelayTimer
    {

        public event EventHandler TimerElapsed;
        private TimeSpan _delay;
        private  Timer _timer;
        public TimeSpan Delay
        {
            get { return _delay; }
            set { _delay = value; }
        }

        /// <summary>
        /// Creates new Timer
        /// </summary>
        /// <param name="delay"></param>
        /// <param name="callback">callback to be executed when the timer will elapse.
        /// Will be executed on the main thread.</param>
        public PostDelayTimer(TimeSpan delay, Action callback)
        {
            if (callback == null) throw new ArgumentNullException(nameof(callback));
            _delay = delay;
            _timer = new Timer(delay.TotalMilliseconds);//runs in a thread from the thread pool
            _timer.Elapsed += (sender, e) =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    callback();
                    _timer.Enabled = false;
                    OnTimerElapsed();
                });
            };
        }
        public void StartTimer()
        {
            _timer.Start();
        }
        protected virtual void OnTimerElapsed()
        {
            var handler = TimerElapsed;
            handler?.Invoke(this, EventArgs.Empty);
        }
    }
}
