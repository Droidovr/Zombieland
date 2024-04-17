using System;
using System.Collections.Generic;
using UnityEngine;


namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    public class WeaponController : Controller, IWeaponController
    {
        public event Action OnShotPerformed;
        public event Action OnShotFailed;

        public ICharacterController CharacterController { get; private set; }
        public IWeapon Weapon { get; private set; }
        public string CurrentImpactID { get; private set; }


        #region Public
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
            CharacterController.EquipmentController.OnImpactChanged -= ImpactChangedHandler;
            CharacterController.RootController.UIController.OnFire -= ButtonFireHandler;
            CharacterController.RootController.UIController.OnWeaponReaload -= WeaponRealoadHaundler;

            base.Disable();
        }
        #endregion


        #region Protected
        protected override void CreateHelpersScripts()
        {
            CharacterController.EquipmentController.OnWeaponChanged += WeaponChangedHandler;
            CharacterController.EquipmentController.OnImpactChanged += ImpactChangedHandler;
            CharacterController.RootController.UIController.OnFire += ButtonFireHandler;
            CharacterController.RootController.UIController.OnWeaponReaload += WeaponRealoadHaundler;

            //// Test
            //Pistol pistol = new Pistol(this);
            //pistol.Init();
            //pistol.Serialize();
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }
        #endregion


        #region Private
        private void WeaponChangedHandler(Weapon weapon)
        {
            Weapon = weapon;
            Weapon.WeaponData.Owner = CharacterController;
            Weapon.Init(this);
            Weapon.ShotProcess.OnShotPerformed += ShotHandler;
        }

        private void ImpactChangedHandler(string impactID)
        {
            CurrentImpactID = impactID;
        }

        private void ButtonFireHandler(bool isFire)
        {
            if (Weapon != null)
            {
                if (isFire)
                {
                    Debug.Log("StartFire isFire:" + isFire);

                    Weapon.ShotProcess.StartFire();
                }
                else
                {
                    Debug.Log("StopFire isFire:" + isFire);

                    Weapon.ShotProcess.StopFire();
                }
            }
        }

        private void WeaponRealoadHaundler()
        {
            CharacterController.EquipmentController.ReloadCurrentWeapon();
        }

        private void ShotHandler()
        {
            if (Weapon != null)
            {
                OnShotPerformed?.Invoke();
            }
        }
        #endregion
    }
}