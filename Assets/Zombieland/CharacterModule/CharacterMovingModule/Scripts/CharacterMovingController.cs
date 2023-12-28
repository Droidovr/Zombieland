using System;
using UnityEngine;
using Zombieland.RootModule;

namespace Zombieland.CharacterModule.CharacterMovingModule
{
    public class CharacterMoovingController : IController, ICharacterMovingController
    {
        public bool IsActive { get; private set; }
        public Vector2 DirectionMove
        {
            get
            {
                return _directionMove;
            }
            set
            {
                _directionMove = value;
            }
        }
        public PhysicCharacterProperties PhysicCharacterProperties 
        {
            get { return _physicCharacterProperties; } 
        }

        public event Action<string, IController> OnReady;


        private Vector2 _directionMove;
        private ITestCharacterController _testCharacterController;
        private GameObject _prefabCharacter;
        private PhysicCharacterProperties _physicCharacterProperties;

        public void Disable()
        {
            IsActive = false;
            OnReady?.Invoke(String.Empty, this);
        }

        public void Initialize<T>(T parentController)
        {
            _testCharacterController = parentController as ITestCharacterController;
            //_prefabCharacter = _testCharacterController.GetPrefab();

            if (SetPhysicCharacterProperties() && InitCharacterMove())
            {
                IsActive = true;
            }
            OnReady?.Invoke(String.Empty, this);
        }

        private bool SetPhysicCharacterProperties()
        {
            return true;
        }

        private bool InitCharacterMove()
        {
            _prefabCharacter.AddComponent<CharacterPhysicMoving>();
            // Додаємо фізичні властивості, якщо потрібно
            
            return true;
        }
    }
}
