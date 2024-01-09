using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.UIModule
{
    public class UIController : Controller, IUIController
    {
        public event Action<Vector2> OnJoystickMoved;

        private InitializerJoystick _initializerJoystick;

        public UIController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            // This class’s constructor doesn’t have any content yet.
            CreateInputSystem();
        }

        public override void Disable()
        {
            //_initializerJoystick.InputSystem.OnJoystickMoved -= HandleJoystickMoved;
        }

        protected override void CreateHelpersScripts()
        {
            //CreateInputSystem();
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }

        private void CreateInputSystem()
        {
            GameObject emptyGameobject = new GameObject();
            emptyGameobject.AddComponent<InitializerJoystick>();

            _initializerJoystick = emptyGameobject.GetComponent<InitializerJoystick>();

            _initializerJoystick.Init();
            _initializerJoystick.InputSystem.OnJoystickMoved += HandleJoystickMoved;
        }

        private void HandleJoystickMoved(Vector2 joystickPosition)
        {
            OnJoystickMoved?.Invoke(joystickPosition);
            Debug.Log(joystickPosition);
        }
    }
}
