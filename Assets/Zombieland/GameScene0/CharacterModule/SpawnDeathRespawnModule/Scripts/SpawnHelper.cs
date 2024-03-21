using System;
using UnityEngine;
using Zombieland.GameScene0.CharacterModule.SpawnDeathRespawnModule;

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

            SpawnData spawnData = characterData.SpawnData;
            //AvailablePosition availablePosition = new AvailablePosition();

            //Vector3 spawnPosition = availablePosition.GetSpawnPosition(spawnData.DefaultPosition, spawnData.SpawnRadius, 0.2f);
            //OnSpawn?.Invoke(spawnPosition);
        }
    }
}