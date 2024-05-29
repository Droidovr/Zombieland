using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.SoundBurstModule.Scripts
{
    public class SoundBurst
    {
        public event Action OnSound;

        private const float TARGET_VOLUME = 0.2f;

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

            AudioClip clip = _sounds[soundName];
            float adjustedVolume = AdjustVolume(clip, TARGET_VOLUME);
            _audioSource.PlayOneShot(clip, adjustedVolume);

            OnSound?.Invoke();
        }

        private float AdjustVolume(AudioClip clip, float targetVolume)
        {
            float maxSample = 0f;
            float[] samples = new float[clip.samples * clip.channels];
            clip.GetData(samples, 0);

            foreach (float sample in samples)
            {
                if (Mathf.Abs(sample) > maxSample)
                {
                    maxSample = Mathf.Abs(sample);
                }
            }

            return targetVolume / maxSample;
        }
    }
}