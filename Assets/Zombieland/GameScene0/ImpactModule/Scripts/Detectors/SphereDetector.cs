using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Zombieland.GameScene0.ImpactModule
{
    [Serializable]
    public class SphereDetector : IImpactCommand
    {
        [JsonIgnore]
        public IImpactController ImpactController { get; set; }
        public bool ExecuteOnActivation { get; set; }
        public float DetectionRadius { get; set; }

        private GameObject _impactObject;
        private CollisionHandler _collisionHandler;

        public void Init()
        {
            _impactObject = ImpactController.ImpactData.DeliveryHandler.ImpactObject;
            if(ExecuteOnActivation) return;
            _collisionHandler = _impactObject.AddComponent<CollisionHandler>();
            _collisionHandler.Init(ProcessCollision);
        }

        public void Execute()
        {
            if (ExecuteOnActivation)
            {
                ProcessCollision(null);
            }
        }

        public void Deactivate()
        {
            // Has no implementation
        }

        public void ProcessCollision(Collider targetObjectCollider)
        {
            var overlapColliders = Physics.OverlapSphere(_impactObject.transform.position, DetectionRadius);
            if (overlapColliders.Length > 0)
            {
                var impactableObjects = new List<IImpactable>();
                foreach (var overlapCollider in overlapColliders)
                {
                    if(overlapCollider.TryGetComponent<IImpactable>(out var impactableObject))
                        impactableObjects.Add(impactableObject);
                }
                ImpactController.TargetImpactableList = impactableObjects;
            }
            ImpactController.ImpactData.DeliveryHandler.ApplyImpactOnDelivery();
        }
    }
}