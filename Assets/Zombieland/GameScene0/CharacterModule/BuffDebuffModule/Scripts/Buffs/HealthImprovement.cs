using System;

namespace Zombieland.GameScene0.CharacterModule.BuffDebuffModule
{
    [Serializable]
    public class HealthImprovement : IBuffDebuffCommand
    {
        public string Name { get; private set; }
        public ICharacterController ImpactTarget { get; set; }
        public ICharacterController Owner { get; set; }

        public float LifeTime = 20f;

        private BuffDebuffController _buffDebuffController;

        public HealthImprovement(BuffDebuffController buffDebuffController)
        {
            _buffDebuffController = buffDebuffController;
        }

        public void Execute()
        {
            
        }

        public SingleImpact GetProcessedImpactValue(SingleImpact buffDebuff)
        {
              return buffDebuff;
        }
    }
}