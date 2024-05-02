using System;
using Newtonsoft.Json;
using Zombieland.GameScene0.NPCManagerModule;

namespace Zombieland.GameScene0.NPCModule.NPCDataModule
{
    [Serializable]
    public class NpcData
    {
        public string Name { get; set; }
        public string ID  { get; set; }

        public float MaxHealth { get; set; }
        public float CurrentHealth { get; set; }
        
        public float Speed { get; set; }
        public float StopDistance { get; set; }

        [JsonIgnore]
        public NpcSpawnData SpawnData {get; set;}
        
        public float VisionAwarenessSpeed { get; set; } // How fast vision awareness grows up, after the the target got inside the vision cone
        public float HearingAwarenessSpeed { get; set; } // How fast hearing awareness grows up, after the the target got inside the hearing disc
        public float AwarenessDecaySpeed { get; set; } // How fast general awareness goes down, after the the target left the vision cone and hearing disc. On value 0 OnTargetInFocus(FALSE) event will bew called.
        public float MaxAwarenessLevel { get; set; } // After reaching this level OnTargetInFocus(TRUE) event will bew called.
    }
}
