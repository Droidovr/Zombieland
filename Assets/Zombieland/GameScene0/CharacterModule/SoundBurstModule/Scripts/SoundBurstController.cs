using System;
using System.Collections.Generic;
using UnityEngine;
using Zombieland.GameScene0.CharacterModule.WeaponModule;

namespace Zombieland.GameScene0.CharacterModule.SoundBurstModule.Scripts
{
    public class SoundBurstController : Controller, ISoundBurstController
    {
        public ICharacterController CharacterController { get; private set; }
        
        private AudioIDAsset _audioID;
        private SoundBurst _soundBurst;
        private AudioSource _audioSource;
        
        public SoundBurstController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            CharacterController = parentController as ICharacterController;
        }
        
        public override void Disable()
        {
            CharacterController.WeaponController.OnShotPerformed -= PlayWeaponSound;
            CharacterController.AnimationController.OnStep -= () => PlaySimpleSound(_audioID.walkSoundName);
            CharacterController.TakeImpactController.OnApplyImpact -= PlayImpactSound;

            base.Disable();
        }
        
        protected override void CreateHelpersScripts()
        {
            _audioSource = CharacterController.VisualBodyController.CharacterInScene.AddComponent<AudioSource>();
            _audioID = Resources.Load<AudioIDAsset>("AudioID");

            _soundBurst = new SoundBurst(_audioSource);

            CharacterController.WeaponController.OnShotPerformed += PlayWeaponSound;
            CharacterController.AnimationController.OnStep += () => PlaySimpleSound(_audioID.walkSoundName);
           //CharacterController.TakeImpactController.OnApplyImpact += PlayImpactSound;
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }
        
        private void PlayWeaponSound(Weapon weapon)
        {
            _soundBurst.PlaySound(weapon);
        }

        private void PlaySimpleSound(string soundName)
        {
            _soundBurst.PlaySound(soundName);
        }
        private void PlayImpactSound(Vector3 vector1, Vector3 vector2)
        {
            _soundBurst.PlaySound(_audioID.impactSoundName);
        }
    }
}
