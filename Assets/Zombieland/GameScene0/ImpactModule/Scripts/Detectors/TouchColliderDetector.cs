using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Zombieland.GameScene0.ImpactModule
{
    [Serializable]
    public class TouchColliderDetector : IImpactCommand
    {
        [JsonIgnore]
        public IImpactController ImpactController { get; set; }

        public void Init()
        {
            var collisionHandler =  ImpactController.ImpactData.DeliveryHandler.ImpactObject.AddComponent<CollisionHandler>();
            collisionHandler.Init(ProcessCollision);
        }
        
        public void Activate()
        {
            // Has no implementation
        }

        private void ProcessCollision(Collider targetObjectCollider)
        {
            if(targetObjectCollider.TryGetComponent<IImpactable>(out var impactableObject))
                ImpactController.TargetImpactableList.Add(impactableObject);
            ImpactController.ImpactData.DeliveryHandler.ApplyImpactOnDelivery();
        }
    }
}
