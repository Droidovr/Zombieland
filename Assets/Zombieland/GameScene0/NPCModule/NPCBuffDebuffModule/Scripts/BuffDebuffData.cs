using Newtonsoft.Json;
using System;
using Zombieland.GameScene0.CharacterModule;


namespace Zombieland.GameScene0.NPCModule.NPCBuffDebuffModule
{
    [Serializable]
    public class BuffDebuffData
    {
        public string ID { get; set; } // Serializable
        public string Name { get; set; } // Serializable
        public string IconID { get; set; } // Serializable
        public string PrefabID { get; set; } // Serializable
        public VFXPosition VFXPosition { get; set; } // Serializable
        public float LifeTime { get; set; } // Serializable
        public float Interval { get; set; } // Serializable
        public DirectImpactData DirectImpactData { get; set; }
        [JsonIgnore] public ICharacterController ImpactTarget { get; set; }
        [JsonIgnore] public INPCController Owner { get; set; }
    }
}