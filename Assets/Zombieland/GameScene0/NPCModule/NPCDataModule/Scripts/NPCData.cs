using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Zombieland.GameScene0.NPCModule.NPCDataModule
{
    [Serializable]
    public class NpcData
    {
        public string name = "NPCDefault";
        public string ID;

        public float maxHealth = 100;
        public float currentHealth = 100;
        
        public float speed = 10;
        public float stopDistance;

        [JsonIgnore]
        public Vector3 spawnPosition;
        [JsonIgnore]
        public List<Vector3> wanderPositions = new List<Vector3>();
        
        public float visionAwarenessSpeed;
        public float hearingAwarenessSpeed;
        public float awarenessDecaySpeed;
        public float maxAwarenessLevel;
    }
}
