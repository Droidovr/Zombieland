using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.UIModule
{
    public class UIInputMobileController : Controller, IUIMainController
    {
        public event Action<Vector2> OnMoved;
        public event Action<string> OnButtonClick;

        private InitializerMobileInputGameobjects _initializerInputGameobjects;
        private InputMobile _inputMobile;

        #region PUBLIC
        public UIInputMobileController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            // This class’s constructor doesn’t have any content yet.
        }

        public override void Disable()
        {
            if (_initializerInputGameobjects != null)
            {
                _inputMobile.OnJoystickMoved -= HandleJoystickMoved;
                _inputMobile.OnButtonClick -= HandleButtonClick;
            }
        }
        #endregion PUBLIC


        #region PROTECTED
        protected override void CreateHelpersScripts()
        {
            _initializerInputGameobjects = new InitializerMobileInputGameobjects();
            _initializerInputGameobjects.Init();

            _inputMobile = _initializerInputGameobjects.InputMobile;

            _inputMobile.OnJoystickMoved += HandleJoystickMoved;
            _inputMobile.OnButtonClick += HandleButtonClick;
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }
        #endregion PROTECTED


        #region PRIVATE
        private void HandleJoystickMoved(Vector2 joystickPosition)
        {
            OnMoved?.Invoke(joystickPosition);
        }

        private void HandleButtonClick(string nameButton)
        {
            OnButtonClick?.Invoke(nameButton);
        }
        #endregion PRIVATE
    }
}