using System;
using System.Numerics;

namespace Zombieland.GameScene0.CharacterModule.SpawnDeathRespawnModule
{
    public interface ISpawnDeathRespawnController
    {
        event Action<Vector3> OnSpawn;

        ICharacterController CharacterController { get; }
    }
}