using System;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.SpawnDeathRespawnModule
{
    public class SpawnHelper
    {
        public event Action<Vector3> OnSpawn;

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
            Vector3 DefaultPosition = new Vector3(spawnData.DefaultPosition.X, spawnData.DefaultPosition.Y, spawnData.DefaultPosition.Z);
            AvailablePosition availablePosition = new AvailablePosition();

            Vector3 spawnPosition = availablePosition.GetSpawnPosition(DefaultPosition, spawnData.SpawnRadius, radiusAgent, spawnData.SpawnType);

            if (spawnPosition != null)
            {
                OnSpawn?.Invoke(spawnPosition);
            }
        }
    }
}