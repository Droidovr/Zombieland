using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.UIModule
{
    public class UIMobileController : Controller, IUIMobileController
    {
        public event Action<Vector2> OnJoystickMoved;
        public event Action OnFire;

        private InitializerMobileInputGameobjects _initializerInputGameobjects;


        #region PUBLIC
        public UIMobileController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            // This class’s constructor doesn’t have any content yet.
        }

        public override void Disable()
        {
            if (_initializerInputGameobjects != null)
            {
                _initializerInputGameobjects.InputMobileManager.OnJoystickMoved -= HandleJoystickMoved;
                _initializerInputGameobjects.InputMobileManager.OnFire -= HandleFire;
            }
        }
        #endregion PUBLIC


        #region PROTECTED
        protected override void CreateHelpersScripts()
        {
            CreateInputGameobjects();
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }
        #endregion PROTECTED


        #region PRIVATE
        private void CreateInputGameobjects()
        {
            _initializerInputGameobjects = new InitializerMobileInputGameobjects();
            _initializerInputGameobjects.Init();
            _initializerInputGameobjects.InputMobileManager.OnJoystickMoved += HandleJoystickMoved;
            _initializerInputGameobjects.InputMobileManager.OnFire += HandleFire;
        }

        private void HandleJoystickMoved(Vector2 joystickPosition)
        {
            OnJoystickMoved?.Invoke(joystickPosition);
            Debug.Log(joystickPosition);
        }

        private void HandleFire()
        {
            OnFire?.Invoke();
            Debug.Log("Pif-Paf ... Pif-Paf ... Pif-Paf !!!!");
        }
        #endregion PRIVATE
    }
}
