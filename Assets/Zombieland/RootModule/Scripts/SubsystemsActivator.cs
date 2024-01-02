using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Zombieland.RootModule
{
    public sealed class SubsystemsActivator<T> : ISubsystemsActivator
    {
        public event Action<string> OnReady;

        private readonly T _parentController;
        private readonly List<IController> _controllers;
        private string[] _controllerNames;
        private List<string> _preparedControllerNames = new();
        private StringBuilder _stringBuilder = new();
        private int _counter;
        private bool _targetActivity;
        private bool _currentActivity;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private const int ControlTime = 2000; //in millisecond


        public SubsystemsActivator(T parentController, List<IController> subsystemsControllers)
        {
            _parentController = parentController;
            _controllers = subsystemsControllers;
            _controllerNames = _controllers.Select(controller => controller.GetType().Name).ToArray();
        }

        public void SetSubsystemsActivity(bool isActive)
        {
            StartCountdownCfControlTime();
            
            _targetActivity = isActive;
            _currentActivity = !isActive;
            _preparedControllerNames.Clear();
            _counter = _controllers.Count;
            for (int i = 0; i < _controllers.Count; i++)
            {
                _controllers[i].OnReady += OnSubsystemsReadinessHandler;
            }

            if (isActive)
            {
                for (int i = 0; i < _controllers.Count; i++)
                {
                    _controllers[i].Initialize(_parentController);
                }
            }
            else
            {
                for (int i = 0; i < _controllers.Count; i++)
                {
                    _controllers[i].Disable();
                }
            }
        }

        private void StartCountdownCfControlTime()
        {
            Task.Delay(ControlTime, _cancellationTokenSource.Token).ContinueWith(task => {
                if (!task.IsCanceled)
                {
                    OnPassedTimeHandler();
                }
            });
        }

        private void OnSubsystemsReadinessHandler(string errorMessage, IController reportingController)
        {
            reportingController.OnReady -= OnSubsystemsReadinessHandler;
            if (string.IsNullOrEmpty(errorMessage))
            {
                _preparedControllerNames.Add(reportingController.GetType().Name);
                _counter--;
                if (_counter == 0)
                {
                    OnSystemReadyHandler(string.Empty);
                }
            }
            else
            {
                for (int i = 0; i < _controllers.Count; i++)
                {
                    _controllers[i].OnReady -= OnSubsystemsReadinessHandler;
                }

                OnSystemReadyHandler($"{this.GetType().Name}: Report from {reportingController.GetType().Name}. System preparation failure! Error message: {errorMessage}");
            }
        }

        private void OnSystemReadyHandler(string errorMessage)
        {
            if (string.IsNullOrEmpty(errorMessage))
            {
                _currentActivity = _targetActivity;
                Debug.Log($"<color=green> {_parentController.GetType().Name} subsystems are ready!</color>");
            }
            else
            {
                Debug.Log($"<color=red> {_parentController.GetType().Name} System are not ready!</color>");
                Debug.LogError(errorMessage);
                ShowControllersState();
            }

            _cancellationTokenSource.Cancel();
            OnReady?.Invoke(errorMessage);
        }

        // Disabling startup after time has elapsed.
        private void OnPassedTimeHandler()
        {
            if (_targetActivity != _currentActivity)
            {
                Debug.Log($"<color=yellow>The system of {_parentController.GetType().Name} was not activated within the allotted time!</color>");
                ShowControllersState();
            }
        }

        private void ShowControllersState()
        {
            var unpreparedControllerNames = _controllerNames.Concat(_preparedControllerNames).GroupBy(name => name)
                .Where(group => group.Count() == 1)
                .Select(group => group.Key)
                .ToList();

            Debug.Log(
                $"<color=green>Report from {_parentController.GetType().Name}: Prepared controllers: {GetCombinedNamesOf(_preparedControllerNames)}</color>");
            Debug.Log(
                $"<color=red>Report from {_parentController.GetType().Name}: ERROR! Unprepared controllers: {GetCombinedNamesOf(unpreparedControllerNames)}</color>");
        }

        private StringBuilder GetCombinedNamesOf(List<string> controllerNames)
        {
            _stringBuilder.Clear();
            for (int i = 0; i < controllerNames.Count; i++)
            {
                _stringBuilder.Append(controllerNames[i]);
                _stringBuilder.Append(", ");
            }
            return _stringBuilder;
        }
    }
}