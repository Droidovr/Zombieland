using System;
using System.Timers;

namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    public class ShotTimer
    {
        public event Action OnPermission;

        private Timer _timer;
        private Func<bool> _checkPermission;

        public ShotTimer(float interval, Func<bool> checkPermission)
        {
            _checkPermission = checkPermission;

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
            bool isPermission = _checkPermission.Invoke();

            if (isPermission)
            {
                OnPermission?.Invoke();
            }
        }
    }
}