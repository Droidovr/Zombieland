using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.NPCModule.NPCSoundModule
{
    public class SoundBurst
    {
        private INPCSoundController _nPCSoundController;
        private AudioSource _audioSource;
        private Dictionary<string, AudioClip> _sounds;
        private LayerMask _wallLayer = LayerMask.GetMask("Wall");


        public SoundBurst(INPCSoundController nPCSoundController)
        {
            _nPCSoundController = nPCSoundController;

            _audioSource = _nPCSoundController.NPCController.NPCVisualBodyController.NPCInScene.GetComponent<AudioSource>();
            _sounds = new Dictionary<string, AudioClip>();
        }

        public void PlaySound(string soundName, float volume)
        {
            if (!_sounds.ContainsKey(soundName))
            {
                AudioClip audio = Resources.Load<AudioClip>(soundName);
                _sounds.Add(soundName, audio);
            }

            Vector3 npcPosition = _nPCSoundController.NPCController.NPCVisualBodyController.NPCInScene.transform.position;
            Vector3 playerPosition = _nPCSoundController.NPCController.NPCManagerController.RootController.CharacterController.VisualBodyController.CharacterInScene.transform.position;

            float distance = Vector3.Distance(npcPosition, playerPosition);

            if (distance <= _audioSource.maxDistance)
            {
                if (!Physics.Linecast(npcPosition, playerPosition, _wallLayer))
                {
                    _audioSource.PlayOneShot(_sounds[soundName], volume);
                }
            }
        }
    }
}
