using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.UIModule
{
    public class UIController : Controller, IUIController
    {
        public event Action<Vector2> OnMoved;
        public event Action OnFireDown;
        public event Action OnFireUp;
        public event Action<string> OnInventoryButtonClick;

        private UIMainController _uIMainController;

        #region PUBLIC
        public UIController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            // This class's constructor doesn't have any content yet.
        }

        public override void Disable()
        {
            if (_uIMainController != null)
            {
                _uIMainController.OnMoved -= HandleMoved;
                _uIMainController.OnFireDown -= HandleFireClickDown;
                _uIMainController.OnFireUp -= HandleFireClickUp;
            }

            base.Disable();
        }
        #endregion PUBLIC


        #region PROTECTED
        protected override void CreateHelpersScripts()
        {
            // This controller doesn't have any helpers scripts at the moment.
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            _uIMainController = new UIMainController(this, null);
            subsystemsControllers.Add((IController)_uIMainController);

            _uIMainController.OnMoved += HandleMoved;
            _uIMainController.OnFireDown += HandleFireClickDown;
            _uIMainController.OnFireUp += HandleFireClickUp;
        }
        #endregion PROTECTED


        #region PRIVATE
        private void HandleMoved(Vector2 vectorMove)
        {
            OnMoved?.Invoke(vectorMove);
            // Debug.Log(vectorMove);
        }

        private void HandleFireClickDown()
        {
            OnFireDown?.Invoke();
            Debug.Log("HandleFireClickDown");
        }

        private void HandleFireClickUp()
        {
            OnFireUp?.Invoke();
            Debug.Log("HandleFireClickUp");
        }
        #endregion PRIVATE
    }
}