using System;
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

            PlayOneShot(soundName);
        }
        public void PlaySound(string soundName)
        {
            PlayOneShot(soundName);
        }

        private void PlayOneShot(string soundName)
        {
            if (!LoadSound(soundName))
            {
                return;
            }
            
            _audioSource.PlayOneShot(_sounds[soundName], VOLUME);
        }
        
        private bool LoadSound(string soundName)
        {
            if (IsSoundExist(soundName))
            {
                return true;
            }
            
            try
            {
                AudioClip audio = Resources.Load<AudioClip>(soundName);
                _sounds.Add(soundName, audio);
                return true;
            }
            catch (Exception e)
            {
                Debug.Log($"Sound - {soundName} is not exist in Resources");
                return false;
            }
        }
        private bool IsSoundExist(string soundName)
        {
            if (_sounds.ContainsKey(soundName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
