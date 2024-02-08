using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Zombieland.GameScene0.ImpactModule
{
    [Serializable]
    public class ImpactInstantTeleporter : IImpactCommand
    {
        [JsonIgnore]
        public IImpactController ImpactController { get; set; }
        public string PrefabName { get; set; }

        public List<IImpactCommand> ImpactsList { get; set; }

        public void Init()
        {
            foreach (var impact in ImpactsList)
            {
                impact.ImpactController = ImpactController;
                impact.Init();
            }
        }
        
        public void Execute()
        {
            // Impacts Execution
        }

        public void Deactivate()
        {
        }
    }
}
