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
            CharacterController.TakeImpactController.OnApplyImpact += ApplyImpactHandler;
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }

        private void ShotPerformedHandler(Weapon weapon)
        {
            if (weapon.WeaponData.VFXPrefabName != "")
            {
                _vFXCreator.CtreateVFX(weapon.WeaponData.VFXPrefabName, CharacterController.WeaponController.WeaponPointFire.position, CharacterController.WeaponController.WeaponPointFire.rotation);
            }
        }

        private void ApplyImpactHandler(Vector3 position, Vector3 direction)
        {
            Quaternion rotation = Quaternion.LookRotation(-direction);

            _vFXCreator.CtreateVFX("CFX2_Blood", position, rotation);
        }
    }
}