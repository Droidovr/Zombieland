using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Zombieland.GameScene0.ImpactModule
{
    [Serializable]
    public class SphereDetector : IDetectorCommand
    {
        [JsonIgnore]
        public IImpactController ImpactController { get; set; }
        [JsonIgnore]
        public Transform ImpactObjectTransform { get; set; }
        [JsonIgnore]
        public Collider TargetObjectCollider { get; set; }
        public float DetectionRadius { get; set; }

        public void Init()
        { }

        public void Execute()
        {
            var overlapColliders = Physics.OverlapSphere(ImpactObjectTransform.position, DetectionRadius);
            if(overlapColliders.Length <= 0) return;
            var impactableObjects = new List<IImpactable>();
            foreach (var overlapCollider in overlapColliders)
            {
                if(overlapCollider.TryGetComponent<IImpactable>(out var impactableObject))
                    impactableObjects.Add(impactableObject);
            }
            ImpactController.TargetImpactableList = impactableObjects;
        }
    }
}
