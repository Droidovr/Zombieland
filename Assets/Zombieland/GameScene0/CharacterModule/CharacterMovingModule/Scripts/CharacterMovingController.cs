using System;
using UnityEngine;
using Zombieland.RootModule;

namespace Zombieland.CharacterModule.CharacterMovingModule
{
    public class CharacterMovingController : IController, ICharacterMovingController
    {
        public bool IsActive { get; private set; }

        public event Action<string, IController> OnReady;
        public event Action<Vector2> OnJoustickMoved;

        private ITestCharacterController _testCharacterController;
        private IController _testVisualBodyController;
        private IController _testCharacterDataController;
        private IController _testUIController;
        private GameObject _character;
        private TestCharacterData _testCharacterData;

        private bool _visualBodyControllerReady;
        private bool _characterDataControllerReady;
        private bool _uiControllerReady;

        #region PUBLIC
        public void Disable()
        {
            SetSystemsActivity(false);
        }

        public void Initialize<T>(T parentController)
        {
            _testCharacterController = parentController as ITestCharacterController;
            _testVisualBodyController = _testCharacterController.TestVisualBodyController as IController;
            _testCharacterDataController = _testCharacterController.TestCharacterDataController as IController;
            _testUIController = _testCharacterController.TestUIController as IController;

            _testVisualBodyController.OnReady += PhysicsInitializer;
            _testCharacterDataController.OnReady += SetPhysicCharacterProperties;
            _testUIController.OnReady += InputInitializer;
        }
        #endregion PUBLIC


        #region PRIVATE
        private void PhysicsInitializer(string arg1, IController controller)
        {
            _character = _testCharacterController.TestVisualBodyController.GetCharacterGameobject();

            _visualBodyControllerReady = true;
            OnControllerReady();
        }

        private void SetPhysicCharacterProperties(string arg1, IController controller)
        {
            _testCharacterData = _testCharacterController.TestCharacterDataController.GetCharacterData();

            _characterDataControllerReady = true;
            OnControllerReady();
        }

        private void InputInitializer(string arg1, IController controller)
        {
            (controller as ITestUIController).OnJoustickMoved += HandheldJoystickMoved;

            _uiControllerReady = true;
            OnControllerReady();
        }

        private void HandheldJoystickMoved(Vector2 joystickPosition)
        {
            OnJoustickMoved?.Invoke(joystickPosition);
        }

        private void OnControllerReady()
        {
            if (_visualBodyControllerReady && _characterDataControllerReady && _uiControllerReady)
            {
                _character.AddComponent<CharacterPhysicMoving>();

                CharacterPhysicMoving characterPhysicMoving = _character.GetComponent<CharacterPhysicMoving>();

                characterPhysicMoving.MovingSpeed = _testCharacterData.SpeedMoving;
                characterPhysicMoving.MovingRotation = _testCharacterData.SpeedRotation;
                characterPhysicMoving.Gravity = _testCharacterData.Gravity;
                characterPhysicMoving.CharacterMovingController = this;
                characterPhysicMoving.Initialize();

                SetSystemsActivity(true);
            }
        }

        private void SetSystemsActivity(bool isActive)
        {
            IsActive = isActive;
            OnReady?.Invoke(String.Empty, this);
        }
        #endregion PRIVATE
    }
}
