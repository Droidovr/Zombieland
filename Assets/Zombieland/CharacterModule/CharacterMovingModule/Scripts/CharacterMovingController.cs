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


        public event Action<string, IController> OnReady;


        private Vector2 _directionMove;
        private ITestCharacterController _testCharacterController;
        private IController _testVisualBodyController;
        private GameObject _character;

        public void Disable()
        {
            SetSystemsActivity(false);
        }

        public void Initialize<T>(T parentController)
        {
            _testCharacterController = parentController as ITestCharacterController;
            _testVisualBodyController = _testCharacterController.TestVisualBodyController as IController;

            _testVisualBodyController.OnReady += PhysicsInitializer;
        }

        private void PhysicsInitializer(string arg1, IController controller)
        {
            _character = _testCharacterController.TestVisualBodyController.GetCharacterGameobject();

            CharacterPhysicsInitializer.AddRigidbodyComponent(_character);
            CharacterPhysicsInitializer.AddColliderComponent(_character);

            SetSystemsActivity(true);
        }

        private void SetPhysicCharacterProperties()
        {
            
        }

        private void SetSystemsActivity(bool isActive)
        {
            IsActive = isActive;
            OnReady?.Invoke(String.Empty, this);
        }
    }
}
