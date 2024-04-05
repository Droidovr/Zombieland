using System;
using UnityEngine;

namespace Zombieland.GameScene0.NPCModule.NPCDataModule
{
    [Serializable]
    public class NPCData
    {
        public string Name = "NPCDefault";
        public string ID;

        public float MaxHealth = 100;
        public float CurrentHealth = 100;

        public float Speed = 10;
        public Vector3 SpawnPosition = Vector3.zero;
        public float StopDistance;
    }
}
