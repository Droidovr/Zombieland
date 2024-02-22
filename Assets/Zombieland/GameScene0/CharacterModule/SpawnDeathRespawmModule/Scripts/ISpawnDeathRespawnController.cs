using System;
using System.Numerics;

namespace Zombieland.GameScene0.CharacterModule.SpawnDeathRespawmModule
{
  public interface ISpawnDeathRespawnController
  {
    event Action<Vector3> OnSpawn;
    
    ICharacterController CharacterController { get; }
  }
}