using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.SoundBurstModule.Scripts
{
    public class SoundBurst
    {
        public event Action OnSound;

        private const float VOLUME = 0.3f;

        private AudioSource _audioSource;
        private Dictionary<string, AudioClip> _sounds;

        public SoundBurst(ISoundBurstController soundBurstController)
        {
            _audioSource = soundBurstController.CharacterController.VisualBodyController.CharacterInScene.AddComponent<AudioSource>();

            _sounds = new Dictionary<string, AudioClip>();
        }
        public void PlaySound(string soundName)
        {
            if (!_sounds.ContainsKey(soundName))
            {
                AudioClip audio = Resources.Load<AudioClip>(soundName);
                _sounds.Add(soundName, audio);
            }
            
            _audioSource.PlayOneShot(_sounds[soundName], VOLUME);

            OnSound?.Invoke();
        }
    }
}
