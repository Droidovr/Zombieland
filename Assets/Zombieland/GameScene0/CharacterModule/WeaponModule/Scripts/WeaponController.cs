using System;
using System.Collections.Generic;
using UnityEngine;


namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    public class WeaponController : Controller, IWeaponController
    {
        public event Action OnImpactDepleted;
        public event Action OnShotAnimationPreparing;
        public event Action OnShotPerformed;
        public event Action OnShotFailed;

        public ICharacterController CharacterController { get; private set; }
        public IWeapon Weapon { get; private set; }
        public string CurrentImpactName { get; private set; }

        public WeaponController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            CharacterController = parentController as ICharacterController;
        }

        public override void Disable()
        {
            if (Weapon != null)
            {
                Weapon.ShotProcess.StopFire();
                Weapon.ShotProcess.OnShotPerformed -= ShotHandler;
            }

            CharacterController.EquipmentController.OnWeaponChanged -= WeaponChangedHandler;
            CharacterController.EquipmentController.OnImpactChanged -= AmmoChangedHandler;
            CharacterController.RootController.UIController.OnFire -= ButtonFireHandler;
            CharacterController.RootController.UIController.OnWeaponReaload -= ReCharge;

            base.Disable();
        }

        public void ReCharge()
        {
            CharacterController.EquipmentController.ReloadCurrentWeapon();
        }

        protected override void CreateHelpersScripts()
        {
            CharacterController.EquipmentController.OnWeaponChanged += WeaponChangedHandler;
            CharacterController.EquipmentController.OnImpactChanged += AmmoChangedHandler;
            CharacterController.RootController.UIController.OnFire += ButtonFireHandler;
            CharacterController.RootController.UIController.OnWeaponReaload += ReCharge;
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }

        private void WeaponChangedHandler(Weapon weapon)
        {
            Weapon = weapon;
            Weapon.Init(this);
            Weapon.ShotProcess.OnShotPerformed += ShotHandler;
        }

        private void AmmoChangedHandler(string impactName)
        {
            CurrentImpactName = impactName;
        }

        private void ButtonFireHandler(bool isFire)
        {
            if (isFire)
            {
                if (Weapon != null)
                {
                    Weapon.ShotProcess.StartFire();
                    Debug.Log("Weapon - ButtonFireDownHandler");
                }
            }
            else
            {
                if (Weapon != null)
                {
                    Weapon.ShotProcess.StopFire();
                    Debug.Log("Weapon - ButtonFireUpHandler");
                }
            }
        }

        private void ShotHandler()
        {
            if (Weapon != null)
            {
                OnShotPerformed?.Invoke();
                Debug.Log("Weapon - OnShotHandler");
            }
        }
    }
}