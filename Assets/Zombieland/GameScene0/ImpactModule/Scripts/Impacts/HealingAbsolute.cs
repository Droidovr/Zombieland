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
        public float Points { get; set; }

        public void Init()
        {
        }
        
        public void Execute()
        {
            foreach (var impactableObject in ImpactController.TargetImpactableList)
            {
                impactableObject.ApplyImpact(ImpactController);
            }
            
            Debug.Log("HealingAbsolute - " + Points);
        }

        public void Deactivate()
        {
        }
    }
}
