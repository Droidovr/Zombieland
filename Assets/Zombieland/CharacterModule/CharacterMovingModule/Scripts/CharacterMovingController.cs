using System;
using UnityEngine;
using Zombieland.CharacterModule.CharacterDataModule;
using Zombieland.RootModule;

namespace Zombieland.CharacterModule.CharacterMovingModule
{
    public class CharacterMoovingController : IController, ICharacterMovingController
    {
        public bool IsActive { get; private set; }
        public event Action<string, IController> OnReady;

        //private ICharacterController _characterController;
        private CharacterPhysicMoving _physicBasedMoving;
        private PhysicCharacterProperties _physicCharacterProperties;

        public void Disable()
        {
            IsActive = false;
            OnReady?.Invoke(String.Empty, this);
        }

        public void Initialize<T>(T parentController)
        {
            //_characterController = parentController as ICharacterController;
            // _physicBasedMoving = получить ссылку


            // Get Data & filling PhysicCharacterProperties

            if (SetPhysicCharacterProperties())
            {
                IsActive = true;
            }
            OnReady?.Invoke(String.Empty, this);
        }

        public void Move(Vector2 direction)
        {
            _physicBasedMoving.Move(direction);
        }

        private bool SetPhysicCharacterProperties()
        {
            return true;
        }
    }
}
