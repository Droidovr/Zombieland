namespace Zombieland.GameScene0.CharacterModule.BuffDebuffModule
{
    public class BuffDebuffData
    {
        public string ID { get; private set; } // Serializable
        public string Name { get; private set; } // Serializable
        public string IconID { get; private set; } // Serializable
        public string PreabID { get; private set; } // Serializable
        public VFXPosition VFXPosition { get; private set;} // Serializable
        public float LifeTime { get; private set; } // Serializable
        public float Interval { get; private set; } // Serializable
        public DirectImpactData DirectImpactData { get; set; }
        public ICharacterController ImpactTarget { get; set; }
        public ICharacterController Owner { get; set; }
    }
}