using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    public class AsyncTimer
    {
        private readonly Action _callback;
        private readonly int _intervalMilliseconds;
        private CancellationTokenSource _cancellationTokenSource;
        private bool _shouldStopTimer;

        public AsyncTimer(Action callback, int intervalMilliseconds)
        {
            _callback = callback;
            _intervalMilliseconds = intervalMilliseconds;
            _cancellationTokenSource = new CancellationTokenSource();
            _shouldStopTimer = true;
        }

        public void Start()
        {
            _shouldStopTimer = false;
            TimerLoop();
        }

        private async void TimerLoop()
        {
            while (!_shouldStopTimer)
            {
                Debug.Log("ID TimerLoop Thread: " + Thread.CurrentThread.ManagedThreadId);
                //UnityEngine.Application.InvokeOnMainThread(() => _callback.Invoke());
                _callback.Invoke();
                await Task.Delay(_intervalMilliseconds, _cancellationTokenSource.Token);
            }
        }

        public void Stop()
        {
            _shouldStopTimer = true;
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = new CancellationTokenSource();
        }
    }
}