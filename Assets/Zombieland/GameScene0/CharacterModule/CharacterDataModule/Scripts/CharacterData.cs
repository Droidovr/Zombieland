using System;

namespace Zombieland.GameScene0.CharacterModule.CharacterDataModule
{
    [Serializable]
    public class CharacterData
    {
        public float MaxMovingSpeed;
        public float DesignMovingSpeed;
        public float MaxRotationSpeed;
        public float DesignRotationSpeed;
 
        public float HP;
        public float HPMax;
        public float HPDefault;
        public float Stamina;

        public bool IsDead;
        public bool IsStunned;
    }
}