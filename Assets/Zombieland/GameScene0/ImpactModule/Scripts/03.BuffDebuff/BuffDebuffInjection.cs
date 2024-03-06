using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Zombieland.GameScene0.ImpactModule
{
    [Serializable]
    public class BuffDebuffInjection : IImpactCommand
    {
        [JsonIgnore] public IImpact Impact { get; set; }
        
        public List<IImpactable> Targets { get; set; }

        public void Execute()
        {
            Impact.Deactivate();
        }

        public void Deactivate()
        {
            
        }
    }
}
