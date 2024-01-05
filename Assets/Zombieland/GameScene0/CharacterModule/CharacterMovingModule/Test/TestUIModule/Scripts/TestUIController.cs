using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.CharacterMovingModule
{
    public class TestUIController : Controller, ITestUIController
    {
        public ICharacterController CharacterController { get; }

        public event Action<Vector2> OnJoustickMoved;

        private CreateInputGameobject _inputSystem;

        public TestUIController(IController parentController)
        {
            //CharacterController = (ICharacterController)parentController;
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            _inputSystem = new CreateInputGameobject();
            _inputSystem.InputSystem.OnJoustickMoved += OnJoustickMoved;
        }

        private void HandleJoustickMoved(Vector2 joystickPosition)
        {
            OnJoustickMoved?.Invoke(joystickPosition);
        }
    }
}
