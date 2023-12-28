using System;
using System.Collections.Generic;
using UnityEngine;
using Zombieland.RootModule;

namespace Zombieland.CharacterModule.CharacterMovingModule
{
    public class TestCharacterController : MonoBehaviour, IController, ITestCharacterController
    {
        public bool IsActive { get; private set; }
        public ICharacterMovingController CharacterMovingController { get; private set; }


        public event Action<string, IController> OnReady;

        private List<IController> _controllers;
        private int _counter;

        #region MonoBehaviour
        private void Awake()
        {
            Initialize<ITestCharacterController>(null);
        }
        #endregion

        #region PUBLIC
        public void Initialize<T>(T parentController)
        {
            _controllers = new List<IController>();
            CreateSubsystems();
        }

        public void Disable()
        {
            SetSubsystemsActivity(false);
        }
        #endregion PUBLIC


        #region PRIVATE
        private void CreateSubsystems()
        {
            //TODO : Add required subsystems here
            CharacterMovingController = new CharacterMoovingController();
            _controllers.Add(CharacterMovingController as IController);

            SetSubsystemsActivity(false);
        }

        private void SetSubsystemsActivity(bool isActive)
        {
            IsActive = isActive;
            _counter = _controllers.Count;
            foreach (IController controller in _controllers)
            {
                controller.OnReady += OnSubsystemReadinessHandler;
            }

            foreach (IController controller in _controllers)
            {
                controller.Initialize(this);
            }
        }

        private void OnSubsystemReadinessHandler(string errorMassage, IController controller)
        {
            controller.OnReady -= OnSubsystemReadinessHandler;
            if (string.IsNullOrEmpty(errorMassage))
            {
                Debug.Log($"Report from {controller.GetType().Name}: I'm ready!");
                _counter--;
                if (_counter == 0)
                {
                    Debug.Log($"<color=green>On system ready!</color>");
                    OnSystemReadyHandler(errorMassage);
                }
            }
            else
            {
                for (int i = 0; i < _controllers.Count; i++)
                {
                    _controllers[i].OnReady -= OnSubsystemReadinessHandler;
                }

                string message = $"{this.GetType().Name}: Report from {controller.GetType().Name}. System preparation failture! Error message: {errorMassage}";
                Debug.Log(message);
                OnSystemReadyHandler(message);
            }
        }

        private void OnSystemReadyHandler(string errorMessage)
        {
            IsActive = !IsActive;
            OnReady?.Invoke(errorMessage, this);
            if (string.IsNullOrEmpty(errorMessage))
            {
                Debug.Log($"<color=green>System ready!</color>");
            }
            else
            {
                Debug.LogError(errorMessage);
            }
        }
        #endregion PRIVATE
    }
}
