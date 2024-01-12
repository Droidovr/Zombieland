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

        #region PUBLIC
        public UIInputMobileController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            // This class’s constructor doesn’t have any content yet.
        }

        public override void Disable()
        {
            if (_initializerInputGameobjects != null)
            {
                UnSubscriptionToEvent();
            }
        }
        #endregion PUBLIC

        protected override void CreateHelpersScripts()
        {
            CreateInputGameobjects();
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }

        #region PRIVATE
        private void CreateInputGameobjects()
        {
            _initializerInputGameobjects = new InitializerMobileInputGameobjects();
            _initializerInputGameobjects.Init();

            SubscriptionToEvent();
        }

        private void SubscriptionToEvent()
        {
            _initializerInputGameobjects.InputMobile.OnJoystickMoved += HandleJoystickMoved;
            _initializerInputGameobjects.InputMobile.OnButtonClick += HandleButtonClick;
        }

        private void UnSubscriptionToEvent()
        {
            _initializerInputGameobjects.InputMobile.OnJoystickMoved -= HandleJoystickMoved;
            _initializerInputGameobjects.InputMobile.OnButtonClick -= HandleButtonClick;
        }

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