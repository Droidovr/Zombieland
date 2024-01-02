using System;
using System.Collections.Generic;
using UnityEngine;
using Zombieland.GameDataModule;

namespace Zombieland.RootModule
{
    public class RootController : MonoBehaviour, IController, IRootController
    {
        public bool IsReady { get; private set; }
        public event Action<string, IController> OnReady;

        //TODO : Add required subsystems here
        public IGameDataController GameDataController { get; private set; }

        private List<IController> _subsystemsControllers;
        private ISubsystemsActivator _subsystemsActivator;



        #region PUBLIC
        public void Initialize<T>(T parentController)
        {
            _subsystemsControllers = new List<IController>();
            CreateSubsystems();
        }

        public void Disable()
        {
            _subsystemsActivator.OnReady += OnSystemReadyHandler;
            _subsystemsActivator.SetSubsystemsActivity(false);
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
            GameDataController = new GameDataController();
            _subsystemsControllers.Add(GameDataController as IController);

            _subsystemsActivator = new SubsystemsActivator<IRootController>(this, _subsystemsControllers);
            _subsystemsActivator.OnReady += OnSystemReadyHandler;
            _subsystemsActivator.SetSubsystemsActivity(true);
        }

        private void OnSystemReadyHandler(string errorMessage)
        {
            _subsystemsActivator.OnReady -= OnSystemReadyHandler;
            IsReady = string.IsNullOrEmpty(errorMessage);
            OnReady?.Invoke(errorMessage, this);
            
            //TODO : if IsActive == true -> start the game or load new scene when IsActive = false
        }
        #endregion PRIVATE

        // #region TEST
        // private void CreateTestCharacterData()
        // {
        //     var newCharData = new Zombieland.CharacterModule.CharacterDataModule.CharacterData();
        //     newCharData.MaxAcceleration = 0.1f;
        //     newCharData.MaxSpeed = 5f;
        //     this.GameDataController.SaveDada("TestCharacterData", newCharData);
        // }
        //
        // private void TestLoadingOfCharacterData()
        // {
        //     var charData = this.GameDataController.GetData<Zombieland.CharacterModule.CharacterDataModule.CharacterData>("TestCharacterData");
        //     Debug.Log($"<color=blue>Acceleration of character = {charData.MaxAcceleration}</color>");
        // }
        // #endregion
    }
}