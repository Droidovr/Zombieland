using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.ImpactModule
{
    [Serializable]
    public class UpfrontRayDetector
    {
        public float DetectionRadius { get; set; }
        private const float MinCastSphereRadius = 0.2f;

        public List<IImpactable> GetTargets(GameObject impactObject)
        {
            var castSphereRadius = impactObject.TryGetComponent<SphereCollider>(out var sphereCollider) 
                ? sphereCollider.radius 
                : MinCastSphereRadius;
            
            var raycastHits = Physics.SphereCastAll(impactObject.transform.position, castSphereRadius, impactObject.transform.forward, DetectionRadius);
            if (raycastHits.Length > 0)
            {
                var impactableObjects = new List<IImpactable>();
                foreach (var raycastHit in raycastHits)
                {
                    if (raycastHit.collider.TryGetComponent<IImpactable>(out var impactableObject))
                    {
                        impactableObjects.Add(impactableObject);
                    }
                }
                return impactableObjects;
            }

            return null;
        }
    }
}
