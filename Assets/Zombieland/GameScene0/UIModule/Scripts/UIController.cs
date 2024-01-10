using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.UIModule
{
    public class UIController : Controller, IUIController
    {
        public event Action<Vector2> OnJoystickMoved;

        private InitializerJoystick _initializerJoystick;

        #region PUBLIC
        public UIController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            // This class’s constructor doesn’t have any content yet.
        }

        public override void Disable()
        {
            if (_initializerJoystick != null)
            {
                _initializerJoystick.InputManager.OnJoystickMoved -= HandleJoystickMoved;
            }
        }
        #endregion PUBLIC




        #region PROTECTED
        protected override void CreateHelpersScripts()
        {
            CreateJoystick();
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }
        #endregion PROTECTED




        #region PRIVATE
        private void CreateJoystick()
        {
            _initializerJoystick = new InitializerJoystick();
            _initializerJoystick.Init();
            _initializerJoystick.InputManager.OnJoystickMoved += HandleJoystickMoved;
        }

        private void HandleJoystickMoved(Vector2 joystickPosition)
        {
            OnJoystickMoved?.Invoke(joystickPosition);
            Debug.Log(joystickPosition);
        }
        #endregion PRIVATE
    }
}
