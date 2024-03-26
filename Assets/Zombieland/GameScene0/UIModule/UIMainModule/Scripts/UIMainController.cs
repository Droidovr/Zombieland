using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.UIModule
{
    public class UIMainController : Controller, IUIMainController
    {
        public event Action<Vector2> OnMoved;
        public event Action<bool> OnFire;

        private InitializerInputPrefab _initializerInputGameobjects;

        #region PUBLIC
        public UIMainController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            // This class's constructor doesn't have any content yet.
        }

        public override void Disable()
        {
            if (_initializerInputGameobjects != null)
            {
                _initializerInputGameobjects.Input.OnMoved -= HandleMoved;
                _initializerInputGameobjects.Input.OnFire -= HandleButtonClick;
            }

            base.Disable();
        }
        #endregion PUBLIC


        #region PROTECTED
        protected override void CreateHelpersScripts()
        {
            _initializerInputGameobjects = new InitializerInputPrefab();
            _initializerInputGameobjects.Init();

            _initializerInputGameobjects.Input.OnMoved += HandleMoved;
            _initializerInputGameobjects.Input.OnFire += HandleButtonClick;
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn't have any subsystems at the moment.
        }
        #endregion PROTECTED


        #region PRIVATE
        private void HandleMoved(Vector2 vectorMove)
        {
            OnMoved?.Invoke(vectorMove);
        }

        private void HandleButtonClick(bool isFire)
        {
            OnFire?.Invoke(isFire);
        }
        #endregion PRIVATE
    }
}
