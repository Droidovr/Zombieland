using UnityEngine;
using Zombieland.GameScene0.CharacterModule.WeaponModule;

namespace Zombieland.GameScene0.CharacterModule.SoundBurstModule.Scripts
{
    public class SoundBurst
    {
        private ISoundBurstController _soundBurstController;
        private AudioSource _audioSource;

        public SoundBurst(ISoundBurstController controller ,AudioSource audioSource)
        {
            _soundBurstController = controller;
            _audioSource = audioSource;
        }

        public void Disable()
        {
            
        }
        
        public void PlaySound(Weapon weapon)
        {
            
        }
    }
}
