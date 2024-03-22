using System;
using System.Collections.Generic;
using UnityEngine;


namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    public class WeaponController : Controller, IWeaponController
    {
        public event Action OnAmmoDepleted;
        public event Action OnShotPerformed;
        public event Action OnShotFailed;

        public ICharacterController CharacterController { get; private set; }
        public IWeapon Weapon { get; private set; }
        public string CurrentImpactName { get; private set; }



        public WeaponController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            CharacterController = parentController as ICharacterController;
        }

        public override void Enable()
        {
            CharacterController.EquipmentController.OnWeaponChanged += WeaponChangedHandler;
            CharacterController.EquipmentController.OnAmmoChanged += AmmoChangedHandler;
            CharacterController.RootController.UIController.OnFireDown += ButtonFireDownHandler;
            CharacterController.RootController.UIController.OnFireUp += ButtonFireUpHandler;

            base.Enable();
        }

        public override void Disable()
        {
            if (Weapon != null)
            {
                Weapon.ShotProcess.StopFire();
                Weapon.ShotProcess.OnShotPerformed -= OnShotHandler;
            }

            CharacterController.EquipmentController.OnWeaponChanged -= WeaponChangedHandler;
            CharacterController.EquipmentController.OnAmmoChanged -= AmmoChangedHandler;
            CharacterController.RootController.UIController.OnFireDown -= ButtonFireDownHandler;
            CharacterController.RootController.UIController.OnFireUp -= ButtonFireUpHandler;

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

        private void WeaponChangedHandler(Weapon weapon)
        {
            Weapon = weapon;
            Weapon.ShotProcess.OnShotPerformed += OnShotHandler;
        }

        private void AmmoChangedHandler(string impactName)
        {
            CurrentImpactName = impactName;
        }

        private void ButtonFireDownHandler()
        {
            if (Weapon != null)
            {
                Weapon.ShotProcess.StartFire();
                Debug.Log("Weapon - ButtonFireDownHandler");
            }
        }

        private void ButtonFireUpHandler() 
        {
            if (Weapon != null)
            {
                Weapon.ShotProcess.StopFire();
                Debug.Log("Weapon - ButtonFireUpHandler");
            }
        }

        private void OnShotHandler()
        {
            if (Weapon != null)
            {
                OnShotPerformed?.Invoke();
                Debug.Log("Weapon - OnShotHandler");
            }
        }
    }
}