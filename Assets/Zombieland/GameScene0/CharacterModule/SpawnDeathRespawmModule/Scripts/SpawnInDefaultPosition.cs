using System;
using UnityEngine;
using Zombieland;
using Zombieland.GameScene0.CharacterModule.SpawnDeathRespawmModule;
using Zombieland.GameScene0.CharacterModule.SpawnDeathRespawmModule.Scripts;

//namespace Zombieland.GameScene0.CharacterModule.SpawnDeathRespawmModule
//{
  public class SpawnInDefaultPosition : ICommandSpawner
  {
    public SpawnHelper SpawnHelper { get; set; }

    public System.Numerics.Vector3 DefaultSpawnPosition;
    public ISpawnDeathRespawnController SpawnDeathRespawnController;


    public void Execute()
    {
      //проверить и найти свободную точку спавна

      UnityEngine.Vector3 defaultSpawnPosition = new UnityEngine.Vector3(DefaultSpawnPosition.X, DefaultSpawnPosition.Y, DefaultSpawnPosition.Z);
      
      SpawnHelper.SpawnHandler(defaultSpawnPosition);
      //Debug.Log($"{DefaultSpawnPosition}");
      
    }
  }
//}