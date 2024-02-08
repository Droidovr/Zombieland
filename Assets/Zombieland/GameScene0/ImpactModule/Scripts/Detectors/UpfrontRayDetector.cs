using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Zombieland.GameScene0.ImpactModule
{
    [Serializable]
    public class UpfrontRayDetector : IDetectorCommand
    {
        [JsonIgnore]
        public IImpactController ImpactController { get; set; }
        [JsonIgnore]
        public Transform ImpactObjectTransform { get; set; }
        [JsonIgnore]
        public Collider TargetObjectCollider { get; set; }
        public float DetectionRayDistance { get; set; }

        public void Init()
        { }

        public void Execute()
        {
            var raycastHits = Physics.RaycastAll(ImpactObjectTransform.position, Vector3.forward, DetectionRayDistance);
            if(raycastHits.Length <= 0) return;
            var impactableObjects = new List<IImpactable>();
            foreach (var raycastHit in raycastHits)
            {
                if(raycastHit.collider.TryGetComponent<IImpactable>(out var impactableObject))
                    impactableObjects.Add(impactableObject);
            }
            ImpactController.TargetImpactableList = impactableObjects;
        }
    }
}
