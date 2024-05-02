using System;
using System.Collections.Generic;
using UnityEngine;
using Zombieland.GameScene0.CharacterModule.SensorModule.ImpactableSensorModule;

namespace Zombieland.GameScene0.ImpactModule
{
    [Serializable]
    public class TouchColliderDetector
    {
        public void GetTargets(GameObject impactObject, out List<IImpactable> impactablesList,
            out Vector3 collisionPosition)
        {
            var collisionHandler = impactObject.GetComponent<CollisionHandler>();
            if (!collisionHandler.TargetObjectCollider)
            {
                impactablesList = null;
                collisionPosition = Vector3.zero;
            }
            else
            {
                impactablesList = collisionHandler.TargetObjectCollider.TryGetComponent<IImpactable>(out var impactableObject)
                    ? new List<IImpactable> {impactableObject}
                    : null;
                collisionPosition = collisionHandler.CollisionPosition;
            }
        }
    }
}
