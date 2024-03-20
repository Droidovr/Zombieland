using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.ImpactModule
{
    [Serializable]
    public class TouchColliderDetector
    {
        public List<IImpactable> GetTargets(GameObject impactObject)
        {
            var targetCollider = impactObject.GetComponent<CollisionHandler>().TargetObjectCollider;
            return targetCollider.TryGetComponent<IImpactable>(out var impactableObject)
                ? new List<IImpactable> {impactableObject}
                : null;
        }
    }
}
