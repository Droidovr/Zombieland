using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.NPCModule.NPCSoundModule
{
    public class SoundBurst
    {
        private const float VOLUME = 0.7f;

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

        public void PlaySound(string soundName)
        {
            if (!_sounds.ContainsKey(soundName))
            {
                AudioClip audio = Resources.Load<AudioClip>(soundName);
                _sounds.Add(soundName, audio);
            }

            //_audioSource.PlayOneShot(_sounds[soundName], VOLUME);

            float distance = Vector3.Distance
                    (
                        _nPCSoundController.NPCController.NPCVisualBodyController.NPCInScene.transform.position,
                        _nPCSoundController.NPCController.NPCManagerController.RootController.CharacterController.VisualBodyController.CharacterInScene.transform.position
                    );

            if (distance <= _audioSource.maxDistance)
            {
                Vector3 direction = _nPCSoundController.NPCController.NPCVisualBodyController.NPCInScene.transform.position -
                    _nPCSoundController.NPCController.NPCManagerController.RootController.CharacterController.VisualBodyController.CharacterInScene.transform.position;

                Ray ray = new Ray(_nPCSoundController.NPCController.NPCVisualBodyController.NPCInScene.transform.position, direction);

                RaycastHit hit;
                bool isHit = Physics.Raycast(ray, out hit, direction.magnitude);

                if (isHit && ((1 << hit.collider.gameObject.layer) & _wallLayer) == 0)
                {
                    Debug.Log("PlaySound: " + soundName);
                    _audioSource.PlayOneShot(_sounds[soundName], VOLUME);
                }
            }
        }
    }
}
