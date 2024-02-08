using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Zombieland.GameScene0.ImpactModule
{
    [Serializable]
    public class TouchColliderDetector : IDetectorCommand
    {
        [JsonIgnore]
        public IImpactController ImpactController { get; set; }
        [JsonIgnore]
        public Transform ImpactObjectTransform { get; set; }
        [JsonIgnore]
        public Collider TargetObjectCollider { get; set; }

        public void Init()
        { }

        public void Execute()
        {
            if(TargetObjectCollider.TryGetComponent<IImpactable>(out var impactableObject))
                ImpactController.TargetImpactableList.Add(impactableObject);
        }
    }
}
