namespace Zombieland.GameScene0.CharacterModule.BuffDebuffModule
{
    public class BuffDebuffData
    {
        public string ID { get; set; } // Serializable
        public string Name { get; set; } // Serializable
        public string IconID { get; set; } // Serializable
        public string PrefabID { get; set; } // Serializable
        public VFXPosition VFXPosition { get; set;} // Serializable
        public int LifeTime { get; set; } // Serializable
        public int Interval { get; set; } // Serializable
        public DirectImpactData DirectImpactData { get; set; }
        public ICharacterController ImpactTarget { get; set; }
        public ICharacterController Owner { get; set; }
    }
}