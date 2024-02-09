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
        public float DetectionRayDistance { get; set; }

        private GameObject _impactObject;
        private CollisionHandler _collisionHandler;

        public void Init()
        {
            _impactObject = ImpactController.ImpactData.DeliveryHandler.ImpactObject;
            if(ExecuteOnActivation) return;
            _collisionHandler = _impactObject.AddComponent<CollisionHandler>();
            _collisionHandler.Init(ProcessCollision);
        }

        public void Activate()
        {
            if (ExecuteOnActivation)
            {
                ProcessCollision();
            }
        }

        private void ProcessCollision()
        {
            var raycastHits = Physics.RaycastAll(_impactObject.transform.position, Vector3.forward, DetectionRayDistance);
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
