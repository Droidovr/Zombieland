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
        private UIInputPCController _uIInputPCController;
        private PlatformType _platformType;


        #region PUBLIC
        public UIMainController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {

        }

        public override void Disable()
        {
            if (_platformType == PlatformType.Mobile && _uIInputMobileController != null)
            {
                _uIInputMobileController.OnMoved -= HandleMoved;
                _uIInputMobileController.OnButtonClick -= HandleMainButtonClick;
            }

            //_uIInputPCController.OnMoved -= HandleMoved;
            //_uIInputPCController.OnButtonClick -= HandleMainButtonClick;
        }
        #endregion PUBLIC


        #region PROTECTED
        protected override void CreateHelpersScripts()
        {
            _platformType = PlatformDetection.GetPlatformType();

            Debug.Log("<color=red>" + _platformType.ToString() + "</color>");

            //if (_platformType == PlatformType.Unknown)
            //{
            //    throw new InvalidOperationException("The platform type could not be determined.");
            //}
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            //if (_platformType == PlatformType.Mobile)
            //{
                _uIInputMobileController = new UIInputMobileController(this, null);
                subsystemsControllers.Add((IController)_uIInputMobileController);

                _uIInputMobileController.OnMoved += HandleMoved;
                _uIInputMobileController.OnButtonClick += HandleMainButtonClick;
            //}

            //_uIInputPCController = new UIInputPCController(this, null);
            //subsystemsControllers.Add((IController)_uIInputPCController);

            //_uIInputPCController.OnMoved += HandleMoved;
            //_uIInputPCController.OnButtonClick += HandleMainButtonClick;

        }

        private void _uIInputPCController_OnMoved(Vector2 obj)
        {
            throw new NotImplementedException();
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
