using System;
using System.Numerics;


namespace Zombieland.GameScene0.CharacterModule.SpawnDeathRespawnModule
{
    [Serializable]
    public class SpawnData
    {
        public Vector3 DefaultPosition { get; set; }
        public Vector3 DefaultRotation { get; set; }
        public float SpawnRadius { get; set; }
        public SpawnType SpawnType { get; set; }

    }
}