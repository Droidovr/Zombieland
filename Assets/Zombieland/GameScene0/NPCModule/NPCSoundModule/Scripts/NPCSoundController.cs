using System.Collections.Generic;
using UnityEngine;
using Zombieland.GameScene0.WeaponModule;


namespace Zombieland.GameScene0.NPCModule.NPCSoundModule
{
    public class NPCSoundController : Controller, INPCSoundController
    {
        public INPCController NPCController { get; private set; }

        private SoundBurst _soundBurst;


        public NPCSoundController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            NPCController = parentController as INPCController;
        }


        protected override void CreateHelpersScripts()
        {
            _soundBurst = new SoundBurst(this);

            NPCController.NPCWeaponController.OnShotPerformed += PlayWeaponSound;
            NPCController.NPCAnimationController.OnStep += PlayOnStepSound;
            NPCController.NPCTakeDamageController.OnApplyImpact += PlayImpactSound;
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }


        private void PlayWeaponSound(Weapon weapon)
        {
            _soundBurst.PlaySound(weapon.WeaponData.SoundName, 0.3f);
        }

        private void PlayOnStepSound()
        {
            _soundBurst.PlaySound("Walk", 0.7f);
        }

        private void PlayImpactSound(Vector3 vector1, Vector3 vector2)
        {
            _soundBurst.PlaySound("Hit", 0.7f);
        }
    }
}