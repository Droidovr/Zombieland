using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Zombieland
{
    public class DependencyTracker : IDependencyTracker
    {
        public event Action<string> OnReady;

        private readonly IController _parentController;
        private List<IController> _requiredControllers;
        private List<IController> _notPreparedRequiredControllers;
        private int _counter;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private const int ControlTime = 100; //in millisecond


        public DependencyTracker(IController parentController, List<IController> requiredControllers)
        {
            _parentController = parentController;
            _requiredControllers = requiredControllers ?? new List<IController>();
        }

        public void Init()
        {
            Debug.Log($"<color=gray>{_parentController.GetType().FullName} Init!</color>");
            TrackRequiredControllerCreation();
        }

        public void Deinit()
        {
            _cancellationTokenSource.Cancel();
            for (int i = 0; i < _notPreparedRequiredControllers.Count; i++)
            {
                _notPreparedRequiredControllers[i].OnReady -= OnRequiredControllerReadyHandler;
            }
        }

        private void TrackRequiredControllerCreation()
        {
            Task.Delay(ControlTime, _cancellationTokenSource.Token).ContinueWith(task => {
                if (!task.IsCanceled)
                {
                    OnPassedTimeHandler();
                }
            });
        }

        private void OnPassedTimeHandler()
        {
            _cancellationTokenSource.Cancel();
            // Debug.Log($"<color=gray>{_parentController.GetType().FullName}  OnTimedEvent.</color>");
            _counter = _requiredControllers.Count;
            for (int i = 0; i < _requiredControllers.Count; i++)
            {
                if (_requiredControllers[i] != null)
                {
                    _counter--;
                     // Debug.Log($"<color=gray>{_parentController.GetType().Name} Controller {_requiredControllers[i].GetType().Name} != null = {_requiredControllers[i] != null}  Counter = {_counter}!</color>");
                }
                else
                {
                    // Debug.Log($"<color=gray>{_parentController.GetType().Name} Controller {_requiredControllers[i].GetType().Name} != null = {_requiredControllers[i] != null}  Counter = {_counter}!</color>");
                }
            }
            Debug.Log($"<color=gray>{_parentController.GetType().FullName}  OnTimedEvent. Counter of unprepared controllers = {_counter}!</color>");
            if (_counter == 0)
            {
                Debug.Log($"<color=gray>{_parentController.GetType().FullName}  OnTimedEvent. All the required controllers have been created!</color>");
                CheckRequiredControllersReadiness();
            }
        }

        private void CheckRequiredControllersReadiness()
        {
            _notPreparedRequiredControllers = new List<IController>();
            Debug.Log($"<color=gray>{_parentController.GetType().FullName} _requiredControllers.Count = {_requiredControllers.Count}</color>");
            for (int i = 0; i < _requiredControllers.Count; i++)
            {
                Debug.Log($"<color=gray>{_parentController.GetType().FullName} _requiredControllers[i] == null = {_requiredControllers[i] == null }</color>");
                if (!_requiredControllers[i].IsActive)
                {
                    _notPreparedRequiredControllers.Add(_requiredControllers[i]);
                }
            }

            if (_notPreparedRequiredControllers.Count == 0)
            {
                OnDependencysReadyHandler(string.Empty);
            }
            else
            {
                for (int i = 0; i < _notPreparedRequiredControllers.Count; i++)
                {
                    _notPreparedRequiredControllers[i].OnReady += OnRequiredControllerReadyHandler;
                }
            }
        }

        private void OnRequiredControllerReadyHandler(string errorMessage, IController reportingController)
        {
            reportingController.OnReady -= OnRequiredControllerReadyHandler;
            if (string.IsNullOrEmpty(errorMessage))
            {
                var id = _notPreparedRequiredControllers.IndexOf(reportingController);
                _notPreparedRequiredControllers.RemoveAt(id);
                if (_notPreparedRequiredControllers.Count == 0)
                {
                    OnDependencysReadyHandler(string.Empty);
                }
            }
            else
            {
                for (int i = 0; i < _notPreparedRequiredControllers.Count; i++)
                {
                    _notPreparedRequiredControllers[i].OnReady -= OnRequiredControllerReadyHandler;
                }
                OnDependencysReadyHandler($"{this.GetType().Name}: Report from {reportingController.GetType().FullName}. Required controller {reportingController.GetType().Name} is crashed!");
            }
        }

        private void OnDependencysReadyHandler(string errorMessage)
        {
            OnReady?.Invoke(errorMessage);
        }
    }
}