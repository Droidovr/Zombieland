using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.NPCModule.NPCSoundModule
{
    public class SoundBurst
    {
        private const float VOLUME = 0.7f;

        private AudioSource _audioSource;
        private Dictionary<string, AudioClip> _sounds;
        private Plane[] _planes;
        private Collider _nPCCollider;

        public SoundBurst(INPCSoundController nPCSoundController)
        {
            _audioSource = nPCSoundController.NPCController.NPCVisualBodyController.NPCInScene.AddComponent<AudioSource>();
            
            Camera camera = nPCSoundController.NPCController.NPCManagerController.RootController.CameraController.PlayerCamera;
            _planes = GeometryUtility.CalculateFrustumPlanes(camera);

            _nPCCollider = nPCSoundController.NPCController.NPCVisualBodyController.NPCInScene.GetComponentInChildren<Collider>();

            _sounds = new Dictionary<string, AudioClip>();
        }

        public void PlaySound(string soundName)
        {
            if (!_sounds.ContainsKey(soundName))
            {
                AudioClip audio = Resources.Load<AudioClip>(soundName);
                _sounds.Add(soundName, audio);
            }

            if (GeometryUtility.TestPlanesAABB(_planes, _nPCCollider.bounds))
            {
                Debug.Log("<color=red>_nPCRenderer.isVisible != null</color>");
            }
            else
            {
                Debug.Log("<color=red>_nPCRenderer.isVisible = null</color>");
            }

            if (GeometryUtility.TestPlanesAABB(_planes, _nPCCollider.bounds))
            {
                _audioSource.PlayOneShot(_sounds[soundName], VOLUME);
            }
        }
    }
}
