
using System;
using System.Collections.Generic;

namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    public class WeaponController : Controller, IWeaponController
    {
        public event Action OnAmmoDepleted;
        public event Action OnShotPerformed;
        public event Action OnShotFailed;

        public readonly ICharacterController CharacterController;

        private Weapon _weapon;

        public WeaponController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            CharacterController = parentController as ICharacterController;

            CharacterController.EquipmentController.OnWeaponChanged += SetWeapon;
            CharacterController.RootController.UIController.OnFireDown += OnButtonFireDownHandler;
            CharacterController.RootController.UIController.OnFireUp += OnButtonFireUpHandler;
        }

        public override void Disable()
        {
            CharacterController.EquipmentController.OnWeaponChanged -= SetWeapon;
            CharacterController.RootController.UIController.OnFireDown -= OnButtonFireDownHandler;
            CharacterController.RootController.UIController.OnFireUp -= OnButtonFireUpHandler;

            base.Disable();
        }

        protected override void CreateHelpersScripts()
        {
            // This controller doesn’t have any helpers scripts at the moment.
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }

        private void SetWeapon(Weapon wepon)
        {
            _weapon = wepon;
        }

        private void OnButtonFireDownHandler()
        { 
            // Логика обработки нажатия кнопки стрельбы
        }

        private void OnButtonFireUpHandler() 
        { 
            // Логика обработки отпускания кнопки стрельбы
        }
    }
}