using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.UIModule
{
    public class UIMainController : Controller, IUIMainController
    {
        public event Action<Vector2> OnMoved;
        public event Action<string> OnButtonClick;

        private UIInputMobileController _uIInputMobileController;


        #region PUBLIC
        public UIMainController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            // This class’s constructor doesn’t have any content yet.
        }

        public override void Disable()
        {
            if (_uIInputMobileController != null)
            {
                _uIInputMobileController.OnMoved -= HandleMoved;
                _uIInputMobileController.OnButtonClick -= HandleMainButtonClick;
            }
        }
        #endregion PUBLIC


        #region PROTECTED
        protected override void CreateHelpersScripts()
        {
            // This controller does not have helpers scripts.
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // It is necessary to organize the logic of which control system to load depending on the platform

            _uIInputMobileController = new UIInputMobileController(this, null);
            subsystemsControllers.Add((IController)_uIInputMobileController);

            _uIInputMobileController.OnMoved += HandleMoved;
            _uIInputMobileController.OnButtonClick += HandleMainButtonClick;
        }
        #endregion PROTECTED


        #region PRIVATE
        private void HandleMoved(Vector2 vectorMove)
        {
            OnMoved?.Invoke(vectorMove);
        }

        private void HandleMainButtonClick(string nameButton)
        {
            OnButtonClick?.Invoke(nameButton);
        }
        #endregion PRIVATE
    }
}
