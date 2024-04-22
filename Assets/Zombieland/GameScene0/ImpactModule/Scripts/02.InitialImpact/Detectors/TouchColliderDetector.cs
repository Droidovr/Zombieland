using System;
using System.Collections.Generic;
using UnityEngine;
using Zombieland.GameScene0.CharacterModule.SensorModule.ImpactableSensorModule;

namespace Zombieland.GameScene0.ImpactModule
{
    [Serializable]
    public class TouchColliderDetector
    {
        public List<IImpactable> GetTargets(GameObject impactObject)
        {
            var targetCollider = impactObject.GetComponent<CollisionHandler>().TargetObjectCollider;
            if (!targetCollider)
                return null;
            return targetCollider.TryGetComponent<IImpactable>(out var impactableObject)
                ? new List<IImpactable> {impactableObject}
                : null;
        }
    }
}
