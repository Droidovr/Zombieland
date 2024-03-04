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
        
        public void Execute()
        {
            // Has no implementation
        }

        public void Deactivate()
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
