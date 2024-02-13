using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Zombieland.GameScene0.ImpactModule
{
    [Serializable]
    public class CollisionEffect : IImpactCommand
    {
        [JsonIgnore]
        public IImpactController ImpactController { get; set; }
        public string EffectPrefabName { get; set; }
    
        public void Init()
        {
            // has no implementation
        }

        public void Activate()
        {
            var effectPrefab = Resources.Load<GameObject>(EffectPrefabName);
            foreach (var impactableObject in ImpactController.TargetImpactableList)
            {
                GameObject.Instantiate(effectPrefab, impactableObject.ImpactObjectTransform);
            }
        }

        public void Deactivate()
        {
            // has no implementation
        }
    }
}
