using System.Collections.Generic;
using UnityEngine;
using Zombieland.GameScene0.CharacterModule.WeaponModule;

namespace Zombieland.GameScene0.CharacterModule.CharacterVFX
{
    public class CharacterVFXController : Controller, ICharacterVFXController
    {
        public ICharacterController CharacterController { get; private set; }

        private VFXCreator _vFXCreator;

        public CharacterVFXController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            CharacterController = parentController as ICharacterController;
            _vFXCreator = new VFXCreator(this);
        }

        public override void Disable()
        {
            CharacterController.WeaponController.OnShotPerformed -= ShotPerformedHandler;

            base.Disable();
        }

        protected override void CreateHelpersScripts()
        {
            CharacterController.WeaponController.OnShotPerformed += ShotPerformedHandler;
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn�t have any subsystems at the moment.
        }

        private void ShotPerformedHandler(Weapon weapon)
        {
            Transform weaponPointFire = CharacterController.VisualBodyController.WeaponInScene.GetComponent<Transform>().Find("PointFire");

            _vFXCreator.CtreateVFX(weapon.WeaponData.VFXPrefabName, weaponPointFire.position, weaponPointFire.rotation);
        }
    }
}