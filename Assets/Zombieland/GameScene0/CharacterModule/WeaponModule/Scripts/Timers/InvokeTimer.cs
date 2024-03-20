using System;
using System.Timers;

namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    public class InvokeTimer
    {
        private Timer _timer;
        private Action _invokeMethod;

        public InvokeTimer(float interval, Action invokeMethod)
        {
            _invokeMethod = invokeMethod;

            int intervalMS = (int)(interval * 1000);
            _timer = new Timer(intervalMS);
        }

        public void Start()
        {
            _timer.Elapsed += HandleTimerElapsed;
            _timer.Start();
        }

        public void Stop()
        {
            _timer?.Stop();
            _timer.Elapsed -= HandleTimerElapsed;
        }

        private void HandleTimerElapsed(object sender, ElapsedEventArgs e)
        {
            _invokeMethod?.Invoke();
            Stop();
        }
    }
}