using System;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.BuffDebuffModule
{
    public class PeriodicAction
    {
        public event Action OnFinished;
        
        private int _lifeTimer;
        private int _interval;
        private System.Timers.Timer _timer;
        private ElapsedEventHandler _elapsedEventHandler;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public PeriodicAction(int lifeTimer, int interval, ElapsedEventHandler elapsedEventHandler)
        {
            _lifeTimer = lifeTimer;
            _interval = interval;
            _elapsedEventHandler = elapsedEventHandler;
        }

        public void Start()
        {
            Debug.Log("Start");
            if (_interval > 0)
            {
                _timer = new System.Timers.Timer(_interval);
                _timer.Elapsed += _elapsedEventHandler;
                _timer.Start();

                Task.Delay(_lifeTimer, _cancellationTokenSource.Token).ContinueWith(task =>
                {
                    Stop();
                });
            }
            else
            {
                Task.Delay(_lifeTimer, _cancellationTokenSource.Token).ContinueWith(task =>
                {
                    _elapsedEventHandler.Invoke(null, null);

                    Stop();
                });
            }
        }

        public void Stop()
        {
            Debug.Log("Stop");
            _timer?.Stop();
            _cancellationTokenSource.Cancel();
            OnFinished?.Invoke();
        }           
    }
}