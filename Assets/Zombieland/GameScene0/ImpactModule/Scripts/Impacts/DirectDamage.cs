using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Zombieland.GameScene0.ImpactModule
{
    [Serializable]
    public class DirectDamage : IImpactCommand
    {
        [JsonIgnore]
        public IImpactController ImpactController { get; set; }
        public float DamagePoints { get; set; }

        public void Init()
        {
        }
        
        public void Activate()
        {
            Debug.Log("DirectDamage - " + DamagePoints);
        }

        public void Deactivate()
        {
        }
    }
}
