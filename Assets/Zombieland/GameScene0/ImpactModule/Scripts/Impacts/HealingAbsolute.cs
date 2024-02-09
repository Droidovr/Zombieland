using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Zombieland.GameScene0.ImpactModule
{
    [Serializable]
    public class HealingAbsolute : IImpactCommand
    {
        [JsonIgnore]
        public IImpactController ImpactController { get; set; }
        public float HealingPoints { get; set; }

        public void Init()
        {
        }
        
        public void Activate()
        {
            Debug.Log("HealingAbsolute - " + HealingPoints);
        }

        public void Deactivate()
        {
        }
    }
}
