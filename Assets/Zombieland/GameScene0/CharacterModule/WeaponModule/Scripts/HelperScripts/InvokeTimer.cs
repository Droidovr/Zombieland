using System;
using System.Timers;

namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    public class InvokeTimer : IFireTimer
    {
        private Timer _timer;
        private Action _invokeMethod;

        public InvokeTimer(float delayTime, Action invokeMethod)
        {
            _invokeMethod = invokeMethod;

            int intervalMS = (int)(delayTime * 1000);
            _timer = new Timer(intervalMS);
            _timer.SynchronizingObject = null;
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