using System.Collections.Generic;
using UnityEngine;
using Zombieland.GameScene0.CharacterModule.WeaponModule;

namespace Zombieland.GameScene0.CharacterModule.SoundBurstModule.Scripts
{
    public class SoundBurst
    {
        private const float VOLUME = 0.7f;
        private AudioSource _audioSource;

        private Dictionary<string, AudioClip> _sounds;

        public SoundBurst(AudioSource audioSource)
        {
            _sounds = new Dictionary<string, AudioClip>();
            
            _audioSource = audioSource;
        }
        
        public void PlaySound(Weapon weapon)
        {
            string soundName = weapon.WeaponData.SoundName;

            if (!_sounds.ContainsKey(soundName))
            {
                LoadSound(soundName);
            }
            
            _audioSource.PlayOneShot(_sounds[soundName], VOLUME);
        }
        public void PlaySound(string soundName)
        {
            if (!_sounds.ContainsKey(soundName))
            {
                LoadSound(soundName);
            }
            
            _audioSource.PlayOneShot(_sounds[soundName], VOLUME);
        }

        private void LoadSound(string soundName)
        {
            _sounds.Add(soundName, Resources.Load<AudioClip>(soundName));
        }
    }
}
