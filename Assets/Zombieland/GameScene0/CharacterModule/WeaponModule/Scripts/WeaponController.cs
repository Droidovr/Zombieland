using System;
using System.Collections.Generic;


namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    public class WeaponController : Controller, IWeaponController
    {
        public event Action<Weapon> OnShotPerformed;
        public event Action OnShotFailed;

        public ICharacterController CharacterController { get; private set; }
        public IWeapon Weapon { get; private set; }


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
            CharacterController.VisualBodyController.OnWeaponInSceneReady -= WeaponInSceneReadyHandler;
            CharacterController.AnimationController.OnAnimationAttack -= ButtonFireHandler;

            base.Disable();
        }
        #endregion


        #region Protected
        protected override void CreateHelpersScripts()
        {
            CharacterController.EquipmentController.OnWeaponChanged += WeaponChangedHandler;
            CharacterController.VisualBodyController.OnWeaponInSceneReady += WeaponInSceneReadyHandler;
            CharacterController.AnimationController.OnAnimationAttack += ButtonFireHandler;

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
            if (Weapon != null)
            {
                Weapon.ShotProcess.StopFire();
                Weapon.ShotProcess.OnShotPerformed -= ShotHandler;
            }

            Weapon = weapon;
        }

        private void WeaponInSceneReadyHandler()
        {
            Weapon.Init(this);
            Weapon.ShotProcess.OnShotPerformed += ShotHandler;
        }

        private void ButtonFireHandler(bool isFire)
        {
            if (Weapon != null)
            {
                if (isFire)
                {
                    Weapon.ShotProcess.StartFire();
                }
                else
                {
                    Weapon.ShotProcess.StopFire();
                }
            }
        }

        private void ShotHandler()
        {
            if (Weapon != null)
            {
                OnShotPerformed?.Invoke((Weapon) Weapon);
            }
        }
        #endregion
    }
}