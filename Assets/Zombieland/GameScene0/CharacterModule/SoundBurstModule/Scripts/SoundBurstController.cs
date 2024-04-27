using System.Collections.Generic;
using UnityEngine;
using Zombieland.GameScene0.CharacterModule.WeaponModule;

namespace Zombieland.GameScene0.CharacterModule.SoundBurstModule.Scripts
{
    public class SoundBurstController : Controller, ISoundBurstController
    {
        public ICharacterController CharacterController { get; private set; }

        private SoundBurst _soundBurst;
        private AudioSource _audioSource;
        
        public SoundBurstController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            CharacterController = parentController as ICharacterController;
        }
        public void PlaySound(Weapon weapon)
        {
            _soundBurst.PlaySound(weapon);
        }
        public override void Disable()
        {
            _soundBurst.Disable();
            
            base.Disable();
        }
        
        protected override void CreateHelpersScripts()
        {
            _audioSource = CharacterController.VisualBodyController.CharacterInScene.AddComponent<AudioSource>();

            _soundBurst = new SoundBurst(this ,_audioSource);

            CharacterController.WeaponController.OnShotPerformed += PlaySound;
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }
    }
}
