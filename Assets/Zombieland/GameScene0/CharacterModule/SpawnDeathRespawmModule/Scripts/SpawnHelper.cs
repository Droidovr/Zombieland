using System;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.SpawnDeathRespawmModule.Scripts
{
  public class SpawnHelper
  {
    public event Action<Vector3> OnSpawn;
    
    private SpawnDeathRespawnController _spawnDeathRespawnController;
    private ICommandSpawner _spawnMethod;
    
    public SpawnHelper(SpawnDeathRespawnController spawnDeathRespawnController)
    {
      _spawnDeathRespawnController = spawnDeathRespawnController;
    }

    public void Enable()
    {
      var characterData = _spawnDeathRespawnController.CharacterController.CharacterDataController.CharacterData;
      _spawnMethod = characterData.SpawnMethod;
      _spawnMethod.SpawnHelper = this;
      Debug.Log($"<color=red>{_spawnMethod.SpawnHelper.ToString()}</color>");
      _spawnMethod.Execute();
    }

    public void SpawnHandler(Vector3 spawnPosition)
    {
      Debug.Log($"<color=red>{spawnPosition}</color>");
      OnSpawn?.Invoke(spawnPosition);
    }
    
    public void Disable()
    {
      
    }


  }
}