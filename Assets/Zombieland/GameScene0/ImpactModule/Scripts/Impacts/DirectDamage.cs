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
        public float Points { get; set; }

        public void Init()
        {
        }
        
        public void Activate()
        {
            foreach (var impactableObject in ImpactController.TargetImpactableList)
            {
                impactableObject.ApplyImpact(ImpactController);
            }
            
            Debug.Log("DirectDamage - " + Points);
        }

        public void Deactivate()
        {
        }
    }
}
