using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Zombieland.GameScene0.CharacterModule.BuffDebuffModule;

namespace Zombieland.GameScene0.ImpactModule
{
    [Serializable]
    public class InitialImpact : IImpactCommand
    {
        [JsonIgnore] public IImpact Impact { get; set; }
        public IDetector Detector { get; set; }
        public List <DirectImpactData> InitialImpactData { get; set; }

        public virtual void Execute()
        {
            var targetsList = Detector.GetTargets(Impact.ImpactObject);
            Impact.Targets = targetsList;
        }
        
        public void Deactivate()
        {
            // Has no implementation
        }
    }
}
