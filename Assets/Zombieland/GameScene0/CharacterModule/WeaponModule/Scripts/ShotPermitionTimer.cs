using System;
using System.Timers;

namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    public class ShotPermitionTimer
    {
        public event Action OnShotPermition;

        private int _interval;
        private Timer _timer;
        private Func<bool> _checkPermission;

        public ShotPermitionTimer(float interval, Func<bool> checkPermission)
        {
            _interval = (int)(interval * 1000);
            _checkPermission = checkPermission ?? throw new ArgumentNullException(nameof(checkPermission));
            _timer = new Timer(_interval);
            _timer.Elapsed += HandleTimerElapsed;
            _timer.Start();
        }

        private void HandleTimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (_checkPermission.Invoke())
            {
                OnShotPermition?.Invoke();
            }
        }

        public void Stop()
        {
            _timer?.Stop();
        }
    }
}
