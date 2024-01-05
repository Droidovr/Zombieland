using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.CharacterMovingModule
{
    public class CharacterMovingController : Controller, ICharacterMovingController
    {
        public ICharacterController CharacterController { get; }

        public event Action<Vector2> OnJoustickMoved;

        private ITestVisualBodyController _testVisualBodyController;
        private ITestCharacterDataController _testCharacterDataController;
        private ITestUIController _testUIController;

        private GameObject _character;

        public CharacterMovingController(IController parentController)
        { 
            CharacterController = (ICharacterController) parentController;
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            InitTestSystem();

            if (GetDependentSystemsActivity())
            {
                _character = _testVisualBodyController.GetCharacterGameobject();
                
                _character.AddComponent<CharacterPhysicMoving>();

                CharacterPhysicMoving characterPhysicMoving = _character.GetComponent<CharacterPhysicMoving>();

                characterPhysicMoving.MovingSpeed = _testCharacterDataController.GetCharacterData().SpeedMoving;
                characterPhysicMoving.MovingRotation = _testCharacterDataController.GetCharacterData().SpeedRotation;
                characterPhysicMoving.Gravity = _testCharacterDataController.GetCharacterData().Gravity;
                characterPhysicMoving.CharacterMovingController = this;
                characterPhysicMoving.Initialize();
            }
        }

        private void InitTestSystem()
        {
            _testVisualBodyController = new TestVisualBodyController(this);
            
            _testCharacterDataController = new TestCharacterDataController(this);
            
            _testUIController = new TestUIController(this);
            _testUIController.OnJoustickMoved += HandheldJoystickMoved;
        }

        private void HandheldJoystickMoved(Vector2 joystickPosition)
        {
            OnJoustickMoved?.Invoke(joystickPosition);
        }

        private bool GetDependentSystemsActivity()
        {
            if (_testVisualBodyController != null && _testCharacterDataController != null && _testUIController != null)
            {
                return true;
            }
            else 
            { 
                return false; 
            }
        }
    }
}
