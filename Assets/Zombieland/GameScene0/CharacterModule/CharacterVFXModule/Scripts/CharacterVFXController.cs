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
            Debug.Log(CharacterController.WeaponController.WeaponPointFire.position);

            Vector3 localPoint = new Vector3(0f, 0.0743f, 0.305f);
            Vector3 globalPoint = CharacterController.VisualBodyController.WeaponInScene.transform.TransformPoint(localPoint);

            _vFXCreator.CtreateVFX(weapon.WeaponData.VFXPrefabName, globalPoint, CharacterController.WeaponController.WeaponPointFire.rotation);
        }

        private void ApplyImpactHandler(Vector3 position, Vector3 direction)
        {
            Quaternion rotation = Quaternion.LookRotation(-direction);

            _vFXCreator.CtreateVFX("CFX2_Blood", position, rotation);
        }
    }
}