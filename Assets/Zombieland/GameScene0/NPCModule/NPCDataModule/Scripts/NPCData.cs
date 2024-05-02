using System;
using Newtonsoft.Json;

namespace Zombieland.GameScene0.NPCModule.NPCDataModule
{
    [Serializable]
    public class NpcData
    {
        public string name = "NpcDefault";
        public string ID;

        public float maxHealth = 100;
        public float currentHealth = 100;
        
        public float speed = 10;
        public float stopDistance;

        [JsonIgnore]
        public NpcSpawnData SpawnData {get; set;}
        
        public float visionAwarenessSpeed;
        public float hearingAwarenessSpeed;
        public float awarenessDecaySpeed;
        public float maxAwarenessLevel;
    }
}
