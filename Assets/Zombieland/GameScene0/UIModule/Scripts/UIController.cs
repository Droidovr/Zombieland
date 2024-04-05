using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.UIModule
{
    public class UIController : Controller, IUIController
    {
        public event Action<Vector2> OnMoved;
        public event Action<Vector2> OnMouseMoved;
        public event Action<bool> OnFire;
        public event Action<bool> OnStealth;
        public event Action<bool> OnFastRun;
        public event Action OnWeaponReaload;
        public event Action OnUse;
        public event Action OnInventory;
        public event Action OnThrow;
        public event Action OnNumber1;
        public event Action OnNumber2;
        public event Action OnNumber3;
        public event Action OnNumber4;

        public Vector2 SizeCursor { get; set; }

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
                _uIMainController.OnMouseMoved -= HandleMouseMoved;
                _uIMainController.OnFire -= HandleFireClick;
                _uIMainController.OnStealth -= HandleStealthClick;
                _uIMainController.OnFastRun -= HandleFastRunClick;
                _uIMainController.OnWeaponReaload -= HandleWeaponRealoadClick;
                _uIMainController.OnUse -= HandleUseEClick;
                _uIMainController.OnInventory -= HandleInventoryEClick;
                _uIMainController.OnThrow -= HandleThrowClick;
                _uIMainController.OnNumber1 -= HandleNumber1Click;
                _uIMainController.OnNumber2 -= HandleNumber2Click;
                _uIMainController.OnNumber3 -= HandleNumber3Click;
                _uIMainController.OnNumber4 -= HandleNumber4Click;
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
            _uIMainController.OnMouseMoved += HandleMouseMoved;
            _uIMainController.OnFire += HandleFireClick;
            _uIMainController.OnStealth += HandleStealthClick;
            _uIMainController.OnFastRun += HandleFastRunClick;
            _uIMainController.OnWeaponReaload += HandleWeaponRealoadClick;
            _uIMainController.OnUse += HandleUseEClick;
            _uIMainController.OnInventory += HandleInventoryEClick;
            _uIMainController.OnThrow += HandleThrowClick;
            _uIMainController.OnNumber1 += HandleNumber1Click;
            _uIMainController.OnNumber2 += HandleNumber2Click;
            _uIMainController.OnNumber3 += HandleNumber3Click;
            _uIMainController.OnNumber4 += HandleNumber4Click;
        }
        #endregion PROTECTED


        #region PRIVATE
        private void HandleMoved(Vector2 vectorMove)
        {
            OnMoved?.Invoke(vectorMove);
        }

        private void HandleMouseMoved(Vector2 mousePosition)
        {
            OnMouseMoved?.Invoke(mousePosition);
        }

        private void HandleFireClick(bool isFire)
        {
            OnFire?.Invoke(isFire);
        }

        private void HandleStealthClick(bool isStaelth)
        {
            OnStealth?.Invoke(isStaelth);
        }

        private void HandleFastRunClick(bool isFastRun)
        {
            OnFastRun?.Invoke(isFastRun);
        }

        private void HandleWeaponRealoadClick()
        {
            OnWeaponReaload?.Invoke();
        }

        private void HandleUseEClick()
        {
            OnUse?.Invoke();
        }

        private void HandleInventoryEClick()
        {
            OnInventory?.Invoke();
        }

        private void HandleThrowClick()
        {
            OnThrow?.Invoke();
        }

        private void HandleNumber1Click()
        {
            OnNumber1?.Invoke();
        }

        private void HandleNumber2Click()
        {
            OnNumber2?.Invoke();
        }

        private void HandleNumber3Click()
        {
            OnNumber3?.Invoke();
        }

        private void HandleNumber4Click()
        {
            OnNumber4?.Invoke();
        }
        #endregion PRIVATE
    }
}