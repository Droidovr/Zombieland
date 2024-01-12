using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.UIModule
{
    public class UIPCController : Controller, IUIPCController
    {
        public event Action<Vector2> OnKeyboardMoved;
        public event Action OnFire;

        private InitializerPCInputGameobjects _initializerInputGameobjects;


        #region PUBLIC
        public UIPCController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            // This class’s constructor doesn’t have any content yet.
        }
        public override void Disable()
        {
            if (_initializerInputGameobjects != null)
            {
                _initializerInputGameobjects.InputPCManager.OnKeyboardMoved -= HandleJoystickMoved;
                _initializerInputGameobjects.InputPCManager.OnFire -= HandleFire;
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
            _initializerInputGameobjects = new InitializerPCInputGameobjects();
            _initializerInputGameobjects.Init();
            _initializerInputGameobjects.InputPCManager.OnKeyboardMoved += HandleJoystickMoved;
            _initializerInputGameobjects.InputPCManager.OnFire += HandleFire;
        }

        private void HandleJoystickMoved(Vector2 moveVector)
        {
            OnKeyboardMoved?.Invoke(moveVector);
            Debug.Log(moveVector);
        }

        private void HandleFire()
        {
            OnFire?.Invoke();
            Debug.Log("Pif-Paf ... Pif-Paf ... Pif-Paf !!!!");
        }
        #endregion PRIVATE
    }
}
