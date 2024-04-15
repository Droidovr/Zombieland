using System;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.SpawnDeathRespawnModule
{
    public class SpawnHelper
    {
        public event Action<Vector3, Quaternion> OnSpawn;

        private readonly SpawnDeathRespawnController _spawnDeathRespawnController;

        public SpawnHelper(SpawnDeathRespawnController spawnDeathRespawnController)
        {
            _spawnDeathRespawnController = spawnDeathRespawnController;
        }

        public void Start()
        {
            var characterData = _spawnDeathRespawnController.CharacterController.CharacterDataController.CharacterData;
            var radiusAgent = _spawnDeathRespawnController.CharacterController.VisualBodyController.CharacterInScene.GetComponent<UnityEngine.CharacterController>().radius;

            SpawnData spawnData = characterData.SpawnData;
            Vector3 defaultPosition = new Vector3(spawnData.DefaultPosition.X, spawnData.DefaultPosition.Y, spawnData.DefaultPosition.Z);
            Quaternion defaultRotation = Quaternion.Euler(new Vector3(spawnData.DefaultRotation.X, spawnData.DefaultRotation.Y, spawnData.DefaultRotation.Z));
            AvailablePosition availablePosition = new AvailablePosition();

            Vector3 spawnPosition = availablePosition.GetSpawnPosition(defaultPosition, spawnData.SpawnRadius, radiusAgent, spawnData.SpawnType);

            if (spawnPosition != null)
            {
                OnSpawn?.Invoke(spawnPosition, defaultRotation);
            }
        }
    }
}