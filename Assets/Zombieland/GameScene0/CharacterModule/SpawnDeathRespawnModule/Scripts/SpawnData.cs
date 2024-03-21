using System;

namespace Zombieland.GameScene0.CharacterModule.SpawnDeathRespawnModule
{
    [Serializable]
    public class SpawnData
    {
        public System.Numerics.Vector3 DefaultPosition { get; set; }
        public float SpawnRadius { get; set; }
        public SpawnType SpawnType { get; set; }

    }
}