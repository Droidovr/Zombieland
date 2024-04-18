using System;
using UnityEngine;

namespace Zombieland.GameScene0.NPCModule.NPCDataModule
{
    [Serializable]
    public class NPCData
    {
        public string name = "NPCDefault";
        public string ID;

        public float maxHealth = 100;
        public float currentHealth = 100;

        public float speed = 10;
        public Vector3 spawnPosition = Vector3.zero;
        public float stopDistance;
        
        public float visionAwarenessSpeed;
        public float hearingAwarenessSpeed;
        public float awarenessDecaySpeed;
        public float maxAwarenessLevel;
    }
}
