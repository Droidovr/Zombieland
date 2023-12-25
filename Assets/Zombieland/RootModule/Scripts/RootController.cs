using System;
using System.Collections.Generic;
using UnityEngine;
using Zombieland.GameDataModule;

namespace Zombieland.RootModule
{
    public class RootController : MonoBehaviour, IController, IRootController
    {
        public bool IsActive { get; private set; }
        public event Action<string, IController> OnReady;


        //TODO : Add required subsystems here
        public IGameDataController GameDataController { get; private set; }

        private List<IController> _controllers;
        private int _counter;


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

        #region UNITY
        void Awake()
        {
            Initialize<IRootController>(null);
        }

        private void OnDestroy()
        {
            Disable();
        }
        #endregion UNITY

        #region PRIVATE
        private void CreateSubsystems()
        {
            //TODO : Add required subsystems here
            GameDataController = new GameDataModule.GameDataController();
            _controllers.Add(GameDataController as IController);

            SetSubsystemsActivity(true);
        }

        private void SetSubsystemsActivity(bool isActive)
        {
            IsActive = !isActive;
            _counter = _controllers.Count;
            foreach (var controller in _controllers)
            {
                controller.OnReady += OnSubsystemsReadinessHandler;
            }

            foreach (var controller in _controllers)
            {
                controller.Initialize(this);
            }
        }

        private void OnSubsystemsReadinessHandler(string errorMessage, IController controller)
        {
            controller.OnReady -= OnSubsystemsReadinessHandler;
            if (string.IsNullOrEmpty(errorMessage))
            {
                Debug.Log($"Report from {controller.GetType().Name}: I'm ready!");
                _counter--;
                if (_counter == 0)
                {
                    Debug.Log($"<color=green>On system ready!</color>");
                    OnSystemReadyHandler(errorMessage);
                }
            }
            else
            {
                for (int i = 0; i < _controllers.Count; i++)
                {
                    _controllers[i].OnReady -= OnSubsystemsReadinessHandler;
                }

                var message =
                    $"{this.GetType().Name}: Report from {controller.GetType().Name}. System preparation failure! Error message: {errorMessage}";
                Debug.LogError(message);
                OnSystemReadyHandler(message);
            }
        }

        private void OnSystemReadyHandler(string errorMessage)
        {
            IsActive = !IsActive;
            OnReady?.Invoke(errorMessage, this);
            if (string.IsNullOrEmpty(errorMessage))
            {
                Debug.Log($"<color=green> System ready!</color>");
            }
            else
            {
                Debug.LogError(errorMessage);
            }

            //TODO : if IsActive == true -> start the game or load new scene when IsActive = false
            
            //CreateTestCharacterData();
            //TestLoadingOfCharacterData();
        }
        #endregion PRIVATE

        #region TEST

        private void CreateTestCharacterData()
        {
            var newCharData = new Zombieland.CharacterModule.CharacterDataModule.CharacterData();
            newCharData.MaxAcceleration = 0.1f;
            newCharData.MaxSpeed = 5f;
            this.GameDataController.SaveDada("TestCharacterData", newCharData);
        }

        private void TestLoadingOfCharacterData()
        {
            var charData = this.GameDataController.GetData<Zombieland.CharacterModule.CharacterDataModule.CharacterData>("TestCharacterData");
            Debug.Log($"<color=blue>Acceleration of character = {charData.MaxAcceleration}</color>");
        }
        #endregion
    }
}