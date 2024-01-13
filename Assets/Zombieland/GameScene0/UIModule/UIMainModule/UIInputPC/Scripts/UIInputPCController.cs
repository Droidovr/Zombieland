using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.UIModule
{
    public class UIInputPCController : Controller, IUIMainController
    {
        public event Action<Vector2> OnMoved;
        public event Action<string> OnButtonClick;

        private InitializerPCInputGameobjects _initializerInputGameobjects;
        private InputPC _inputPC;


        #region PUBLIC
        public UIInputPCController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            // This class’s constructor doesn’t have any content yet.
        }
        public override void Disable()
        {
            if (_initializerInputGameobjects != null)
            {
                _inputPC.OnKeyboardMoved -= HandleJoystickMoved;
                _inputPC.OnButtonClick -= HandleButtonClick;
            }
        }
        #endregion PUBLIC

        protected override void CreateHelpersScripts()
        {
            _initializerInputGameobjects = new InitializerPCInputGameobjects();
            _initializerInputGameobjects.Init();

            _inputPC = _initializerInputGameobjects.InputPC;

            _inputPC.OnKeyboardMoved += HandleJoystickMoved;
            _inputPC.OnButtonClick += HandleButtonClick;
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }


        #region PRIVATE
        private void HandleJoystickMoved(Vector2 moveVector)
        {
            OnMoved?.Invoke(moveVector);
        }

        private void HandleButtonClick(string nameButton)
        {
            OnButtonClick?.Invoke(nameButton);
        }
        #endregion PRIVATE
    }
}
