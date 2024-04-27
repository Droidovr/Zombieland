using System;
using System.Collections.Generic;
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
        public Vector3 spawnPosition = Vector3.zero;
        public float stopDistance;

        public List<Vector3> wanderPositions;
        
        public float visionAwarenessSpeed;
        public float hearingAwarenessSpeed;
        public float awarenessDecaySpeed;
        public float maxAwarenessLevel;
    }
}
