using System;
using Newtonsoft.Json;

namespace Zombieland.GameScene0.ImpactModule
{
    [Serializable]
    public class ImpactInstantTeleport : IImpactCommand
    {
        [JsonIgnore] public IImpact Impact { get; set; }

        public void Execute()
        {
            Impact.DirectImpact.Execute();
        }
        public void Deactivate()
        {
            // Has no implementation
        }
    }
}