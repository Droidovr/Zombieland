using System;
using System.Collections.Generic;
using UnityEngine;
using Zombieland.GameScene0.RootModule;

namespace Zombieland.GameScene0.CharacterModule.CharacterMovingModule
{
    public class CharacterMovingController : Controller, ICharacterMovingController
    {
        public event Action<Vector2> OnMoved;

        public readonly ICharacterController CharacterController;

        private float _movingSpeed;
        private float _movingRotation;
        private float _gravity;

        public CharacterMovingController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            CharacterController = parentController as ICharacterController;
        }

        protected override void CreateHelpersScripts()
        {
            CharacterController.RootController.UIController.OnMoved += HandleMoved;

            GetDataFromCharacterData();

            Customization();
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            
        }

        #region PRIVATE
        private void GetDataFromCharacterData()
        {
            // Get Data CharacterDataController
            _movingSpeed = 5f;
            _movingRotation = 5f;
            _gravity = 9.8f;                
        }

        private void Customization()
        {
            GameObject character = CharacterController.VisualBodyController.CharacterPrefab;

            UnityEngine.CharacterController unityCharacterController = character.AddComponent<UnityEngine.CharacterController>();
            unityCharacterController.center = new Vector3(0, 1f, 0);
            
            character.AddComponent<CharacterPhysicMoving>();
            CharacterPhysicMoving characterPhysicMoving = character.GetComponent<CharacterPhysicMoving>();
            characterPhysicMoving.Initialize(this, _movingSpeed, _movingRotation, _gravity);
        }
        private void HandleMoved(Vector2 vectorMove)
        {
            OnMoved?.Invoke(vectorMove);
        }
        #endregion
    }
}
