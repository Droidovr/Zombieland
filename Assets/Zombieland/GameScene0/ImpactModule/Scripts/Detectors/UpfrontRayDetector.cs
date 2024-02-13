using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Zombieland.GameScene0.ImpactModule
{
    [Serializable]
    public class UpfrontRayDetector : IImpactCommand
    {
        [JsonIgnore]
        public IImpactController ImpactController { get; set; }
        public bool ExecuteOnActivation { get; set; }
        public float DetectionRadius { get; set; }

        private GameObject _impactObject;
        private CollisionHandler _collisionHandler;

        private float _castSphereRadius;
        private const float MinCastSphereRadius = 0.2f;


        public void Init()
        {
            _impactObject = ImpactController.ImpactData.DeliveryHandler.ImpactObject;
            if(ExecuteOnActivation) return;
            _collisionHandler = _impactObject.AddComponent<CollisionHandler>();
            _collisionHandler.Init(ProcessCollision);

            _castSphereRadius = _impactObject.TryGetComponent<SphereCollider>(out var sphereCollider) 
                ? sphereCollider.radius 
                : MinCastSphereRadius;
        }

        public void Activate()
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

        private void ProcessCollision(Collider targetObjectCollider)
        {
            var raycastHits = Physics.SphereCastAll(_impactObject.transform.position, _castSphereRadius, _impactObject.transform.forward, DetectionRadius);
            if(raycastHits.Length <= 0) return;
            var impactableObjects = new List<IImpactable>();
            foreach (var raycastHit in raycastHits)
            {
                if (raycastHit.collider.TryGetComponent<IImpactable>(out var impactableObject))
                {
                    impactableObjects.Add(impactableObject);
                }
            }
            
            ImpactController.TargetImpactableList = impactableObjects;
            ImpactController.ImpactData.DeliveryHandler.ApplyImpactOnDelivery();
        }
    }
}
